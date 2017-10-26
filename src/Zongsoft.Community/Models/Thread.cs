/*
 * Authors:
 *   钟峰(Popeye Zhong) <9555843@qq.com>
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
	/// 表示主题的业务实体类。
	/// </summary>
	public class Thread : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private ulong _threadId;
		private uint _siteId;
		private ushort _forumId;
		private ulong _postId;
		private string _tags;
		private ThreadStatus _status;
		private DateTime? _statusTimestamp;
		private bool _disabled;
		private bool _isApproved;
		private bool _isLocked;
		private bool _isPinned;
		private bool _isValued;
		private bool _isGlobal;
		private uint _totalViews;
		private uint _totalReplies;
		private ulong? _mostRecentPostId;
		private uint? _mostRecentPostAuthorId;
		private uint _creatorId;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public Thread()
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置主题编号，主键。
		/// </summary>
		public ulong ThreadId
		{
			get
			{
				return _threadId;
			}
			set
			{
				this.SetPropertyValue(() => this.ThreadId, ref _threadId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题所属的站点编号。
		/// </summary>
		public uint SiteId
		{
			get
			{
				return _siteId;
			}
			set
			{
				this.SetPropertyValue(() => this.SiteId, ref _siteId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题所属的论坛编号。
		/// </summary>
		public ushort ForumId
		{
			get
			{
				return _forumId;
			}
			set
			{
				this.SetPropertyValue(() => this.ForumId, ref _forumId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题所属的论坛对象。
		/// </summary>
		public Forum Forum
		{
			get
			{
				return this.GetPropertyValue(() => this.Forum);
			}
			set{
				this.SetPropertyValue(() => this.Forum, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的标题。
		/// </summary>
		public string Subject
		{
			get
			{
				return this.GetPropertyValue(() => Subject);
			}
			set
			{
				this.SetPropertyValue(() => this.Subject, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的内容摘要。
		/// </summary>
		public string Summary
		{
			get
			{
				return this.GetPropertyValue(() => this.Summary);
			}
			set
			{
				this.SetPropertyValue(() => this.Summary, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的标签集。
		/// </summary>
		public string Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				this.SetPropertyValue(nameof(Tags), ref _tags, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的内容帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				this.SetPropertyValue(() => this.PostId, ref _postId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的内容帖子对象。
		/// </summary>
		public Post Post
		{
			get
			{
				return this.GetPropertyValue(() => this.Post);
			}
			set
			{
				this.SetPropertyValue(() => this.Post, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的封面图片路径。
		/// </summary>
		public string CoverPicturePath
		{
			get
			{
				return this.GetPropertyValue(() => this.CoverPicturePath);
			}
			set
			{
				this.SetPropertyValue(() => this.CoverPicturePath, value);
			}
		}

		/// <summary>
		/// 获取主体的封面图片的访问地址。
		/// </summary>
		public string CoverPictureUrl
		{
			get
			{
				return Zongsoft.IO.FileSystem.GetUrl(this.CoverPicturePath);
			}
		}

		/// <summary>
		/// 获取或设置主题的链接地址。
		/// </summary>
		public string LinkUrl
		{
			get
			{
				return this.GetPropertyValue(() => this.LinkUrl);
			}
			set
			{
				this.SetPropertyValue(() => this.LinkUrl, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的状态。
		/// </summary>
		public ThreadStatus Status
		{
			get
			{
				return _status;
			}
			set
			{
				this.SetPropertyValue(() => this.Status, ref _status, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的状态变更时间。
		/// </summary>
		public DateTime? StatusTimestamp
		{
			get
			{
				return _statusTimestamp;
			}
			set
			{
				this.SetPropertyValue(() => this.StatusTimestamp, ref _statusTimestamp, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的状态描述文本。
		/// </summary>
		public string StatusDescription
		{
			get
			{
				return this.GetPropertyValue(() => this.StatusDescription);
			}
			set
			{
				this.SetPropertyValue(() => this.StatusDescription, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否被禁用，如果禁用则不显示。
		/// </summary>
		public bool Disabled
		{
			get
			{
				return _disabled;
			}
			set
			{
				this.SetPropertyValue(() => this.Disabled, ref _disabled, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否已经审核通过。
		/// </summary>
		public bool IsApproved
		{
			get
			{
				return _isApproved;
			}
			set
			{
				this.SetPropertyValue(() => this.IsApproved, ref _isApproved, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否被锁定，如果锁定则不允许回复。
		/// </summary>
		public bool IsLocked
		{
			get
			{
				return _isLocked;
			}
			set
			{
				this.SetPropertyValue(() => this.IsLocked, ref _isLocked, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否置顶。
		/// </summary>
		public bool IsPinned
		{
			get
			{
				return _isPinned;
			}
			set
			{
				this.SetPropertyValue(() => this.IsPinned, ref _isPinned, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否为精华帖。
		/// </summary>
		public bool IsValued
		{
			get
			{
				return _isValued;
			}
			set
			{
				this.SetPropertyValue(() => this.IsValued, ref _isValued, value);
			}
		}

		/// <summary>
		/// 获取或设置主题是否为全局贴。
		/// </summary>
		public bool IsGlobal
		{
			get
			{
				return _isGlobal;
			}
			set
			{
				this.SetPropertyValue(() => this.IsGlobal, ref _isGlobal, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的累计浏览总数。
		/// </summary>
		public uint TotalViews
		{
			get
			{
				return _totalViews;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalViews, ref _totalViews, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的回帖总数。
		/// </summary>
		public uint TotalReplies
		{
			get
			{
				return _totalReplies;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalReplies, ref _totalReplies, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的置顶时间。
		/// </summary>
		public DateTime? PinnedTime
		{
			get
			{
				return this.GetPropertyValue(() => this.PinnedTime);
			}
			set
			{
				this.SetPropertyValue(() => this.PinnedTime, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的全局发布的时间。
		/// </summary>
		public DateTime? GlobalTime
		{
			get
			{
				return this.GetPropertyValue(() => this.GlobalTime);
			}
			set
			{
				this.SetPropertyValue(() => this.GlobalTime, value);
			}
		}

		/// <summary>
		/// 获取或设置最后被阅读的时间。
		/// </summary>
		public DateTime? ViewedTime
		{
			get
			{
				return this.GetPropertyValue(() => this.ViewedTime);
			}
			set
			{
				this.SetPropertyValue(() => this.ViewedTime, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的最后回帖编号。
		/// </summary>
		public ulong? MostRecentPostId
		{
			get
			{
				return _mostRecentPostId;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostId, ref _mostRecentPostId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者编号。
		/// </summary>
		public uint? MostRecentPostAuthorId
		{
			get
			{
				return _mostRecentPostAuthorId;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostAuthorId, ref _mostRecentPostAuthorId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者名称。
		/// </summary>
		public string MostRecentPostAuthorName
		{
			get
			{
				return this.GetPropertyValue(() => this.MostRecentPostAuthorName);
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostAuthorName, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的最后回帖作者头像。
		/// </summary>
		public string MostRecentPostAuthorAvatar
		{
			get
			{
				return this.GetPropertyValue(() => this.MostRecentPostAuthorAvatar);
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostAuthorAvatar, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的最后回帖时间。
		/// </summary>
		public DateTime? MostRecentPostTime
		{
			get
			{
				return this.GetPropertyValue(() => this.MostRecentPostTime);
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostTime, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的作者编号。
		/// </summary>
		public uint CreatorId
		{
			get
			{
				return _creatorId;
			}
			set
			{
				this.SetPropertyValue(nameof(CreatorId), ref _creatorId, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的作者。
		/// </summary>
		public UserProfile Creator
		{
			get
			{
				return this.GetPropertyValue(() => this.Creator);
			}
			set
			{
				this.SetPropertyValue(() => this.Creator, value);
			}
		}

		/// <summary>
		/// 获取或设置主题的创建时间。
		/// </summary>
		public DateTime CreatedTime
		{
			get
			{
				return _createdTime;
			}
			set
			{
				this.SetPropertyValue(nameof(CreatedTime), ref _createdTime, value);
			}
		}
		#endregion
	}
}
