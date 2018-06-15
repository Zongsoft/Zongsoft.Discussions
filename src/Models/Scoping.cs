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
	/// 表示数据作用域的枚举。
	/// </summary>
	public enum Scoping : byte
	{
		/// <summary>无限制。</summary>
		None,

		/// <summary>隐藏，仅限资源所有者。</summary>
		Hidden,

		/// <summary>内部，仅限站内成员。</summary>
		Internal,

		/// <summary>外部，站内外用户（即非匿名用户）。</summary>
		External,

		/// <summary>指定的用户</summary>
		Specific,
	}
}
