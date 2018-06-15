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
using System.ComponentModel;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示可访问性(可写入性)的枚举。
	/// </summary>
	public enum Accessibility : byte
	{
		/// <summary>无限制，所有用户均可访问</summary>
		None,

		/// <summary>登录用户均可访问</summary>
		Users,

		/// <summary>仅限管理员</summary>
		Administrators,

		/// <summary>指定的用户</summary>
		Specifics,
	}
}
