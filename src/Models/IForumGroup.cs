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
	/// 表示论坛分组的业务实体接口。
	/// </summary>
	public interface IForumGroup : Zongsoft.Data.IEntity
	{
		/// <summary>
		/// 获取或设置论坛组所属的站点编号，复合主键。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组的编号，复合主键。
		/// </summary>
		ushort GroupId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组所在站点中的排列序号。
		/// </summary>
		ushort SortOrder
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组的名称。
		/// </summary>
		string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组的图标。
		/// </summary>
		string Icon
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组的可见性。
		/// </summary>
		Visiblity Visiblity
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组的描述文本。
		/// </summary>
		string Description
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛组中的论坛集。
		/// </summary>
		IEnumerable<IForum> Forums
		{
			get; set;
		}
	}
}
