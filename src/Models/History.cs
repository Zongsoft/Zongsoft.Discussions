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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示用户浏览记录的实体类。
	/// </summary>
	public class History : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private uint _userId;
		private ulong _threadId;
		private Thread _thread;
		private uint _count;
		private DateTime _firstViewedTime;
		private DateTime _mostRecentViewedTime;
		#endregion

		#region 构造函数
		public History()
		{
			this.Count = 1;
			this.FirstViewedTime = this.MostRecentViewedTime = DateTime.Now;
		}

		public History(uint userId, ulong threadId)
		{
			this.UserId = userId;
			this.ThreadId = threadId;
			this.Count = 1;
			this.FirstViewedTime = this.MostRecentViewedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置用户的编号。
		/// </summary>
		public uint UserId
		{
			get
			{
				return _userId;
			}
			set
			{
				this.SetPropertyValue(() => this.UserId, ref _userId, value);
			}
		}

		/// <summary>
		/// 获取或设置浏览的主题编号。
		/// </summary>
		public ulong ThreadId
		{
			get
			{
				return _threadId;
			}
			set
			{
				this.SetPropertyValue(() => this.ThreadId, ref _threadId, value);
			}
		}

		/// <summary>
		/// 获取或设置浏览的主题对象。
		/// </summary>
		public Thread Thread
		{
			get
			{
				return _thread;
			}
			set
			{
				this.SetPropertyValue(nameof(Thread), ref _thread, value);
			}
		}

		/// <summary>
		/// 获取或设置累计浏览次数。
		/// </summary>
		public uint Count
		{
			get
			{
				return _count;
			}
			set
			{
				this.SetPropertyValue(() => this.Count, ref _count, value);
			}
		}

		/// <summary>
		/// 获取或设置首次浏览的时间。
		/// </summary>
		public DateTime FirstViewedTime
		{
			get
			{
				return _firstViewedTime;
			}
			set
			{
				this.SetPropertyValue(() => this.FirstViewedTime, ref _firstViewedTime, value);
			}
		}

		/// <summary>
		/// 获取或设置最后浏览的时间。
		/// </summary>
		public DateTime MostRecentViewedTime
		{
			get
			{
				return _mostRecentViewedTime;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentViewedTime, ref _mostRecentViewedTime, value);
			}
		}
		#endregion
	}
}
