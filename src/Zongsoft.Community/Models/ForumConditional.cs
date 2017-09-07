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
	public class ForumConditional : Zongsoft.Data.Conditional
	{
		#region 公共属性
		[Conditional("Name")]
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

		public string Name
		{
			get
			{
				return this.GetPropertyValue(() => this.Name);
			}
			set
			{
				this.SetPropertyValue(() => this.Name, value);
			}
		}

		public ForumVisiblity? Visiblity
		{
			get
			{
				return this.GetPropertyValue(() => this.Visiblity);
			}
			set
			{
				this.SetPropertyValue(() => this.Visiblity, value);
			}
		}

		public ForumAccessibility? Accessibility
		{
			get
			{
				return this.GetPropertyValue(() => this.Accessibility);
			}
			set
			{
				this.SetPropertyValue(() => this.Accessibility, value);
			}
		}

		public bool? IsPopular
		{
			get
			{
				return this.GetPropertyValue(() => this.IsPopular);
			}
			set
			{
				this.SetPropertyValue(() => this.IsPopular, value);
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
