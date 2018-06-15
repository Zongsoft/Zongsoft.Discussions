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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	public class UserProfileConditional : Zongsoft.Data.Conditional
	{
		#region 公共属性
		[Conditional("User.Name", "User.PhoneNumber", "User.Email", "User.FullName")]
		public string Key
		{
			get
			{
				return this.GetPropertyValue(() => this.Key);
			}
			set
			{
				this.SetPropertyValue(() => this.Key, value);
			}
		}

		public uint? SiteId
		{
			get
			{
				return this.GetPropertyValue(() => this.SiteId);
			}
			set
			{
				this.SetPropertyValue(() => this.SiteId, value);
			}
		}

		[Conditional("User.Name", "User.PhoneNumber", "User.Email")]
		public string Identity
		{
			get
			{
				return this.GetPropertyValue(() => this.Identity);
			}
			set
			{
				this.SetPropertyValue(() => this.Identity, value);
			}
		}

		public Gender? Gender
		{
			get
			{
				return this.GetPropertyValue(() => this.Gender);
			}
			set
			{
				this.SetPropertyValue(() => this.Gender, value);
			}
		}
		#endregion
	}
}
