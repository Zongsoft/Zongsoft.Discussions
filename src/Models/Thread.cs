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
	/// 表示主题的业务实体类。
	/// </summary>
	public interface IThread : Zongsoft.Data.IModel
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置主题编号，主键。
		/// </summary>
		ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的站点编号。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的论坛编号。
		/// </summary>
		ushort ForumId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的论坛对象。
		/// </summary>
		IForum Forum
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的标题。
		/// </summary>
		string Subject
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容摘要。
		/// </summary>
		string Summary
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的标签集。
		/// </summary>
		string Tags
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容帖子编号。
		/// </summary>
		ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容帖子对象。
		/// </summary>
		IPost Post
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的封面图片路径。
		/// </summary>
		string CoverPicturePath
		{
			get; set;
		}

		/// <summary>
		/// 获取主体的封面图片的访问地址。
		/// </summary>
		[Model.Property(Model.PropertyImplementationMode.Extension, typeof(ThreadExtension))]
		string CoverPictureUrl
		{
			get;
		}

		/// <summary>
		/// 获取或设置主题的链接地址。
		/// </summary>
		string LinkUrl
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的状态。
		/// </summary>
		ThreadStatus Status
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的状态变更时间。
		/// </summary>
		DateTime? StatusTimestamp
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的状态描述文本。
		/// </summary>
		string StatusDescription
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否被禁用，如果禁用则不显示。
		/// </summary>
		bool Disabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否可见。
		/// </summary>
		bool Visible
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否已经审核通过。
		/// </summary>
		bool IsApproved
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否被锁定，如果锁定则不允许回复。
		/// </summary>
		bool IsLocked
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否置顶。
		/// </summary>
		bool IsPinned
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否为精华帖。
		/// </summary>
		bool IsValued
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否为全局贴。
		/// </summary>
		bool IsGlobal
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的累计浏览总数。
		/// </summary>
		uint TotalViews
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的回帖总数。
		/// </summary>
		uint TotalReplies
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的置顶时间。
		/// </summary>
		DateTime? PinnedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的全局发布的时间。
		/// </summary>
		DateTime? GlobalTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置最后被阅读的时间。
		/// </summary>
		DateTime? ViewedTime
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
		/// 获取或设置主题的最后回帖作者编号。
		/// </summary>
		uint? MostRecentPostAuthorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者名称。
		/// </summary>
		string MostRecentPostAuthorName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者头像。
		/// </summary>
		string MostRecentPostAuthorAvatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖时间。
		/// </summary>
		DateTime? MostRecentPostTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的作者编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的作者。
		/// </summary>
		IUserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}
		#endregion
	}

	public interface IThreadConditional : IModel
	{
		#region 公共属性
		string Subject
		{
			get; set;
		}

		ThreadStatus? Status
		{
			get; set;
		}

		Range<DateTime> StatusTimestamp
		{
			get; set;
		}

		bool? Disabled
		{
			get; set;
		}

		bool? Visible
		{
			get; set;
		}

		bool? IsApproved
		{
			get; set;
		}

		bool? IsLocked
		{
			get; set;
		}

		bool? IsPinned
		{
			get; set;
		}

		bool? IsValued
		{
			get; set;
		}

		bool? IsGlobal
		{
			get; set;
		}

		uint? CreatorId
		{
			get; set;
		}

		Range<DateTime> CreatedTime
		{
			get; set;
		}
		#endregion
	}

	internal static class ThreadExtension
	{
		public static string GetCoverPictureUrl(IThread thread)
		{
			return Zongsoft.IO.FileSystem.GetUrl(thread.CoverPicturePath);
		}
	}
}
