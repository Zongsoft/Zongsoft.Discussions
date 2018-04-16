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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	public class MessageConditional : Zongsoft.Data.Conditional
	{
		#region 公共属性
		[Conditional("Subject")]
		public string Key
		{
			get
			{
				return this.GetPropertyValue(() => this.Key);
			}
			set
			{
				this.SetPropertyValue(() => this.Key, value);
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

		public string MessageType
		{
			get
			{
				return this.GetPropertyValue(() => this.MessageType);
			}
			set
			{
				this.SetPropertyValue(() => this.MessageType, value);
			}
		}

		public string Source
		{
			get
			{
				return this.GetPropertyValue(() => this.Source);
			}
			set
			{
				this.SetPropertyValue(() => this.Source, value);
			}
		}

		public MessageStatus? Status
		{
			get
			{
				return this.GetPropertyValue(() => this.Status);
			}
			set
			{
				this.SetPropertyValue(() => this.Status, value);
			}
		}

		public ConditionalRange<DateTime> StatusTimestamp
		{
			get
			{
				return this.GetPropertyValue(() => this.StatusTimestamp);
			}
			set
			{
				this.SetPropertyValue(() => this.StatusTimestamp, value);
			}
		}

		public uint? CreatorId
		{
			get
			{
				return this.GetPropertyValue(() => this.CreatorId);
			}
			set
			{
				this.SetPropertyValue(() => this.CreatorId, value);
			}
		}

		public ConditionalRange<DateTime> CreatedTime
		{
			get
			{
				return this.GetPropertyValue(() => this.CreatedTime);
			}
			set
			{
				this.SetPropertyValue(() => this.CreatedTime, value);
			}
		}
		#endregion
	}
}
