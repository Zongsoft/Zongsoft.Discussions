/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@qq.com>
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
	[DataSearcher("Name")]
	public class ForumService : DataService<Forum>
	{
		#region 构造函数
		public ForumService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public bool IsModerator(uint siteId, ushort forumId, uint? userId = null)
		{
			if(userId == null)
				userId = this.User.UserId;

			return this.DataAccess.Exists<Forum.ForumUser>(
				Condition.Equal(nameof(Forum.ForumUser.SiteId), siteId) & Condition.Equal(nameof(Forum.ForumUser.ForumId), forumId) &
				Condition.Equal(nameof(Forum.ForumUser.UserId), userId) & Condition.Equal(nameof(Forum.ForumUser.IsModerator), true));
		}

		public IEnumerable<UserProfile> GetModerators(uint siteId, ushort forumId)
		{
			return this.DataAccess.Select<UserProfile>(nameof(Forum.ForumUser),
				Condition.Equal(nameof(Forum.ForumUser.SiteId), siteId) &
				Condition.Equal(nameof(Forum.ForumUser.ForumId), forumId) &
				Condition.Equal(nameof(Forum.ForumUser.IsModerator), true),
				"User{*}");
		}

		public IEnumerable<Thread> GetGlobalThreads(uint siteId, Paging paging = null)
		{
			return this.DataAccess.Select<Thread>(
				Condition.Equal(nameof(Thread.SiteId), siteId) &
				Condition.Equal(nameof(Thread.IsGlobal), true) &
				Condition.Equal(nameof(Thread.Visible), true) &
				Condition.Equal(nameof(Thread.Disabled), false),
				paging, Sorting.Descending(nameof(Thread.ThreadId)));
		}

		public IEnumerable<Thread> GetPinnedThreads(uint siteId, ushort forumId, Paging paging = null)
		{
			return this.DataAccess.Select<Thread>(
				Condition.Equal(nameof(Thread.SiteId), siteId) &
				Condition.Equal(nameof(Thread.ForumId), forumId) &
				Condition.Equal(nameof(Thread.IsPinned), true) &
				Condition.Equal(nameof(Thread.Visible), true) &
				Condition.Equal(nameof(Thread.Disabled), false),
				paging, Sorting.Descending(nameof(Thread.ThreadId)));
		}

		public Thread[] GetTopmosts(uint siteId, ushort forumId, int count = 10)
		{
			count = Math.Max(5, Math.Min(50, count));

			var globals = this.GetGlobalThreads(siteId, Paging.Page(1, count));
			var pinneds = this.GetPinnedThreads(siteId, forumId, Paging.Page(1, count));

			return globals.Union(pinneds).OrderByDescending(t => t.ThreadId).Take(count).ToArray();
		}

		public IEnumerable<Thread> GetThreads(uint siteId, ushort forumId, Paging paging = null)
		{
			var criteria = Condition.Equal(nameof(Thread.SiteId), siteId) &
				Condition.Equal(nameof(Thread.ForumId), forumId) &
				Condition.Equal(nameof(Thread.Visible), true) &
				Condition.Equal(nameof(Thread.Disabled), false);

			//只有第一页或者不分页才需要加载最顶部主题集
			if(paging == null || paging.PageIndex == 1)
			{
				//获取指定论坛中最顶部的主题集（全局贴+本论坛的置顶贴）
				var topmosts = this.GetTopmosts(siteId, forumId);

				if(topmosts != null && topmosts.Length > 0)
					criteria.Add(Condition.NotIn(nameof(Thread.ThreadId), topmosts.Select(p => p.ThreadId)));
			}

			return this.DataAccess.Select<Thread>(criteria, paging);
		}
		#endregion

		#region 重写方法
		protected override ICondition OnValidate(Method method, ICondition condition)
		{
			//调用基类同名方法
			condition = base.OnValidate(method, condition);

			ICondition requires;

			//匿名用户只能获取公共数据
			if(!this.Principal.Identity.IsAuthenticated)
				requires = Condition.Equal(nameof(Forum.Visibility), Visibility.Public);
			else
				requires = Condition.In(nameof(Forum.Visibility), (byte)Visibility.Internal, (byte)Visibility.Public) |
					(
						Condition.Equal(nameof(Forum.Visibility), Visibility.Specified) &
						Condition.Exists(nameof(Forum.Users),
							Condition.Equal(nameof(Forum.ForumUser.UserId), this.User.UserId) &
							(
								Condition.Equal(nameof(Forum.ForumUser.IsModerator), true) |
								Condition.In(nameof(Forum.ForumUser.Permission), (byte)Permission.Read, (byte)Permission.Write)
							)
						)
					);

			if(condition == null)
				return requires;
			else
				return ConditionCollection.And(condition, requires);
		}
		#endregion
	}
}
