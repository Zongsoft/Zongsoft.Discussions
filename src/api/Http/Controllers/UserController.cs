﻿/*
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
using System.Web.Http;
using System.Threading.Tasks;

using Zongsoft.Data;
using Zongsoft.Web.Http;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization(AuthorizationMode.Requires)]
	public class UserController : Zongsoft.Web.Http.HttpControllerBase<UserProfile, UserProfileConditional, UserService>
	{
		#region 构造函数
		public UserController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[HttpPatch]
		[ActionName("Status")]
		public void SetStatus(uint id, [FromRoute("args")]UserStatus status)
		{
			if(!this.DataService.SetStatus(id, status))
				throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
		}

		[ActionName("Histories")]
		public IEnumerable<IHistory> GetHistories(uint id, [FromRoute("args")]Paging paging = null)
		{
			return this.DataService.GetHistories(id, paging);
		}

		[ActionName("Statistics")]
		public object GetStatistics(uint id, [FromRoute("args")]string kind = null)
		{
			if(string.IsNullOrWhiteSpace(kind))
				throw HttpResponseExceptionUtility.BadRequest("Missing kind of the statistics.");

			switch(kind.ToLowerInvariant())
			{
				case "message":
					return null;
					//return this.DataService.GetMessageStatistics(id);
				default:
					throw HttpResponseExceptionUtility.BadRequest("Invalid kind of the statistics.");
			}
		}

		[ActionName("Count")]
		public int GetCount(uint id, string args)
		{
			if(string.IsNullOrEmpty(args))
				throw HttpResponseExceptionUtility.BadRequest("Missing arguments of the request.");

			switch(args.ToLowerInvariant())
			{
				case "unread":
				case "message-unread":
					return this.DataService.GetMessageUnreadCount(id);
				case "message":
				case "message-total":
					return this.DataService.GetMessageTotalCount(id);
				default:
					throw HttpResponseExceptionUtility.BadRequest("Invalid argument value.");
			}
		}

		[ActionName("Messages")]
		public object GetMessages(uint id, [FromUri]bool? isRead = null, [FromUri]Paging paging = null)
		{
			return this.GetResult(this.DataService.GetMessages(id, isRead, paging));
		}

		[HttpPatch, HttpPost]
		[ActionName("Avatar")]
		public Task<Zongsoft.IO.FileInfo> SetAvatar(uint id, string args)
		{
			return this.Upload(this.DataService.GetFilePath(id, "avatar"));
		}

		[HttpPatch, HttpPost]
		[ActionName("Photo")]
		public Task<Zongsoft.IO.FileInfo> SetPhoto(uint id, string args)
		{
			return this.Upload(this.DataService.GetFilePath(id, "photo"));
		}
		#endregion
	}
}
