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
	public interface ThreadConditional : IEntity
	{
		#region 公共属性
		[Conditional("Name")]
		string Key
		{
			get; set;
		}

		string Subject
		{
			get; set;
		}

		ThreadStatus? Status
		{
			get; set;
		}

		Range<DateTime> StatusTimestamp
		{
			get; set;
		}

		bool? Disabled
		{
			get; set;
		}

		bool? Visible
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

		bool? IsPinned
		{
			get; set;
		}

		bool? IsValued
		{
			get; set;
		}

		bool? IsGlobal
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
}
