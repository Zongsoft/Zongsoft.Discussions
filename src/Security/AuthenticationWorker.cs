/*
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
using Zongsoft.Services;
using Zongsoft.Security.Membership;

using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Security
{
	public class AuthenticationWorker : Zongsoft.Services.WorkerBase
	{
		#region 成员字段
		private IDataAccess _dataAccess;
		private IAuthenticator _authentication;
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
		[ServiceDependency(Provider = "Community")]
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
		/// 获取或设置身份验证服务。
		/// </summary>
		[ServiceDependency]
		public IAuthenticator Authentication
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
			var profile = this.GetUserProfile(e.User.UserId);

			//设置当前用户的扩展属性
			if(profile != null)
				e.Parameters.Add("Zongsoft.Community.UserProfile", profile);
		}
		#endregion

		#region 私有方法
		private IUserProfile GetUserProfile(uint userId)
		{
			return this.DataAccess.Select<IUserProfile>(Condition.Equal("UserId", userId)).FirstOrDefault();
		}
		#endregion
	}
}
