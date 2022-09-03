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
using System.Security.Claims;

using Zongsoft.Data;
using Zongsoft.Common;
using Zongsoft.Services;
using Zongsoft.Security;
using Zongsoft.Community.Models;
using System.Linq;

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
		public OperationResult Challenge(ClaimsPrincipal principal, string scenario)
		{
			var userId = principal.Identity.GetIdentifier<uint>();

			if(userId == 0)
				return OperationResult.Fail(SecurityReasons.InvalidIdentity);

			//获取当前登录用户所对应的用户对象
			var user = this.GetUser(userId);

			//如果对应的用户对象查找失败则返回
			if(user == null)
				return OperationResult.Fail(SecurityReasons.Forbidden);

			//如果身份校验失败，则更新返回的验证结果并退出
			var result = this.OnVerify(user, scenario);

			if(result.Failed)
				return result.Failure;

			//更新当前用户的声明属性
			this.SetClaims(principal.Identity as ClaimsIdentity, user);

			return OperationResult.Success();
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
		protected virtual OperationResult OnVerify(UserProfile user, string scenario) => OperationResult.Success();
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
