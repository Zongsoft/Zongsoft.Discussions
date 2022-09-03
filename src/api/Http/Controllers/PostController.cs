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
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Zongsoft.Web;
using Zongsoft.Data;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Area("Community")]
	[Route("[area]/Posts")]
	public class PostController : ApiControllerBase<Post, PostService>
	{
		#region 构造函数
		public PostController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[HttpPost("{id}/Upvote/{value?}")]
		public IActionResult Upvote(ulong id, byte value = 1)
		{
			return this.DataService.Upvote(id, value) ? this.NoContent() : this.NotFound();
		}

		[HttpPost("{id}/Downvote/{value?}")]
		public IActionResult Downvote(ulong id, byte value = 1)
		{
			return this.DataService.Downvote(id, value) ? this.NoContent() : this.NotFound();
		}

		[HttpGet("{id}/Upvotes")]
		public IEnumerable<Post.PostVoting> GetUpvotes(ulong id, [FromQuery]Paging page = null)
		{
			return this.DataService.GetUpvotes(id, page);
		}

		[HttpGet("{id}/Downvotes")]
		public IEnumerable<Post.PostVoting> GetDownvotes(ulong id, [FromQuery] Paging page = null)
		{
			return this.DataService.GetDownvotes(id, page);
		}

		[HttpGet("{id}/Comments")]
		public IEnumerable<Post> GetComments(ulong id, [FromQuery] Paging page = null)
		{
			return this.DataService.GetComments(id, page);
		}
		#endregion
	}
}
