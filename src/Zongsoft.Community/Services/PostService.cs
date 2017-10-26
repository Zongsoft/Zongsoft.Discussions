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
	[DataSequence("PostId", 100000)]
	[DataSearchKey("Thread,ThreadId:ThreadId")]
	public class PostService : ServiceBase<Post>
	{
		#region 构造函数
		public PostService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public bool Upvote(ulong postId, byte value = 1)
		{
			var credential = this.EnsureCredential();

			if(value == 0)
				value = 1;

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				//递增指定编号的帖子的点赞总数，如果递增失败则返回
				if(this.DataAccess.Increment<Post>("TotalUpvotes", Condition.Equal("PostId", postId)) < 0)
					return false;

				this.DataAccess.Delete<Post.PostVote>(Condition.Equal("PostId", postId) & Condition.Equal("UserId", credential.UserId));
				var count = this.DataAccess.Insert(new Post.PostVote(postId, credential.UserId, (sbyte)Math.Min(value, (byte)100)));

				//提交事务
				transaction.Commit();

				//返回是否成功
				return count > 0;
			}
		}

		public bool Downvote(ulong postId, byte value = 1)
		{
			var credential = this.EnsureCredential();

			if(value == 0)
				value = 1;

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				//递增指定编号的帖子的被踩总数，如果递增失败则返回
				if(this.DataAccess.Increment<Post>("TotalDownvotes", Condition.Equal("PostId", postId)) < 0)
					return false;

				this.DataAccess.Delete<Post.PostVote>(Condition.Equal("PostId", postId) & Condition.Equal("UserId", credential.UserId));
				var count = this.DataAccess.Insert(new Post.PostVote(postId, credential.UserId, (sbyte)-Math.Min(value, (byte)100)));

				//提交事务
				transaction.Commit();

				//返回是否成功
				return count > 0;
			}
		}

		public IEnumerable<Post> GetChildren(ulong postId, Paging paging = null)
		{
			return this.Select(Condition.Equal("ParentId", postId), paging);
		}
		#endregion

		#region 重写方法
		protected override Post OnGet(ICondition condition, string scope)
		{
			//调用基类同名方法
			var post = base.OnGet(condition, scope);

			if(post == null)
				return null;

			//如果内容类型是外部文件（即非嵌入格式），则读取文件内容
			if(!Utility.IsContentEmbedded(post.ContentType))
				post.Content = Utility.ReadTextFile(post.Content);

			return post;
		}

		protected override IEnumerable<Post> OnSelect(ICondition condition, string scope, Paging paging, params Sorting[] sortings)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			return base.OnSelect(condition, scope, paging, sortings);
		}

		protected override int OnInsert(DataDictionary<Post> data, string scope)
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
				filePath = this.GetContentFilePath(data.Get(p => p.PostId), data.Get(p => p.ContentType));

				//将内容文本写入到文件中
				Utility.WriteTextFile(filePath, value);

				//更新内容文件的存储路径
				data.Set(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.Set(p => p.ContentType, Utility.GetContentType(data.Get(p => p.ContentType), false));
			});

			try
			{
				//调用基类同名方法
				var count = base.OnInsert(data, scope);

				if(count > 0)
				{
					//更新发帖人的关联帖子统计信息
					this.SetUserMostRecentPost(data);
				}
				else
				{
					//如果新增记录失败则删除刚创建的文件
					if(filePath != null && filePath.Length > 0)
						Utility.DeleteFile(filePath);
				}

				return count;
			}
			catch
			{
				//删除新建的文件
				if(filePath != null && filePath.Length > 0)
					Utility.DeleteFile(filePath);

				throw;
			}
		}

		protected override int OnUpdate(DataDictionary<Post> data, ICondition condition, string scope)
		{
			//更新内容到文本文件中
			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//根据当前反馈编号，获得其对应的内容文件存储路径
				var filePath = this.GetContentFilePath(data.Get(p => p.PostId), data.Get(p => p.ContentType));

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
		protected virtual string GetContentFilePath(ulong postId, string contentType)
		{
			return string.Format("/posts/post-{0}.txt", postId.ToString());
		}
		#endregion

		#region 私有方法
		private bool SetUserMostRecentPost(DataDictionary<Post> data)
		{
			if(data == null || data.Get(p => p.IsThread, false))
				return false;

			if(this.DataAccess.Increment<UserProfile>("TotalPosts", Condition.Equal("UserId", data.Get(p => p.CreatorId))) < 0)
				return false;

			return this.DataAccess.Update(this.DataAccess.Naming.Get<UserProfile>(), new
			{
				UserId = data.Get(p => p.CreatorId),
				MostRecentPostId = data.Get(p => p.PostId),
				MostRecentPostTime = data.Get(p => p.CreatedTime),
			}) > 0;
		}
		#endregion
	}
}
