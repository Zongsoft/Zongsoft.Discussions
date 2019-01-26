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
	/// 表示消息的业务实体类。
	/// </summary>
	public interface IMessage : Zongsoft.Data.IEntity
	{
		#region 公共属性
		ulong MessageId
		{
			get; set;
		}

		uint SiteId
		{
			get; set;
		}

		string Subject
		{
			get; set;
		}

		string Content
		{
			get; set;
		}

		string ContentType
		{
			get; set;
		}

		string MessageType
		{
			get; set;
		}

		string Source
		{
			get; set;
		}

		MessageStatus Status
		{
			get; set;
		}

		DateTime? StatusTimestamp
		{
			get; set;
		}

		string StatusDescription
		{
			get; set;
		}

		uint? CreatorId
		{
			get; set;
		}

		IUserProfile Creator
		{
			get; set;
		}

		DateTime CreatedTime
		{
			get; set;
		}
		#endregion

		#region 关联属性
		IEnumerable<MessageUser> Users
		{
			get; set;
		}
		#endregion
	}

	public interface IMessageConditional : IEntity
	{
		#region 公共属性
		string Subject
		{
			get; set;
		}

		string MessageType
		{
			get; set;
		}

		string Source
		{
			get; set;
		}

		MessageStatus? Status
		{
			get; set;
		}

		Range<DateTime> StatusTimestamp
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
	/// 表示消息接受人的业务实体类。
	/// </summary>
	public struct MessageUser
	{
		#region 构造函数
		public MessageUser(ulong messageId, uint userId, bool isRead = false)
		{
			this.MessageId = messageId;
			this.UserId = userId;
			this.IsRead = isRead;

			this.User = null;
			this.Message = null;
		}

		public MessageUser(uint userId)
		{
			this.MessageId = 0;
			this.UserId = userId;
			this.IsRead = false;

			this.User = null;
			this.Message = null;
		}
		#endregion

		public ulong MessageId
		{
			get;
			set;
		}

		public IMessage Message
		{
			get;
			set;
		}

		public uint UserId
		{
			get;
			set;
		}

		public IUserProfile User
		{
			get;
			set;
		}

		public bool IsRead
		{
			get;
			set;
		}
	}
}
