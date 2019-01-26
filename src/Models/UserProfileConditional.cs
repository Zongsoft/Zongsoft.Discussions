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
	public interface IUserProfileConditional : IEntity
	{
		#region 公共属性
		[Conditional("Name", "PhoneNumber", "Email", "FullName")]
		string Key
		{
			get; set;
		}

		uint? SiteId
		{
			get; set;
		}

		[Conditional("Name", "PhoneNumber", "Email")]
		string Identity
		{
			get; set;
		}

		Gender? Gender
		{
			get; set;
		}
		#endregion
	}
}
