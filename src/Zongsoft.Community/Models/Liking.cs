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
	/// 表示帖子点赞的实体类。
	/// </summary>
	public class Liking : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private ulong _postId;
		private uint _userId;
		private byte _points;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public Liking()
		{
			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置被点赞的帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				this.SetPropertyValue(() => this.PostId, ref _postId, value);
			}
		}

		/// <summary>
		/// 获取或设置点赞的用户编号。
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
		/// 获取或设置赞助的积分。
		/// </summary>
		public byte Points
		{
			get
			{
				return _points;
			}
			set
			{
				this.SetPropertyValue(() => this.Points, ref _points, value);
			}
		}

		/// <summary>
		/// 获取或设置点赞的时间。
		/// </summary>
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
