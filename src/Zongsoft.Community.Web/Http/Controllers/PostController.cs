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
	public class PostController : Zongsoft.Web.Http.HttpControllerBase<Post, PostConditional, PostService>
	{
		#region 构造函数
		public PostController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[HttpPost]
		public void Upvote(ulong id, [FromRoute("args")]byte value = 1)
		{
			if(!this.DataService.Upvote(id, value))
				throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
		}

		[HttpPost]
		public void Downvote(ulong id, [FromRoute("args")]byte value = 1)
		{
			if(!this.DataService.Downvote(id, value))
				throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
		}

		[ActionName("Upvotes")]
		public IEnumerable<Post.PostVoting> GetUpvotes(ulong id, [FromUri]Paging paging = null)
		{
			return this.DataService.GetUpvotes(id, paging);
		}

		[ActionName("Downvotes")]
		public IEnumerable<Post.PostVoting> GetDownvotes(ulong id, [FromUri]Paging paging = null)
		{
			return this.DataService.GetDownvotes(id, paging);
		}

		[ActionName("Comments")]
		public IEnumerable<Post.PostComment> GetComments(ulong id, [FromUri]Paging paging = null)
		{
			return this.DataService.GetComments(id, paging);
		}
		#endregion
	}
}
