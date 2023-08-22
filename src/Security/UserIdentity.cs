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
 * Copyright (C) 2015-2023 Zongsoft Corporation. All rights reserved.
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

using Zongsoft.Security;
using Zongsoft.Security.Membership;

namespace Zongsoft.Community.Security
{
	public class UserIdentity : UserIdentityBase<UserIdentity>
	{
		#region 常量定义
		public const string Scheme = "Zongsoft.Community";
		#endregion

		#region 公共属性
		/// <summary>获取或设置用户所属站点编号。</summary>
		public uint SiteId { get; set; }

		/// <summary>获取或设置用户绑定的邮箱地址。</summary>
		public string Email { get; set; }

		/// <summary>获取或设置用户绑定的手机号码。</summary>
		public string Phone { get; set; }

		/// <summary>获取或设置头像标识。</summary>
		public string Avatar { get; set; }

		/// <summary>获取或设置用户性别。</summary>
		public Models.Gender Gender { get; set; }

		/// <summary>获取或设置用户等级，数字越大等级越高。</summary>
		public byte Grade { get; set; }

		/// <summary>获取或设置用户累计发布的帖子总数。</summary>
		public uint TotalPosts { get; set; }

		/// <summary>获取或设置用户累积发布的主题总数。</summary>
		public uint TotalThreads { get; set; }
		#endregion

		#region 静态成员
		public static UserIdentity Current => Get(Scheme, identity => (UserIdentity)UserIdentityTransformer.Instance.Transform(identity));
		#endregion
	}
}