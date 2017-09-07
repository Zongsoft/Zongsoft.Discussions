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
using System.Web.Http;

using Zongsoft.Data;
using Zongsoft.Web.Http;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization(AuthorizationMode.Requires)]
	public class ForumController : Zongsoft.Web.Http.HttpControllerBase<Forum, ForumConditional, ForumService>
	{
		#region 构造函数
		public ForumController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[ActionName("Moderators")]
		public IEnumerable<UserProfile> GetModerators([FromRoute("id")]uint siteId, [FromRoute("id")]ushort forumId)
		{
			return this.DataService.GetModerators(siteId, forumId);
		}

		[ActionName("Globals")]
		public IEnumerable<Thread> GetGlobalThreads(uint id)
		{
			return this.DataService.GetGlobalThreads(id);
		}

		[ActionName("Pinneds")]
		public IEnumerable<Thread> GetPinnedThreads([FromRoute("id")]uint siteId, [FromRoute("id")]ushort forumId)
		{
			return this.DataService.GetPinnedThreads(siteId, forumId);
		}

		[ActionName("Topmosts")]
		public IEnumerable<Thread> GetTopmosts([FromRoute("id")]uint siteId, [FromRoute("id")]ushort forumId)
		{
			return this.DataService.GetTopmosts(siteId, forumId);
		}

		[ActionName("Threads")]
		[HttpPaging]
		public IEnumerable<Thread> GetThreads([FromRoute("id")]uint siteId, [FromRoute("id")]ushort forumId, [FromUri]Paging paging = null)
		{
			return this.DataService.GetThreads(siteId, forumId, paging);
		}
		#endregion
	}
}
