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

using Zongsoft.Data;
using Zongsoft.Services;
using Zongsoft.Resources;
using Zongsoft.Security.Membership;

using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Security
{
	public class AuthenticationWorker : Zongsoft.Services.WorkerBase
	{
		#region 成员字段
		private IDataAccess _dataAccess;
		private UserService _userService;
		private IAuthentication _authentication;
		#endregion

		#region 构造函数
		public AuthenticationWorker()
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置数据访问服务。
		/// </summary>
		[ServiceDependency]
		public IDataAccess DataAccess
		{
			get
			{
				return _dataAccess;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_dataAccess = value;
			}
		}

		/// <summary>
		/// 获取或设置用户管理服务。
		/// </summary>
		[ServiceDependency]
		public UserService UserService
		{
			get
			{
				return _userService;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_userService = value;
			}
		}

		/// <summary>
		/// 获取或设置身份验证服务。
		/// </summary>
		[ServiceDependency]
		public IAuthentication Authentication
		{
			get
			{
				return _authentication;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_authentication = value;
			}
		}
		#endregion

		#region 重写方法
		protected override void OnStart(string[] args)
		{
			var authentication = this.Authentication;

			if(authentication != null)
				authentication.Authenticated += OnAuthenticated;
		}

		protected override void OnStop(string[] args)
		{
			var authentication = this.Authentication;

			if(authentication != null)
				authentication.Authenticated -= OnAuthenticated;
		}
		#endregion

		#region 验证处理
		private void OnAuthenticated(object sender, AuthenticatedEventArgs e)
		{
			//如果身份验证失败则退出
			if(!e.IsAuthenticated)
				return;

			//获取当前登录用户所对应的用户配置对象
			var userProfile = this.UserService.Get(e.User.UserId) as UserProfile;

			//如果对应的用户配置对象没有存在则新增一个
			if(userProfile == null)
			{
				uint siteId = 0;

				if(uint.TryParse(e.User.PrincipalId, out siteId))
				{
					userProfile = new UserProfile(e.User.UserId, siteId);
					this.DataAccess.Insert(userProfile);
				}
			}

			//设置当前用户的扩展属性
			e.Parameters.Add("Community.UserProfile", userProfile);
		}
		#endregion
	}
}
