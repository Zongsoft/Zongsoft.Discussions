/*
 *    ____                                  __
 *   / __ \__  ________   _____  ____  ____/ /
 *  / / / / / / / ___/ | / / _ \/ __ \/ __  / 
 * / /_/ / /_/ / /   | |/ /  __/ / / / /_/ /  
 * \____/\__/\/_/    |___/\___/_/ /_/\__/_/   
 *                                            
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@qq.cn>
 *
 * Copyright (c) 2020-2021 Hunan Yunshu Technology Co.,Ltd. All rights reserved.
 */

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

using Zongsoft.Data;
using Zongsoft.Common;
using Zongsoft.Services;
using Zongsoft.Security;
using Zongsoft.Security.Membership;

using Zongsoft.Community.Models;

namespace Zongsoft.Community.Security
{
	public abstract class UserChallenger : IChallenger
	{
		#region 构造函数
		protected UserChallenger(IServiceProvider services)
		{
			this.DataAccess = services.GetDataAccess() ?? throw new InvalidOperationException("Missing the required data access service.");
		}
		#endregion

		#region 保护属性
		protected IDataAccess DataAccess { get; }
		#endregion

		#region 公共方法
		public object Challenge(ClaimsPrincipal principal, string scenario)
		{
			var userId = principal.Identity.GetIdentifier<uint>();

			if(userId == 0)
				throw new AuthenticationException(SecurityReasons.InvalidIdentity);

			//获取当前登录用户所对应的用户对象
			var user = this.GetUser(userId) ?? throw new AuthenticationException(SecurityReasons.Forbidden);

			//执行身份校验
			this.OnVerify(user, scenario);

			//更新当前用户的声明属性
			this.SetClaims(principal.Identity as ClaimsIdentity, user);

			return null;
		}
		#endregion

		#region 抽象方法
		protected virtual UserProfile GetUser(uint userId)
		{
			return this.DataAccess.Select<UserProfile>(
				Condition.Equal(nameof(UserProfile.UserId), userId),
				Paging.Limit(1)
			).FirstOrDefault();
		}

		protected virtual void OnClaims(ClaimsIdentity identity, UserProfile user) { }
		protected virtual void OnVerify(UserProfile user, string scenario) { }
		#endregion

		#region 私有方法
		private void SetClaims(ClaimsIdentity identity, UserProfile user)
		{
			if(identity == null || user == null)
				return;

			//更新授权身份所属的命名空间
			identity.SetNamespace(user.Namespace);

			identity.AddClaim(nameof(UserProfile.SiteId), user.SiteId.ToString(), ClaimValueTypes.String);

			this.OnClaims(identity, user);
		}
		#endregion
	}
}
