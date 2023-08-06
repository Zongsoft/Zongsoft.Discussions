/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@qq.com>
 * 
 * Copyright (C) 2015-2023 Zongsoft Corporation. All rights reserved.
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
using System.Security.Claims;

using Zongsoft.Security;

using Zongsoft.Community.Models;

namespace Zongsoft.Community.Security
{
	public class UserIdentityTransformer : IClaimsIdentityTransformer
	{
		#region 公共方法
		public bool CanTransform(ClaimsIdentity identity) => string.Equals(identity.AuthenticationType, "Zongsoft.Community", StringComparison.OrdinalIgnoreCase);
		public object Transform(ClaimsIdentity identity) => identity.AsModel<UserProfile>(this.OnTransform);
		#endregion

		#region 虚拟方法
		protected virtual bool OnTransform(UserProfile user, Claim claim)
		{
			switch(claim.Type)
			{
				case nameof(UserProfile.SiteId):
					user.SiteId = (claim.Value != null && uint.TryParse(claim.Value, out var siteId)) ? siteId : 0;
					return true;
				case nameof(UserProfile.Gender):
					user.Gender = (claim.Value != null && Enum.TryParse<Gender>(claim.Value, out var gender)) ? gender : Gender.None;
					return true;
				case nameof(UserProfile.Avatar):
					user.Avatar = claim.Value;
					return true;
				case nameof(UserProfile.Grade):
					user.Grade = (claim.Value != null && byte.TryParse(claim.Value, out var grade)) ? grade : (byte)0;
					return true;
				case nameof(UserProfile.TotalPosts):
					user.TotalPosts = (claim.Value != null && uint.TryParse(claim.Value, out var totalPosts)) ? totalPosts : 0;
					return true;
				case nameof(UserProfile.TotalThreads):
					user.TotalThreads = (claim.Value != null && uint.TryParse(claim.Value, out var totalThreads)) ? totalThreads : 0;
					return true;
				default:
					return false;
			}
		}
		#endregion
	}
}