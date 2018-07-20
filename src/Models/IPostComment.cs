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
	/// 表示帖子回复记录的实体接口。
	/// </summary>
	public interface IPostComment : Zongsoft.Data.IEntity
	{
		/// <summary>
		/// 获取或设置帖子编号。
		/// </summary>
		ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复序号。
		/// </summary>
		ushort SerialId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复关联的源回复序号。
		/// </summary>
		ushort SourceId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复的内容。
		/// </summary>
		string Content
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复的内容类型。
		/// </summary>
		string ContentType
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者地址。
		/// </summary>
		string VisitorAddress
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者描述信息。
		/// </summary>
		string VisitorDescription
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复人编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复人对象。
		/// </summary>
		IUserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置回复时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}
	}
}
