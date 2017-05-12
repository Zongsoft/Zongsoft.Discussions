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
	/// 表示版主的业务实体类。
	/// </summary>
	public class Moderator : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private uint _siteId;
		private ushort _forumId;
		private uint _userId;
		#endregion

		#region 构造函数
		public Moderator()
		{
		}

		public Moderator(uint siteId, ushort forumId, uint userId)
		{
			this.SiteId = siteId;
			this.ForumId = forumId;
			this.UserId = userId;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置版主所属站点编号。
		/// </summary>
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

		/// <summary>
		/// 获取或设置版主所属论坛编号。
		/// </summary>
		public ushort ForumId
		{
			get
			{
				return _forumId;
			}
			set
			{
				this.SetPropertyValue(() => this.ForumId, ref _forumId, value);
			}
		}

		/// <summary>
		/// 获取或设置版主的用户编号。
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
		/// 获取或设置版主所属的论坛对象。
		/// </summary>
		public Forum Forum
		{
			get
			{
				return this.GetPropertyValue(() => this.Forum);
			}
			set
			{
				this.SetPropertyValue(() => this.Forum, value);
			}
		}

		/// <summary>
		/// 获取或设置版主的用户信息。
		/// </summary>
		public UserProfile User
		{
			get
			{
				return this.GetPropertyValue(() => this.User);
			}
			set
			{
				this.SetPropertyValue(() => this.User, value);
			}
		}
		#endregion
	}
}
