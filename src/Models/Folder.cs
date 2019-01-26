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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示文件夹的业务实体类。
	/// </summary>
	public interface IFolder : Zongsoft.Data.IEntity
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置文件夹编号。
		/// </summary>
		uint FolderId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置文件夹名称。
		/// </summary>
		string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置名称的拼音。
		/// </summary>
		string PinYin
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置图标名。
		/// </summary>
		string Icon
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置所属站点编号。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置可见性。
		/// </summary>
		Visiblity Visiblity
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置可访问性。
		/// </summary>
		Accessibility Accessibility
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置创建人编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置创建人对象。
		/// </summary>
		IUserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}
		#endregion

		#region 关联属性
		/// <summary>
		/// 获取或设置文件夹指定用户集合。
		/// </summary>
		IEnumerable<FolderUser> Users
		{
			get; set;
		}
		#endregion
	}

	public struct FolderUser
	{
		#region 公共属性
		public uint FolderId
		{
			get; set;
		}

		public uint UserId
		{
			get; set;
		}

		public IUserProfile User
		{
			get; set;
		}

		public UserKind UserKind
		{
			get; set;
		}
		#endregion
	}
}
