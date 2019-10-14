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
using System.ComponentModel;
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示主题的业务实体类。
	/// </summary>
	public abstract class Thread
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置主题编号，主键。
		/// </summary>
		public abstract ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的站点编号。
		/// </summary>
		public abstract uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的论坛编号。
		/// </summary>
		public abstract ushort ForumId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题所属的论坛对象。
		/// </summary>
		public abstract Forum Forum
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的标题。
		/// </summary>
		public abstract string Title
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容摘要。
		/// </summary>
		public abstract string Summary
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的标签集。
		/// </summary>
		[TypeConverter(typeof(TagsConverter))]
		public abstract string[] Tags
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容帖子编号。
		/// </summary>
		public abstract ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的内容帖子对象。
		/// </summary>
		public abstract Post Post
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的封面图片路径。
		/// </summary>
		public abstract string CoverPicturePath
		{
			get; set;
		}

		/// <summary>
		/// 获取主体的封面图片的访问地址。
		/// </summary>
		public string CoverPictureUrl
		{
			get => Zongsoft.IO.FileSystem.GetUrl(this.CoverPicturePath);
		}

		/// <summary>
		/// 获取或设置主题的链接地址。
		/// </summary>
		public abstract string ArticleUrl
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否被禁用，如果禁用则不显示。
		/// </summary>
		public abstract bool Disabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否可见。
		/// </summary>
		public abstract bool Visible
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否已经审核通过。
		/// </summary>
		public abstract bool IsApproved
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否被锁定，如果锁定则不允许回复。
		/// </summary>
		public abstract bool IsLocked
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否置顶。
		/// </summary>
		public abstract bool IsPinned
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否为精华帖。
		/// </summary>
		public abstract bool IsValued
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题是否为全局贴。
		/// </summary>
		public abstract bool IsGlobal
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的累计浏览总数。
		/// </summary>
		public abstract uint TotalViews
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的回帖总数。
		/// </summary>
		public abstract uint TotalReplies
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的置顶时间。
		/// </summary>
		public abstract DateTime? PinnedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的全局发布的时间。
		/// </summary>
		public abstract DateTime? GlobalTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置最后被阅读的时间。
		/// </summary>
		public abstract DateTime? ViewedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖编号。
		/// </summary>
		public abstract ulong? MostRecentPostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者编号。
		/// </summary>
		public abstract uint? MostRecentPostAuthorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者名称。
		/// </summary>
		public abstract string MostRecentPostAuthorName
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者头像。
		/// </summary>
		public abstract string MostRecentPostAuthorAvatar
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的最后回帖时间。
		/// </summary>
		public abstract DateTime? MostRecentPostTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的作者编号。
		/// </summary>
		public abstract uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的作者。
		/// </summary>
		public abstract UserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置主题的创建时间。
		/// </summary>
		public abstract DateTime CreatedTime
		{
			get; set;
		}
		#endregion
	}

	/// <summary>
	/// 表示主题查询条件的实体类。
	/// </summary>
	public abstract class ThreadConditional : ConditionalBase
	{
		#region 公共属性
		[Conditional(ConditionOperator.Like)]
		public abstract string Title
		{
			get; set;
		}

		public abstract bool? Disabled
		{
			get; set;
		}

		public abstract bool? Visible
		{
			get; set;
		}

		public abstract bool? IsApproved
		{
			get; set;
		}

		public abstract bool? IsLocked
		{
			get; set;
		}

		public abstract bool? IsPinned
		{
			get; set;
		}

		public abstract bool? IsValued
		{
			get; set;
		}

		public abstract bool? IsGlobal
		{
			get; set;
		}

		public abstract uint? CreatorId
		{
			get; set;
		}

		public abstract Range<DateTime>? CreatedTime
		{
			get; set;
		}
		#endregion
	}
}
