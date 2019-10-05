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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示帖子的业务实体类。
	/// </summary>
	public interface IPost : Zongsoft.Data.IModel
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号，主键。
		/// </summary>
		ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的站点编号。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题编号。
		/// </summary>
		ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题对象。
		/// </summary>
		IThread Thread
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容。
		/// </summary>
		string Content
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容类型。
		/// </summary>
		string ContentType
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示是否禁用。
		/// </summary>
		bool Disabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被审核通过。
		/// </summary>
		bool IsApproved
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被锁定，如果锁定则不允许回复。
		/// </summary>
		bool IsLocked
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否为精华帖。
		/// </summary>
		bool IsValued
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计点赞总数。
		/// </summary>
		uint TotalUpvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计被踩总数。
		/// </summary>
		uint TotalDownvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的位置信息。
		/// </summary>
		string VisitorAddress
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的描述信息。
		/// </summary>
		string VisitorDescription
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者对应的用户对象。
		/// </summary>
		UserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的回复集。
		/// </summary>
		IEnumerable<PostComment> Comments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的附件集。
		/// </summary>
		IEnumerable<PostAttachment> Attachments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的投票（点赞和被踩）记录集。
		/// </summary>
		IEnumerable<PostVoting> Votes
		{
			get; set;
		}

		/// <summary>
		/// 获取帖子的点赞记录集。
		/// </summary>
		[Model.Property(Model.PropertyImplementationMode.Extension, typeof(PostExtension))]
		[Runtime.Serialization.SerializationMember(Runtime.Serialization.SerializationMemberBehavior.Ignored)]
		IEnumerable<PostVoting> Upvotes
		{
			get;
		}

		/// <summary>
		/// 获取帖子的被踩记录集。
		/// </summary>
		[Model.Property(Model.PropertyImplementationMode.Extension, typeof(PostExtension))]
		[Runtime.Serialization.SerializationMember(Runtime.Serialization.SerializationMemberBehavior.Ignored)]
		IEnumerable<PostVoting> Downvotes
		{
			get;
		}
		#endregion
	}

	public interface IPostConditional : IModel
	{
		#region 公共属性
		string Content
		{
			get; set;
		}

		ulong? ParentId
		{
			get; set;
		}

		bool? Disabled
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

		bool? IsValued
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

	/// <summary>
	/// 表示帖子回复的业务实体类。
	/// </summary>
	public class PostComment
	{
		#region 成员字段
		private ulong _postId;
		private ushort _serialId;
		private ushort _sourceId;
		private string _content;
		private string _contentType;
		private string _visitorAddress;
		private string _visitorDescription;
		private uint _creatorId;
		private UserProfile _creator;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public PostComment()
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				_postId = value;
			}
		}

		/// <summary>
		/// 获取或设置回复序号。
		/// </summary>
		public ushort SerialId
		{
			get
			{
				return _serialId;
			}
			set
			{
				_serialId = value;
			}
		}

		/// <summary>
		/// 获取或设置回复关联的源回复序号。
		/// </summary>
		public ushort SourceId
		{
			get
			{
				return _sourceId;
			}
			set
			{
				_sourceId = value;
			}
		}

		/// <summary>
		/// 获取或设置回复的内容。
		/// </summary>
		public string Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		/// <summary>
		/// 获取或设置回复的内容类型。
		/// </summary>
		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}

		/// <summary>
		/// 获取或设置访问者地址。
		/// </summary>
		public string VisitorAddress
		{
			get
			{
				return _visitorAddress;
			}
			set
			{
				_visitorAddress = value;
			}
		}

		/// <summary>
		/// 获取或设置访问者描述信息。
		/// </summary>
		public string VisitorDescription
		{
			get
			{
				return _visitorDescription;
			}
			set
			{
				_visitorDescription = value;
			}
		}

		/// <summary>
		/// 获取或设置回复人编号。
		/// </summary>
		public uint CreatorId
		{
			get
			{
				return _creatorId;
			}
			set
			{
				_creatorId = value;
			}
		}

		/// <summary>
		/// 获取或设置回复人对象。
		/// </summary>
		public UserProfile Creator
		{
			get
			{
				return _creator;
			}
			set
			{
				_creator = value;
			}
		}

		/// <summary>
		/// 获取或设置回复时间。
		/// </summary>
		public DateTime CreatedTime
		{
			get
			{
				return _createdTime;
			}
			set
			{
				_createdTime = value;
			}
		}
		#endregion
	}

	/// <summary>
	/// 表示帖子投票的业务实体类。
	/// </summary>
	public class PostVoting
	{
		#region 成员字段
		private ulong _postId;
		private uint _userId;
		private UserProfile _user;
		private string _userName;
		private string _userAvatar;
		private sbyte _value;
		private DateTime _timestamp;
		#endregion

		#region 构造函数
		public PostVoting()
		{
			_timestamp = DateTime.Now;
		}

		public PostVoting(ulong postId, uint userId, sbyte value)
		{
			_postId = postId;
			_userId = userId;
			_value = value;
			_timestamp = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置投票关联的帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				_postId = value;
			}
		}

		/// <summary>
		/// 获取或设置投票的用户编号。
		/// </summary>
		public uint UserId
		{
			get
			{
				return _userId;
			}
			set
			{
				_userId = value;
			}
		}

		/// <summary>
		/// 获取或设置投票的用户对象。
		/// </summary>
		public UserProfile User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		/// <summary>
		/// 获取或设置投票的用户名称。
		/// </summary>
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
			}
		}

		/// <summary>
		/// 获取或设置投票的用户头像。
		/// </summary>
		public string UserAvatar
		{
			get
			{
				return _userAvatar;
			}
			set
			{
				_userAvatar = value;
			}
		}

		/// <summary>
		/// 获取或设置投票值（正数为赞，负数为踩）。
		/// </summary>
		public sbyte Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// 获取或设置投票的时间。
		/// </summary>
		public DateTime Timestamp
		{
			get
			{
				return _timestamp;
			}
			set
			{
				_timestamp = value;
			}
		}
		#endregion
	}

	/// <summary>
	/// 表示帖子附件的业务实体类。
	/// </summary>
	public struct PostAttachment
	{
		#region 成员字段
		private ulong _postId;
		private ulong _fileId;
		private IFile _file;
		#endregion

		#region 构造函数
		public PostAttachment(ulong postId, ulong fileId)
		{
			_postId = postId;
			_fileId = fileId;
			_file = null;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				_postId = value;
			}
		}

		/// <summary>
		/// 获取或设置帖子的文件编号。
		/// </summary>
		public ulong FileId
		{
			get
			{
				return _fileId;
			}
			set
			{
				_fileId = value;
			}
		}

		/// <summary>
		/// 获取或设置帖子的文件对象。
		/// </summary>
		public IFile File
		{
			get
			{
				return _file;
			}
			set
			{
				_file = value;
			}
		}
		#endregion
	}

	public static class PostExtension
	{
		public static IEnumerable<PostVoting> GetUpvotes(IPost post)
		{
			return post.Votes.Where(vote => vote.Value > 0);
		}

		public static IEnumerable<PostVoting> GetDownvotes(IPost post)
		{
			return post.Votes.Where(vote => vote.Value < 0);
		}
	}
}
