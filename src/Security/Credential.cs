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
using System.Collections.Generic;

namespace Zongsoft.Community.Security
{
	public class Credential : Zongsoft.Security.Credential
	{
		#region 构造函数
		public Credential(Zongsoft.Security.Credential innerCredential) : base(innerCredential)
		{
		}
		#endregion

		#region 公共属性
		public bool InAdministrators
		{
			get
			{
				return false;
				//throw new NotImplementedException();
			}
		}

		public uint SiteId
		{
			get
			{
				if(this.Parameters.TryGetValue("Zongsoft.Community.UserProfile", out var value) && value is Models.IUserProfile profile)
					return profile.SiteId;

				return 0;
			}
		}
		#endregion
	}
}
