/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
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
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	public class ForumController : Zongsoft.Web.Http.HttpControllerBase<Forum, ForumConditional, ForumService>
	{
		#region 构造函数
		public ForumController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[ActionName("Moderators")]
		public IEnumerable<UserProfile> GetModerators(ushort id)
		{
			return this.DataService.GetModerators(id, this.GetSchema());
		}

		[ActionName("Globals")]
		public IEnumerable<Thread> GetGlobalThreads(ushort id, [FromUri]Paging paging = null)
		{
			return this.DataService.GetGlobalThreads(id, this.GetSchema(), paging);
		}

		[ActionName("Pinneds")]
		public IEnumerable<Thread> GetPinnedThreads(ushort id, [FromUri]Paging paging = null)
		{
			return this.DataService.GetPinnedThreads(id, this.GetSchema(), paging);
		}

		[ActionName("Topmosts")]
		public IEnumerable<Thread> GetTopmosts(ushort id, [FromRoute("args")]int count = 0)
		{
			return this.DataService.GetTopmosts(id, this.GetSchema(), count);
		}

		[ActionName("Threads")]
		public object GetThreads(ushort id, [FromUri]Paging paging = null)
		{
			return this.GetResult(this.DataService.GetThreads(id, this.GetSchema(), paging));
		}
		#endregion
	}
}
