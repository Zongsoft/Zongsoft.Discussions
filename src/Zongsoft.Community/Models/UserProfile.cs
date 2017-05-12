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
	/// 表示社区子系统中的用户设置信息。
	/// </summary>
	public class UserProfile : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private uint _userId;
		private uint _siteId;
		private Gender _gender;
		private uint _totalPosts;
		private uint _totalThreads;
		private uint _totalInLikes;
		private uint _totalOutLikes;
		private ulong? _mostRecentPostId;
		private DateTime? _mostRecentPostTime;
		private uint? _mostRecentThreadId;
		private DateTime? _mostRecentThreadTime;
		private DateTime _createdTime;
		#endregion

		#region 构造函数
		public UserProfile(uint userId = 0, uint siteId = 0)
		{
			if(userId > 0)
				this.UserId = userId;

			if(siteId > 0)
				this.SiteId = siteId;

			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置用户编号。
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
		/// 获取或设置用户所属的站点编号。
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
		/// 获取或设置用户性别。
		/// </summary>
		public Gender Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				this.SetPropertyValue(() => this.Gender, ref _gender, value);
			}
		}

		/// <summary>
		/// 获取或设置用户的照片文件路径。
		/// </summary>
		public string PhotoPath
		{
			get
			{
				return this.GetPropertyValue(() => this.PhotoPath);
			}
			set
			{
				this.SetPropertyValue(() => this.PhotoPath, value);
			}
		}

		/// <summary>
		/// 获取或设置用户累计发布的帖子总数。
		/// </summary>
		public uint TotalPosts
		{
			get
			{
				return _totalPosts;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalPosts, ref _totalPosts, value);
			}
		}

		/// <summary>
		/// 获取或设置用户累积发布的主题总数。
		/// </summary>
		public uint TotalThreads
		{
			get
			{
				return _totalThreads;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalThreads, ref _totalThreads, value);
			}
		}

		/// <summary>
		/// 获取或设置用户累积收获的点赞数。
		/// </summary>
		public uint TotalInLikes
		{
			get
			{
				return _totalInLikes;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalInLikes, ref _totalInLikes, value);
			}
		}

		/// <summary>
		/// 获取或设置用户累积点赞的总数。
		/// </summary>
		public uint TotalOutLikes
		{
			get
			{
				return _totalOutLikes;
			}
			set
			{
				this.SetPropertyValue(() => this.TotalOutLikes, ref _totalOutLikes, value);
			}
		}

		/// <summary>
		/// 获取或设置用户最后回帖的帖子编号。
		/// </summary>
		public ulong? MostRecentPostId
		{
			get
			{
				return _mostRecentPostId;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostId, ref _mostRecentPostId, value);
			}
		}

		/// <summary>
		/// 获取或设置用户最后回帖的时间。
		/// </summary>
		public DateTime? MostRecentPostTime
		{
			get
			{
				return _mostRecentPostTime;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentPostTime, ref _mostRecentPostTime, value);
			}
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题编号。
		/// </summary>
		public uint? MostRecentThreadId
		{
			get
			{
				return _mostRecentThreadId;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentThreadId, ref _mostRecentThreadId, value);
			}
		}

		/// <summary>
		/// 获取或设置用户最新发布的主题标题。
		/// </summary>
		public string MostRecentThreadSubject
		{
			get
			{
				return this.GetPropertyValue(() => this.MostRecentThreadSubject);
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentThreadSubject, value);
			}
		}

		/// <summary>
		/// 获取或设置用户最新主题的发布时间。
		/// </summary>
		public DateTime? MostRecentThreadTime
		{
			get
			{
				return _mostRecentThreadTime;
			}
			set
			{
				this.SetPropertyValue(() => this.MostRecentThreadTime, ref _mostRecentThreadTime, value);
			}
		}

		/// <summary>
		/// 获取或设置用户配置信息的创建时间。
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

		#region 关联属性
		public Zongsoft.Security.Membership.User User
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
