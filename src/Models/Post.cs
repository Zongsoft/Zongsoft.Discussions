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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示帖子的业务实体类。
	/// </summary>
	public abstract class Post
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号，主键。
		/// </summary>
		public abstract ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的站点编号。
		/// </summary>
		public abstract uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题编号。
		/// </summary>
		public abstract ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题对象。
		/// </summary>
		public abstract Thread Thread
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容。
		/// </summary>
		public abstract string Content
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容类型。
		/// </summary>
		public abstract string ContentType
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示是否禁用。
		/// </summary>
		public abstract bool Disabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被审核通过。
		/// </summary>
		public abstract bool IsApproved
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被锁定，如果锁定则不允许回复。
		/// </summary>
		public abstract bool IsLocked
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否为精华帖。
		/// </summary>
		public abstract bool IsValued
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计点赞总数。
		/// </summary>
		public abstract uint TotalUpvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计被踩总数。
		/// </summary>
		public abstract uint TotalDownvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的位置信息。
		/// </summary>
		public abstract string VisitorAddress
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的描述信息。
		/// </summary>
		public abstract string VisitorDescription
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件标记。
		/// </summary>
		public abstract string AttachmentMark
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者编号。
		/// </summary>
		public abstract uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的创建时间。
		/// </summary>
		public abstract DateTime CreatedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者对应的用户对象。
		/// </summary>
		public abstract UserProfile Creator
		{
			get; set;
		}
		#endregion

		#region 集合属性
		/// <summary>
		/// 获取或设置帖子的附件集。
		/// </summary>
		public abstract ICollection<File> Attachments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的回复集。
		/// </summary>
		public abstract IEnumerable<PostComment> Comments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的投票（点赞和被踩）记录集。
		/// </summary>
		public abstract IEnumerable<PostVoting> Votes
		{
			get; set;
		}

		/// <summary>
		/// 获取帖子的点赞记录集。
		/// </summary>
		[Runtime.Serialization.SerializationMember(Runtime.Serialization.SerializationMemberBehavior.Ignored)]
		public IEnumerable<PostVoting> Upvotes
		{
			get => this.Votes.Where(vote => vote.Value > 0);
		}

		/// <summary>
		/// 获取帖子的被踩记录集。
		/// </summary>
		[Runtime.Serialization.SerializationMember(Runtime.Serialization.SerializationMemberBehavior.Ignored)]
		public IEnumerable<PostVoting> Downvotes
		{
			get => this.Votes.Where(vote => vote.Value < 0);
		}
		#endregion

		#region 嵌套子类
		/// <summary>
		/// 表示帖子回复的业务实体类。
		/// </summary>
		public abstract class PostComment
		{
			#region 构造函数
			protected PostComment()
			{
			}
			#endregion

			#region 公共属性
			/// <summary>
			/// 获取或设置帖子编号。
			/// </summary>
			public abstract ulong PostId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复序号。
			/// </summary>
			public abstract ushort SerialId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复关联的源回复序号。
			/// </summary>
			public abstract ushort SourceId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复的内容。
			/// </summary>
			public abstract string Content
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复的内容类型。
			/// </summary>
			public abstract string ContentType
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置访问者地址。
			/// </summary>
			public abstract string VisitorAddress
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置访问者描述信息。
			/// </summary>
			public abstract string VisitorDescription
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复人编号。
			/// </summary>
			public abstract uint CreatorId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复人对象。
			/// </summary>
			public abstract UserProfile Creator
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置回复时间。
			/// </summary>
			public abstract DateTime CreatedTime
			{
				get; set;
			}
			#endregion
		}

		/// <summary>
		/// 表示帖子投票的业务实体类。
		/// </summary>
		public abstract class PostVoting
		{
			#region 构造函数
			protected PostVoting()
			{
			}
			#endregion

			#region 公共属性
			/// <summary>
			/// 获取或设置投票关联的帖子编号。
			/// </summary>
			public abstract ulong PostId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票的用户编号。
			/// </summary>
			public abstract uint UserId
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票的用户对象。
			/// </summary>
			public abstract UserProfile User
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票的用户名称。
			/// </summary>
			public abstract string UserName
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票的用户头像。
			/// </summary>
			public abstract string UserAvatar
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票值（正数为赞，负数为踩）。
			/// </summary>
			public abstract sbyte Value
			{
				get; set;
			}

			/// <summary>
			/// 获取或设置投票的时间。
			/// </summary>
			public abstract DateTime Timestamp
			{
				get; set;
			}
			#endregion
		}
		#endregion
	}

	/// <summary>
	/// 表示帖子查询条件的实体。
	/// </summary>
	public abstract class PostConditional : ConditionalBase
	{
		#region 公共属性
		public abstract ulong? ThreadId
		{
			get; set;
		}

		public abstract bool? Disabled
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

		public abstract bool? IsValued
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
