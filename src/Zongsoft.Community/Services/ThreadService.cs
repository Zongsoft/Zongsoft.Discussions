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
using Zongsoft.Runtime.Caching;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("ThreadId", 100000)]
	[DataSearchKey("Key:Name")]
	public class ThreadService : ServiceBase<Thread>
	{
		#region 构造函数
		public ThreadService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public IEnumerable<Post> GetPosts(ulong threadId, Paging paging = null)
		{
			var conditions = ConditionCollection.And(
				Condition.Equal("ThreadId", threadId),
				Condition.Equal("ParentId", null) | Condition.Equal("ParentId", 0));

			return this.DataAccess.Select<Post>(conditions, paging, Sorting.Descending("PostId"));
		}
		#endregion

		#region 重写方法
		protected override Thread OnGet(ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Post, Creator, Creator.User";

			//调用基类同名方法
			var thread = base.OnGet(condition, scope);

			if(thread == null)
				return null;

			//如果当前主题是禁用或者未审核的，则需要进行权限判断
			if(thread.Disabled || (!thread.IsApproved))
			{
				//判断当前用户是否为该论坛的版主
				var isModerator = this.ServiceProvider.ResolveRequired<ForumService>().IsModerator(thread.SiteId, thread.ForumId);

				if(!isModerator)
				{
					//当前用户不是版主，并且该主题已被禁用则返回空（相当于主题不存在）
					if(thread.Disabled)
						return null;

					//当前用户不是版主，并且该主题未审核则抛出授权异常
					if(!thread.IsApproved)
						throw new Zongsoft.Security.Membership.AuthorizationException();
				}
			}

			//递增当前主题的累计阅读量
			this.Increment("TotalViews", Condition.Equal("ThreadId", thread.ThreadId));

			//更新当前用户的浏览记录
			this.SetHistory(thread.ThreadId);

			//设置主题对应的帖子内容（如果内容类型是外部文件(即非嵌入格式)，则读取文件内容）
			if(thread.Post != null && !Utility.IsContentEmbedded(thread.Post.ContentType))
				thread.Post.Content = Utility.ReadTextFile(thread.Post.Content);

			//设置主题的回帖集
			thread.Posts = this.GetPosts(thread.ThreadId);

			return thread;
		}

		protected override IEnumerable<Thread> OnSelect(ICondition condition, string scope, Paging paging, params Sorting[] sortings)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			return base.OnSelect(condition, scope, paging, sortings);
		}
		#endregion

		#region 私有方法
		private void SetHistory(ulong threadId)
		{
			var credential = this.EnsureCredential();
			var conditions = Condition.Equal("UserId", credential.UserId) & Condition.Equal("ThreadId", threadId);

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				//递增当前用户对当前主题的累计浏览量
				if(this.DataAccess.Increment<History>("Count", conditions) > 0)
				{
					//更新当前用户对当前主题的最后浏览时间
					this.DataAccess.Update(this.DataAccess.Naming.Get<History>(), new
					{
						MostRecentViewedTime = DateTime.Now,
					}, conditions);
				}
				else
				{
					//尝试新增一条用户的浏览记录
					this.DataAccess.Insert(new History(credential.UserId, threadId));
				}
			}
		}
		#endregion
	}
}
