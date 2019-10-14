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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示社区子系统中的用户设置信息。
	/// </summary>
	public abstract class UserProfile : Zongsoft.Security.Membership.IUserIdentity
	{
		#region 构造函数
		protected UserProfile()
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置用户所属的站点编号。
		/// </summary>
		public abstract uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户编号。
		/// </summary>
		public abstract uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户名称。
		/// </summary>
		public abstract string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户全称（昵称）。
		/// </summary>
		public abstract string FullName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户的所属命名空间。
		/// </summary>
		public abstract string Namespace
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置头像标识。
		/// </summary>
		public abstract string Avatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户性别。
		/// </summary>
		public abstract Gender Gender
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户等级，数字越大等级越高。
		/// </summary>
		public abstract byte Grade
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户的照片文件路径。
		/// </summary>
		public abstract string PhotoPath
		{
			get; set;
		}

		/// <summary>
		/// 获取用户的照片文件访问URL。
		/// </summary>
		public string PhotoUrl
		{
			get => Zongsoft.IO.FileSystem.GetUrl(this.PhotoPath);
		}

		/// <summary>
		/// 获取或设置用户累计发布的帖子总数。
		/// </summary>
		public abstract uint TotalPosts
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户累积发布的主题总数。
		/// </summary>
		public abstract uint TotalThreads
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最后回帖的帖子编号。
		/// </summary>
		public abstract ulong? MostRecentPostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最后回帖的时间。
		/// </summary>
		public abstract DateTime? MostRecentPostTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题编号。
		/// </summary>
		public abstract ulong? MostRecentThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题标题。
		/// </summary>
		public abstract string MostRecentThreadSubject
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新主题的发布时间。
		/// </summary>
		public abstract DateTime? MostRecentThreadTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户的描述信息。
		/// </summary>
		public abstract string Description
		{
			get; set;
		}
		#endregion
	}

	public abstract class UserProfileConditional : ConditionalBase
	{
		#region 公共属性
		public abstract uint? SiteId
		{
			get; set;
		}

		[Conditional("Name", "Phone", "Email")]
		public abstract string Identity
		{
			get; set;
		}

		public abstract Gender? Gender
		{
			get; set;
		}

		public abstract Range<byte>? Grade
		{
			get; set;
		}
		#endregion
	}
}
