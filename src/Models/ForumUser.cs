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
 * Copyright (C) 2015-2018 Zongsoft Corporation. All rights reserved.
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
	/// 表示论坛用户的结构。
	/// </summary>
	public struct ForumUser
	{
		#region 成员字段
		private uint _siteId;
		private ushort _forumId;
		private uint _userId;
		private IUserProfile _user;
		private UserKind _userKind;
		#endregion

		#region 构造函数
		public ForumUser(uint siteId, ushort forumId, uint userId, UserKind userKind)
		{
			_siteId = siteId;
			_forumId = forumId;
			_userId = userId;
			_userKind = userKind;
			_user = null;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置站点编号。
		/// </summary>
		public uint SiteId
		{
			get
			{
				return _siteId;
			}
			set
			{
				_siteId = value;
			}
		}

		/// <summary>
		/// 获取或设置论坛编号。
		/// </summary>
		public ushort ForumId
		{
			get
			{
				return _forumId;
			}
			set
			{
				_forumId = value;
			}
		}

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
				_userId = value;
			}
		}

		/// <summary>
		/// 获取或设置用户信息。
		/// </summary>
		public IUserProfile User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}

		/// <summary>
		/// 获取或设置当前论坛用户的所属种类。
		/// </summary>
		public UserKind UserKind
		{
			get
			{
				return _userKind;
			}
			set
			{
				_userKind = value;
			}
		}
		#endregion
	}
}
