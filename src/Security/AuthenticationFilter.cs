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

using Zongsoft.Data;
using Zongsoft.Common;
using Zongsoft.Services;
using Zongsoft.Security.Membership;

using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Security
{
	public class AuthenticationFilter : Zongsoft.Common.IExecutionFilter<AuthenticationContext>, Zongsoft.Common.IExecutionFilter
	{
		#region 成员字段
		private IDataAccess _dataAccess;
		#endregion

		#region 构造函数
		public AuthenticationFilter()
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置数据访问服务。
		/// </summary>
		[ServiceDependency(Provider = "Community")]
		public IDataAccess DataAccess
		{
			get
			{
				return _dataAccess;
			}
			set
			{
				_dataAccess = value ?? throw new ArgumentNullException();
			}
		}
		#endregion

		#region 过滤方法
		public void OnFiltered(AuthenticationContext context)
		{
			//如果身份验证失败则退出
			if(!context.IsAuthenticated)
				return;

			//获取当前登录用户所对应的用户配置对象
			var profile = this.GetUserProfile(context.User);

			//设置当前用户的扩展属性
			if(profile != null)
				context.Parameters.Add("Zongsoft.Community.UserProfile", profile);
		}

		public void OnFiltering(AuthenticationContext context)
		{
		}

		void IExecutionFilter.OnFiltered(object context)
		{
			if(context is AuthenticationContext ctx)
				this.OnFiltered(ctx);
		}

		void IExecutionFilter.OnFiltering(object context)
		{
			if(context is AuthenticationContext ctx)
				this.OnFiltering(ctx);
		}
		#endregion

		#region 私有方法
		private UserProfile GetUserProfile(IUserIdentity user)
		{
			var profile = this.DataAccess.Select<UserProfile>(Condition.Equal(nameof(UserProfile.UserId), user.UserId)).FirstOrDefault();

			if(profile != null)
				return profile;

			var siteId = 0U;

			if(!string.IsNullOrEmpty(user.Namespace))
			{
				var namespaces = Authentication.Instance.Namespaces ??
					throw new Zongsoft.Security.SecurityException($"The required namespace provider for the user is missing.");

				if(!namespaces.TryGetKey(user.Namespace, out siteId))
					throw new Zongsoft.Security.SecurityException($"Unable to confirm the '{user.Namespace}' namespace of the logged in user.");
			}

			profile = Model.Build<UserProfile>();
			profile.SiteId = siteId;
			profile.UserId = user.UserId;
			profile.Name = user.Name;
			profile.Nickname = user.FullName;
			profile.Description = user.Description;

			if(this.DataAccess.Upsert(profile) > 0)
				return profile;
			else
				return profile = null;
		}
		#endregion
	}
}
