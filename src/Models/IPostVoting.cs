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
 * Copyright (C) 2015-2018 Zongsoft Corporation. All rights reserved.
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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示帖子投票的实体接口。
	/// </summary>
	public interface IPostVoting : Zongsoft.Data.IEntity
	{
		/// <summary>
		/// 获取或设置投票关联的帖子编号。
		/// </summary>
		ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票的用户编号。
		/// </summary>
		uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票的用户对象。
		/// </summary>
		IUserProfile User
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票的用户名称。
		/// </summary>
		string UserName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票的用户头像。
		/// </summary>
		string UserAvatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票值（正数为赞，负数为踩）。
		/// </summary>
		sbyte Value
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置投票的时间。
		/// </summary>
		DateTime Timestamp
		{
			get; set;
		}
	}
}
