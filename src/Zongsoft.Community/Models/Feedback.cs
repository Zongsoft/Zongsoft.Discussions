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
	/// 表示意见反馈的业务实体类。
	/// </summary>
	public class Feedback : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private ulong _feedbackId;
		private uint _siteId;
		private byte _kind;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public Feedback()
		{
			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		public ulong FeedbackId
		{
			get
			{
				return _feedbackId;
			}
			set
			{
				this.SetPropertyValue(() => this.FeedbackId, ref _feedbackId, value);
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

		public byte Kind
		{
			get
			{
				return _kind;
			}
			set
			{
				this.SetPropertyValue(() => this.Kind, ref _kind, value);
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

		public string ContentType
		{
			get
			{
				return this.GetPropertyValue(() => this.ContentType);
			}
			set
			{
				this.SetPropertyValue(() => this.ContentType, value);
			}
		}

		public string ContactName
		{
			get
			{
				return this.GetPropertyValue(() => this.ContactName);
			}
			set
			{
				this.SetPropertyValue(() => this.ContactName, value);
			}
		}

		public string ContactText
		{
			get
			{
				return this.GetPropertyValue(() => this.ContactText);
			}
			set
			{
				this.SetPropertyValue(() => this.ContactText, value);
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
