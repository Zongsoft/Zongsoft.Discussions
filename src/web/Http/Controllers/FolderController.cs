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
using System.Net;
using System.Web.Http;

using Zongsoft.Data;
using Zongsoft.Web.Http;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization(AuthorizationMode.Identity)]
	public class FolderController : Zongsoft.Web.Http.HttpControllerBase<Folder, FolderConditional, FolderService>
	{
		#region 构造函数
		public FolderController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[ActionName("Users")]
		public object GetUsers(uint id, [FromRoute("args")]UserKind? kind = null, [FromUri]Paging paging = null)
		{
			return this.GetResult(this.DataService.GetUsers(id, kind, paging));
		}

		[HttpPatch]
		[ActionName("Icon")]
		public void SetIcon(uint id, [FromRoute("args")]string icon = null)
		{
			if(!this.DataService.SetIcon(id, icon))
				throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		[HttpPatch]
		[ActionName("Visiblity")]
		public void SetVisiblity(uint id, [FromRoute("args")]Visiblity visiblity)
		{
			if(!this.DataService.SetVisiblity(id, visiblity))
				throw new HttpResponseException(HttpStatusCode.NotFound);
		}

		[HttpPatch]
		[ActionName("Accessibility")]
		public void SetAccessibility(uint id, Accessibility accessibility)
		{
			if(!this.DataService.SetAccessibility(id, accessibility))
				throw new HttpResponseException(HttpStatusCode.NotFound);
		}
		#endregion
	}
}
