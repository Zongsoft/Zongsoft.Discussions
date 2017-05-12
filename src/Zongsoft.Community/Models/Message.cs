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
	public class Message : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private ulong _messageId;
		private uint _siteId;
		private MessageStatus _status;
		private DateTime _statusTimestamp;
		private uint? _fromId;
		private uint? _toId;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public Message()
		{
			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		public ulong MessageId
		{
			get
			{
				return _messageId;
			}
			set
			{
				this.SetPropertyValue(() => this.MessageId, ref _messageId, value);
			}
		}

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

		public string Subject
		{
			get
			{
				return this.GetPropertyValue(() => this.Subject);
			}
			set
			{
				this.SetPropertyValue(() => this.Subject, value);
			}
		}

		public string Content
		{
			get
			{
				return this.GetPropertyValue(() => this.Content);
			}
			set
			{
				this.SetPropertyValue(() => this.Content, value);
			}
		}

		public MessageStatus Status
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

		public DateTime StatusTimestamp
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

		public uint? FromId
		{
			get
			{
				return _fromId;
			}
			set
			{
				this.SetPropertyValue(() => this.FromId, ref _fromId, value);
			}
		}

		public UserProfile From
		{
			get
			{
				return this.GetPropertyValue(() => this.From);
			}
			set
			{
				this.SetPropertyValue(() => this.From, value);
			}
		}

		public uint? ToId
		{
			get
			{
				return _toId;
			}
			set
			{
				this.SetPropertyValue(() => this.ToId, ref _toId, value);
			}
		}

		public UserProfile To
		{
			get
			{
				return this.GetPropertyValue(() => this.To);
			}
			set
			{
				this.SetPropertyValue(() => this.To, value);
			}
		}

		public DateTime? ReadTime
		{
			get
			{
				return this.GetPropertyValue(() => this.ReadTime);
			}
			set
			{
				this.SetPropertyValue(() => this.ReadTime, value);
			}
		}

		public DateTime CreatedTime
		{
			get
			{
				return _createdTime;
			}
			set
			{
				this.SetPropertyValue(() => this.CreatedTime, ref _createdTime, value);
			}
		}
		#endregion
	}
}
