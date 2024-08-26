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
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Zongsoft.Web;
using Zongsoft.Data;
using Zongsoft.Web.Http;
using Zongsoft.Security;
using Zongsoft.Security.Membership;
using Zongsoft.Discussions.Models;
using Zongsoft.Discussions.Services;

namespace Zongsoft.Discussions.Web.Controllers
{
	[ControllerName("Threads")]
	public class ThreadController : ServiceController<Thread, ThreadService>
	{
		#region 公共方法
		[HttpPatch("{id}/Approve")]
		[Authorization]
		public object Approve(ulong id)
		{
			return this.DataService.Approve(id) ? this.NoContent() : this.NotFound();
		}

		[HttpPatch("{id}/Hidden")]
		[Authorization]
		public object Hidden(ulong id)
		{
			return this.DataService.Visible(id, false) ? this.NoContent() : this.NotFound();
		}

		[HttpPatch("{id}/Visible")]
		[Authorization]
		public object Visible(ulong id)
		{
			return this.DataService.Visible(id, true) ? this.NoContent() : this.NotFound();
		}

		[HttpPost("{id}/Lock")]
		[Authorization]
		public object Lock(ulong id)
		{
			return this.DataService.SetLocked(id, true) ? this.NoContent() : this.NotFound();
		}

		[HttpDelete("{id}/Locked")]
		[HttpPost("{id}/Unlock")]
		[Authorization]
		public object Unlock(ulong id)
		{
			return this.DataService.SetLocked(id, false) ? this.NoContent() : this.NotFound();
		}

		[HttpPost, HttpPut]
		[ActionName("Pinned")]
		[Authorization]
		public object Pin(ulong id)
		{
			return this.DataService.SetPinned(id, true) ? this.NoContent() : this.NotFound();
		}

		[HttpDelete]
		[ActionName("Pinned")]
		[Authorization]
		public object Unpin(ulong id)
		{
			return this.DataService.SetPinned(id, false) ? this.NoContent() : this.NotFound();
		}

		[HttpPost, HttpPut]
		[ActionName("Valued")]
		[Authorization]
		public object Valued(ulong id)
		{
			return this.DataService.SetValued(id, true) ? this.NoContent() : this.NotFound();
		}

		[HttpDelete]
		[ActionName("Valued")]
		[Authorization]
		public object Unvalued(ulong id)
		{
			return this.DataService.SetValued(id, false) ? this.NoContent() : this.NotFound();
		}

		[HttpPost, HttpPut]
		[ActionName("Global")]
		[Authorization]
		public object Global(ulong id)
		{
			return this.DataService.SetGlobal(id, true) ? this.NoContent() : this.NotFound();
		}

		[HttpDelete]
		[ActionName("Global")]
		[Authorization]
		public object Unglobal(ulong id)
		{
			return this.DataService.SetGlobal(id, false) ? this.NoContent() : this.NotFound();
		}

		[HttpPatch, HttpPut]
		[ActionName("Title")]
		public object SetTitle(ulong id, [FromBody] string value)
		{
			return this.DataService.Update(new
			{
				ThreadId = id,
				Title = value,
			}) > 0 ? this.NoContent() : this.NotFound();
		}

		[HttpPatch, HttpPut]
		[ActionName("Summary")]
		public object SetSummary(ulong id, [FromBody] string value)
		{
			return this.DataService.Update(new
			{
				ThreadId = id,
				Summary = value,
			}) > 0 ? this.NoContent() : this.NotFound();
		}

		[HttpPatch, HttpPut]
		[ActionName("Content")]
		public object SetContent(ulong id, [FromBody] string content)
		{
			return this.DataService.Update(new
			{
				ThreadId = id,
				Post = new
				{
					Content = content,
					this.Request.ContentType,
				}
			}) > 0 ? this.NoContent() : this.NotFound();
		}

		[ActionName("Posts")]
		public object GetPosts(ulong id, [FromQuery]Paging page = null)
		{
			return this.Paginate(page ??= Paging.First(), this.DataService.GetPosts(id, this.Request.Headers.GetDataSchema(), page));
		}
		#endregion
	}
}
