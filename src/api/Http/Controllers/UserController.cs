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
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Zongsoft.Web;
using Zongsoft.Data;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization]
	[Area("Community")]
	[Route("[area]/Users")]
	public class UserController : ApiControllerBase<UserProfile, UserService>
	{
		#region 构造函数
		public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		[HttpGet("{id}/Histories")]
		public IEnumerable<History> GetHistories(uint id, [FromQuery]Paging page = null)
		{
			return this.DataService.GetHistories(id, page);
		}

		[HttpGet("{id}/Statistics/{kind}")]
		public object GetStatistics(uint id, string kind = null)
		{
			if(string.IsNullOrWhiteSpace(kind))
				return this.BadRequest();

			switch(kind.ToLowerInvariant())
			{
				case "message":
					return null;
					//return this.DataService.GetMessageStatistics(id);
				default:
					return this.BadRequest("Invalid kind of the statistics.");
			}
		}

		[HttpGet("{id}/Count/{args}")]
		public IActionResult GetCount(uint id, string args)
		{
			if(string.IsNullOrEmpty(args))
				return this.BadRequest("Missing arguments of the request.");

			switch(args.ToLowerInvariant())
			{
				case "unread":
				case "message-unread":
					return this.Ok(this.DataService.GetMessageUnreadCount(id));
				case "message":
				case "message-total":
					return this.Ok(this.DataService.GetMessageTotalCount(id));
				default:
					return this.BadRequest("Invalid argument value.");
			}
		}

		[HttpGet("{id}/Messages")]
		public object GetMessages(uint id, [FromQuery] bool? isRead = null, [FromQuery] Paging page = null)
		{
			return this.Paginate(this.DataService.GetMessages(id, isRead, page));
		}

		[HttpPost("{id}/Avatar")]
		public Task<Zongsoft.IO.FileInfo> SetAvatar(uint id)
		{
			return this.SetAvatar(id);
		}

		[HttpPost("{id}/Photo")]
		public Task<Zongsoft.IO.FileInfo> SetPhoto(uint id)
		{
			return this.SetPhoto(id);
		}
		#endregion
	}
}
