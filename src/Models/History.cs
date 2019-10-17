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
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示用户浏览记录的实体类。
	/// </summary>
	public interface IHistory
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置用户的编号。
		/// </summary>
		uint UserId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置浏览的主题编号。
		/// </summary>
		ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置浏览的主题对象。
		/// </summary>
		Thread Thread
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置累计浏览次数。
		/// </summary>
		uint Count
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置首次浏览的时间。
		/// </summary>
		DateTime FirstViewedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置最后浏览的时间。
		/// </summary>
		DateTime MostRecentViewedTime
		{
			get; set;
		}
		#endregion
	}

	/// <summary>
	/// 表示浏览记录查询条件的实体类。
	/// </summary>
	public abstract class HistoryConditional : ConditionalBase
	{
		public abstract ulong? ThreadId
		{
			get; set;
		}

		[Conditional("Count")]
		public abstract Range<uint>? Times
		{
			get; set;
		}

		[Conditional(typeof(TimestampConverter))]
		public abstract Range<DateTime>? Timestamp
		{
			get; set;
		}

		#region 嵌套子类
		private class TimestampConverter : ConditionalConverter
		{
			public override ICondition Convert(ConditionalConverterContext context)
			{
				var timestamp = (Range<DateTime>)context.Value;

				if(timestamp.IsEmpty)
					return null;

				return Condition.Between(nameof(IHistory.FirstViewedTime), timestamp) |
				       Condition.Between(nameof(IHistory.MostRecentViewedTime), timestamp);
			}
		}
		#endregion
	}
}
