/*
 * Authors:
 *   钟峰(Popeye Zhong) <9555843@qq.com>
 * 
 * Copyright (C) 2015-2017 Zongsoft Corporation. All rights reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("MessageId", 100000)]
	[DataSearchKey("Status:Stauts", "Creator,CreatorId:CreatorId", "Key:Subject")]
	public class MessageService : ServiceBase<Message>
	{
		#region 构造函数
		public MessageService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public IEnumerable<Message.MessageMember> GetMembers(ulong messageId, MessageMemberStatus? status = null, ConditionalRange<DateTime> period = null, Paging paging = null)
		{
			var conditions = ConditionCollection.And(Condition.Equal("MessageId", messageId));

			if(status.HasValue)
				conditions.Add(Condition.Equal("Status", status.Value));

			if(period != null && period.HasValue)
				conditions.Add(period.ToCondition("CreatedTime"));

			return this.DataAccess.Select<Message.MessageMember>(conditions, "User, User.User", paging);
		}
		#endregion

		#region 重写方法
		protected override Message OnGet(ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			var message = base.OnGet(condition, scope);

			if(message == null)
				return null;

			//如果内容类型是外部文件（即非嵌入格式），则读取文件内容
			if(!Utility.IsContentEmbedded(message.ContentType))
				message.Content = Utility.ReadTextFile(message.Content);

			//获取当前用户的凭证
			var credential = this.EnsureCredential(false);

			if(credential != null && credential.CredentialId != null && credential.CredentialId.Length > 0)
			{
				//更新当前用户对该消息的读取状态
				this.DataAccess.Update(this.DataAccess.Naming.Get<Message.MessageMember>(), new
				{
					Status = MessageMemberStatus.Read,
					StatusTimestamp = DateTime.Now,
				}, Condition.Equal("MessageId", message.MessageId) & Condition.Equal("UserId", credential.UserId));
			}

			return message;
		}

		protected override IEnumerable<Message> OnSelect(ICondition condition, Grouping grouping, string scope, Paging paging, params Sorting[] sortings)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			return base.OnSelect(condition, grouping, scope, paging, sortings);
		}

		protected override int OnDelete(ICondition condition, string[] cascades)
		{
			//获取待删除的数据集
			var messages = this.Select(condition).ToArray();

			//调用基类同名方法
			var count = base.OnDelete(condition, cascades);

			if(count > 0)
			{
				foreach(var message in messages)
				{
					if(!Utility.IsContentEmbedded(message.ContentType))
						Utility.DeleteFile(message.Content);
				}
			}

			return count;
		}

		protected override int OnInsert(DataDictionary<Message> data, string scope)
		{
			string filePath = null;

			//获取原始的内容类型
			var rawType = data.Get(p => p.ContentType, null);

			//调整内容类型为嵌入格式
			data.Set(p => p.ContentType, Utility.GetContentType(rawType, true));

			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//设置内容文件的存储路径
				filePath = this.GetContentFilePath(data.Get(p => p.MessageId), data.Get(p => p.ContentType));

				//将内容文本写入到文件中
				Utility.WriteTextFile(filePath, value);

				//更新内容文件的存储路径
				data.Set(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.Set(p => p.ContentType, Utility.GetContentType(data.Get(p => p.ContentType), false));
			});

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				var count = base.OnInsert(data, scope);

				if(count < 1)
				{
					//如果新增记录失败则删除刚创建的文件
					if(filePath != null && filePath.Length > 0)
						Utility.DeleteFile(filePath);

					return count;
				}

				data.TryGet(p => p.Members, (key, members) =>
				{
					if(members == null)
						return;

					var messageId = data.Get(p => p.MessageId);

					foreach(var member in members)
					{
						member.MessageId = messageId;
						member.Status = MessageMemberStatus.None;
					}

					this.DataAccess.InsertMany(members);
				});

				//提交事务
				transaction.Commit();

				return count;
			}
		}

		protected override int OnUpdate(DataDictionary<Message> data, ICondition condition, string scope)
		{
			//更新内容到文本文件中
			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//根据当前反馈编号，获得其对应的内容文件存储路径
				var filePath = this.GetContentFilePath(data.Get(p => p.MessageId), data.Get(p => p.ContentType));

				//将反馈内容写入到对应的存储文件中
				Utility.WriteTextFile(filePath, value);

				//更新当前反馈的内容文件存储路径属性
				data.Set(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.Set(p => p.ContentType, Utility.GetContentType(data.Get(p => p.ContentType), false));
			});

			//调用基类同名方法
			var count = base.OnUpdate(data, condition, scope);

			if(count < 1)
				return count;

			return count;
		}
		#endregion

		#region 虚拟方法
		protected virtual string GetContentFilePath(ulong messageId, string contentType)
		{
			return string.Format("/messages/message-{0}.txt", messageId.ToString());
		}
		#endregion
	}
}
