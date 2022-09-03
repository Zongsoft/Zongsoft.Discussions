﻿/*
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
using System.Net;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Zongsoft.Web;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization]
	[Area("Community")]
	[Route("[area]/Folders")]
	public class FolderController : ApiControllerBase<Folder, FolderService>
	{
		#region 构造函数
		public FolderController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[HttpPatch("{id}/Icon/{value}")]
		public IActionResult SetIcon(uint id, string value = null)
		{
			return this.DataService.SetIcon(id, value) ? this.NoContent() : this.NotFound();
		}

		[HttpPatch("{id}/Visiblity/{value}")]
		public IActionResult SetVisiblity(uint id, Visibility value)
		{
			return this.DataService.SetVisiblity(id, value) ? this.NoContent() : this.NotFound();
		}

		[HttpPatch("{id}/Accessibility/{value}")]
		public IActionResult SetAccessibility(uint id, Accessibility value)
		{
			return this.DataService.SetAccessibility(id, value) ? this.NoContent() : this.NotFound();
		}
		#endregion
	}
}
