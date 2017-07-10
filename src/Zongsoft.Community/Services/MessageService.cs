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

			if(message.ContentKind == ContentKind.File)
				message.Content = Utility.ReadTextFile(message.Content);

			//获取当前用户的凭证
			var credential = this.EnsureCredential(false);

			if(credential != null && credential.CredentialId != null && credential.CredentialId.Length > 0)
			{
				//更新当前用户对该消息的读取状态
				this.DataAccess.Update(this.DataAccess.Mapper.Get<Message.MessageMember>(), new
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

		protected override int OnInsert(DataDictionary<Message> data, string scope)
		{
			var count = base.OnInsert(data, scope);

			if(count < 1)
				return count;

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

			return count;
		}
		#endregion
	}
}
