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
	[DataSequence("SiteId, ForumId", 101)]
	[DataSearchKey("Key:Name")]
	public class ForumService : ServiceBase<Forum>
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
			{
				var credential = this.EnsureCredential(false);

				if(credential == null)
					return false;

				userId = credential.UserId;
			}

			return this.GetModerators(siteId, forumId).Any(p => p.UserId == userId);
		}

		public ICollection<UserProfile> GetModerators(uint siteId, ushort forumId)
		{
			return this.DataAccess.Select<Moderator>(Condition.Equal("SiteId", siteId) & Condition.Equal("ForumId", forumId), "User, User.User", Paging.Disable).Select(p => p.User).ToArray();
		}

		public IEnumerable<Thread> GetThreads(uint siteId, ushort forumId, Paging paging = null)
		{
			//获取必须的主题服务
			var service = this.ServiceProvider.ResolveRequired<ThreadService>();

			//获取指定论坛中最顶部的主题集（全局贴+本论坛的置顶贴）
			var topmosts = service.GetTopmosts(siteId, forumId);

			//查询指定论坛中的并且排除顶部集中的主题集
			return service.Select(Condition.Equal("SiteId", siteId) & Condition.Equal("ForumId", forumId) & Condition.NotIn("ThreadId", topmosts.Select(p => p.ThreadId)), paging);
		}
		#endregion

		#region 重写方法
		protected override ICondition GetKey(object[] values, out bool singleton)
		{
			if(values.Length == 1)
			{
				singleton = false;
				return Condition.Equal("SiteId", values[0]);
			}

			return base.GetKey(values, out singleton);
		}

		protected override Forum OnGet(ICondition condition, string scope)
		{
			//调用基类同名方法
			var forum = base.OnGet(condition, scope);

			if(forum == null)
				return null;

			//获取当前论坛的版主集
			forum.Moderators = this.GetModerators(forum.SiteId, forum.ForumId);

			return forum;
		}
		#endregion
	}
}
