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
	/// 表示意见反馈的业务实体类。
	/// </summary>
	public interface IFeedback : Zongsoft.Data.IEntity
	{
		#region 公共属性
		ulong FeedbackId
		{
			get; set;
		}

		uint SiteId
		{
			get; set;
		}

		byte Kind
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

		string ContactName
		{
			get; set;
		}

		string ContactText
		{
			get; set;
		}

		DateTime CreatedTime
		{
			get; set;
		}
		#endregion
	}

	public interface IFeedbackConditional : IEntity
	{
		#region 公共属性
		[Conditional("Subject", "ContactName", "ContactText")]
		string Key
		{
			get; set;
		}

		byte? Kind
		{
			get; set;
		}

		string Subject
		{
			get; set;
		}

		string ContactName
		{
			get; set;
		}

		string ContactText
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
