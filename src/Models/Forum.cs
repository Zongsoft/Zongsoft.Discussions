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
	/// 表示论坛的业务实体类。
	/// </summary>
	public interface IForum : Zongsoft.Data.IEntity
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置站点编号，主键。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛编号，主键。
		/// </summary>
		ushort ForumId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛所属的组号。
		/// </summary>
		ushort GroupId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛所属的 <see cref="IForumGroup"/> 分组对象。
		/// </summary>
		IForumGroup Group
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛所在分组中的排列序号。
		/// </summary>
		ushort SortOrder
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的名称(标题)。
		/// </summary>
		string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的描述文本。
		/// </summary>
		string Description
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的封面图片路径。
		/// </summary>
		string CoverPicturePath
		{
			get; set;
		}

		/// <summary>
		/// 获取论坛封面图片的外部访问地址。
		/// </summary>
		[Zongsoft.Data.Entity.Property(Zongsoft.Data.Entity.PropertyImplementationMode.Extension, typeof(ForumExtension))]
		string CoverPictureUrl
		{
			get;
		}

		/// <summary>
		/// 获取或设置一个值，表示论坛是否为热门版块。
		/// </summary>
		bool IsPopular
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示发帖是否需要审核。
		/// </summary>
		bool ApproveEnabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的可见性。
		/// </summary>
		Visiblity Visiblity
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的可访问性。
		/// </summary>
		Accessibility Accessibility
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中的累计帖子总数。
		/// </summary>
		uint TotalPosts
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中的累积主题总数。
		/// </summary>
		uint TotalThreads
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的编号。
		/// </summary>
		ulong? MostRecentThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的标题。
		/// </summary>
		string MostRecentThreadSubject
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的作者编号。
		/// </summary>
		uint? MostRecentThreadAuthorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的作者名称。
		/// </summary>
		string MostRecentThreadAuthorName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的作者头像。
		/// </summary>
		string MostRecentThreadAuthorAvatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最新主题的发布时间。
		/// </summary>
		DateTime? MostRecentThreadTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖编号。
		/// </summary>
		ulong? MostRecentPostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最后回帖的作者编号。
		/// </summary>
		uint? MostRecentPostAuthorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最后回帖的作者名称。
		/// </summary>
		string MostRecentPostAuthorName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最后回帖的作者头像。
		/// </summary>
		string MostRecentPostAuthorAvatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛中最后回帖的时间。
		/// </summary>
		DateTime? MostRecentPostTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛创建人编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛的版主集。
		/// </summary>
		IEnumerable<IUserProfile> Moderators
		{
			get; set;
		}
		#endregion
	}

	public interface IForumConditional : IEntity
	{
		#region 公共属性
		string Name
		{
			get; set;
		}

		Visiblity? Visiblity
		{
			get; set;
		}

		Accessibility? Accessibility
		{
			get; set;
		}

		bool? IsPopular
		{
			get; set;
		}

		Range<DateTime> CreatedTime
		{
			get; set;
		}
		#endregion
	}

	/// <summary>
	/// 表示论坛用户的业务实体类。
	/// </summary>
	public class ForumUser
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置站点编号。
		/// </summary>
		public uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置论坛编号。
		/// </summary>
		public ushort ForumId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户编号。
		/// </summary>
		public uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户信息。
		/// </summary>
		public IUserProfile User
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置当前论坛用户的所属种类。
		/// </summary>
		public UserKind UserKind
		{
			get; set;
		}
		#endregion
	}

	internal static class ForumExtension
	{
		public static string GetCoverPictureUrl(IForum forum)
		{
			return Zongsoft.IO.FileSystem.GetUrl(forum.CoverPicturePath);
		}
	}
}
