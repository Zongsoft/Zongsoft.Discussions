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
	/// 表示用户浏览记录的实体接口。
	/// </summary>
	public interface IHistory : Zongsoft.Data.IEntity
	{
		/// <summary>
		/// 获取或设置用户的编号。
		/// </summary>
		uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置浏览的主题编号。
		/// </summary>
		ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置浏览的主题对象。
		/// </summary>
		IThread Thread
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置累计浏览次数。
		/// </summary>
		uint Count
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置首次浏览的时间。
		/// </summary>
		DateTime FirstViewedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置最后浏览的时间。
		/// </summary>
		DateTime MostRecentViewedTime
		{
			get; set;
		}
	}
}
