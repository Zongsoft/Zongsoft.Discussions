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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	public class FolderConditional : Zongsoft.Data.Conditional
	{
		#region 公共属性
		[Conditional("Name", "PinYin")]
		public string Key
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Key));
			}
			set
			{
				this.SetPropertyValue(nameof(Key), value);
			}
		}

		[Conditional("Name", "PinYin")]
		public string Name
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Name));
			}
			set
			{
				this.SetPropertyValue(nameof(Name), value);
			}
		}

		public Visiblity? Visiblity
		{
			get
			{
				return this.GetPropertyValue<Visiblity?>(nameof(Visiblity));
			}
			set
			{
				this.SetPropertyValue(nameof(Visiblity), value);
			}
		}

		public Accessibility? Accessibility
		{
			get
			{
				return this.GetPropertyValue<Accessibility?>(nameof(Accessibility));
			}
			set
			{
				this.SetPropertyValue(nameof(Accessibility), value);
			}
		}

		public uint? CreatorId
		{
			get
			{
				return this.GetPropertyValue<uint?>(nameof(CreatorId));
			}
			set
			{
				this.SetPropertyValue(nameof(CreatorId), value);
			}
		}

		public ConditionalRange<DateTime> CreatedTime
		{
			get
			{
				return this.GetPropertyValue<ConditionalRange<DateTime>>(nameof(CreatedTime));
			}
			set
			{
				this.SetPropertyValue(nameof(CreatedTime), value);
			}
		}
		#endregion
	}
}
