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
using System.Web.Http;
using System.Threading.Tasks;

using Zongsoft.Web.Http;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Wayto.Common.Web.Http.Controllers
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
		[HttpPost]
		public async Task<Zongsoft.IO.FileInfo> Upload(uint id, string args)
		{
			if(string.IsNullOrWhiteSpace(args))
				throw HttpResponseExceptionUtility.BadRequest("Missing the args.");

			if(!string.Equals(args, "avatar", StringComparison.OrdinalIgnoreCase) && !string.Equals(args, "photo", StringComparison.OrdinalIgnoreCase))
				throw HttpResponseExceptionUtility.BadRequest($"Invalid '{args}' value of the argument.");

			var path = Zongsoft.IO.Path.Parse(this.DataService.GetFilePath(id, args));
			var accessor = new Zongsoft.Web.WebFileAccessor();
			var info = (await accessor.Write(this.Request, path.DirectoryUrl, e => e.FileName = path.FileName)).FirstOrDefault();

			if(info != null)
			{
				if(string.Equals(args, "avatar", StringComparison.OrdinalIgnoreCase))
					this.DataService.SetAvatar(id, info.Path.Url);
				else if(string.Equals(args, "photo", StringComparison.OrdinalIgnoreCase))
					this.DataService.SetPhotoPath(id, info.Path.Url);

				info.Url = info.Url + "?" + Zongsoft.Common.RandomGenerator.GenerateString();
			}

			return info;
		}
		#endregion

		#region 重写方法
		public override void Patch(string id, IDictionary<string, object> data)
		{
			uint userId;

			if(!uint.TryParse(id, out userId))
				throw HttpResponseExceptionUtility.BadRequest($"The '{id}' id value must be a integer.");

			if(data.TryGetValue("Status", out var status) && status != null)
			{
				if(!this.DataService.SetStatus(userId, Zongsoft.Common.Convert.ConvertValue(status, UserStatus.Active)))
					throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
			}
		}
		#endregion
	}
}
