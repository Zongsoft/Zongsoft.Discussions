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
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

using Zongsoft.Data;
using Zongsoft.Services;
using Zongsoft.Security;
using Zongsoft.Security.Membership;

using Zongsoft.Community.Models;

namespace Zongsoft.Community.Security
{
	public class UserChallenger : IChallenger
	{
		#region 单例字段
		public static readonly UserChallenger Instance = new();
		#endregion

		#region 私有构造
		private UserChallenger() { }
		#endregion

		#region 公共方法
		public void Challenge(ClaimsPrincipal principal, string scenario)
		{
			if(principal.Identity == null)
				throw new AuthenticationException(SecurityReasons.InvalidIdentity);

			var userId = principal.Identity.GetIdentifier<uint>();

			if(userId == 0)
				throw new AuthenticationException(SecurityReasons.InvalidIdentity);

			//获取当前主身份所对应的用户信息，如果没有对应的用户则创建一条对应的新用户信息a
			var user = this.GetUser(userId) ?? this.CreateUser(principal.Identity);

			if(user == null)
				throw new AuthenticationException(SecurityReasons.InvalidIdentity);

			//设置用户的命名空间
			if(string.IsNullOrEmpty(((IUserIdentity)user).Namespace))
				((IUserIdentity)user).Namespace = principal.Identity.GetNamespace();

			//执行身份校验
			this.OnVerify(user, scenario);

			//构建新的用户身份
			var identity = this.Identity(user);

			//将新的身份加入到主体中
			principal.AddIdentity(identity);
		}
		#endregion

		#region 虚拟方法
		protected virtual UserProfile GetUser(uint userId) =>
			Module.Current.Accessor.Select<UserProfile>(Condition.Equal(nameof(UserProfile.UserId), userId), Paging.Limit(1)).FirstOrDefault();

		protected virtual UserProfile CreateUser(IIdentity identity)
		{
			var user = identity.AsModel<UserProfile>();

			if(user.SiteId == 0)
			{
				var @namespace = identity.GetNamespace();

				if(!string.IsNullOrEmpty(@namespace))
				{
					if(uint.TryParse(@namespace, out var id))
						user.SiteId = id;
					else
					{
						var site = Module.Current.Accessor.Select<Site>(
							Condition.Equal(nameof(Site.SiteNo), @namespace),
							$"{nameof(Site.SiteId)},{nameof(Site.SiteNo)}",
							Paging.Limit(1)).FirstOrDefault();

						if(site != null)
							user.SiteId = site.SiteId;
					}
				}
			}

			return Module.Current.Accessor.Upsert(user) > 0 ? user : null;
		}

		protected virtual void OnClaims(ClaimsIdentity identity, UserProfile user) { }
		protected virtual void OnVerify(UserProfile user, string scenario) { }
		#endregion

		#region 私有方法
		private ClaimsIdentity Identity(UserProfile user)
		{
			var identity = user.Identity(UserIdentity.Scheme, "Zongsoft");

			identity.AddClaim(nameof(UserProfile.SiteId), user.SiteId.ToString(), ClaimValueTypes.String);
			identity.AddClaim(nameof(UserProfile.Gender), user.Gender.ToString(), ClaimValueTypes.String);
			identity.AddClaim(nameof(UserProfile.Avatar), user.Avatar, ClaimValueTypes.String);
			identity.AddClaim(nameof(UserProfile.Grade), user.Grade.ToString(), ClaimValueTypes.Integer);
			identity.AddClaim(nameof(UserProfile.TotalPosts), user.TotalPosts.ToString(), ClaimValueTypes.UInteger32);
			identity.AddClaim(nameof(UserProfile.TotalThreads), user.TotalThreads.ToString(), ClaimValueTypes.UInteger32);

			//进行其他声明定义
			this.OnClaims(identity, user);

			//返回新构建的身份
			return identity;
		}
		#endregion
	}
}
