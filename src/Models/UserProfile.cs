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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示社区子系统中的用户设置信息。
	/// </summary>
	public interface IUserProfile : Zongsoft.Data.IEntity
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置用户编号。
		/// </summary>
		uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户所属的站点编号。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户名称。
		/// </summary>
		string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户全称（昵称）。
		/// </summary>
		string FullName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置头像标识。
		/// </summary>
		string Avatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户性别。
		/// </summary>
		Gender Gender
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户的照片文件路径。
		/// </summary>
		string PhotoPath
		{
			get; set;
		}

		/// <summary>
		/// 获取用户的照片文件访问URL。
		/// </summary>
		[Zongsoft.Data.Entity.Property(Zongsoft.Data.Entity.PropertyImplementationMode.Extension, typeof(UserProfileExtension))]
		string PhotoUrl
		{
			get;
		}

		/// <summary>
		/// 获取或设置用户累计发布的帖子总数。
		/// </summary>
		uint TotalPosts
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户累积发布的主题总数。
		/// </summary>
		uint TotalThreads
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最后回帖的帖子编号。
		/// </summary>
		ulong? MostRecentPostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最后回帖的时间。
		/// </summary>
		DateTime? MostRecentPostTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题编号。
		/// </summary>
		ulong? MostRecentThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题标题。
		/// </summary>
		string MostRecentThreadSubject
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户最新主题的发布时间。
		/// </summary>
		DateTime? MostRecentThreadTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户配置信息的创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}
		#endregion
	}

	public interface IUserProfileConditional : IEntity
	{
		#region 公共属性
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

	internal static class UserProfileExtension
	{
		public static string GetPhotoUrl(IUserProfile profile)
		{
			return Zongsoft.IO.FileSystem.GetUrl(profile.PhotoPath);
		}
	}
}
