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
	/// 表示消息接受人的业务实体类。
	/// </summary>
	public struct MessageUser
	{
		#region 构造函数
		public MessageUser(ulong messageId, uint userId, bool isRead = false)
		{
			this.MessageId = messageId;
			this.UserId = userId;
			this.IsRead = isRead;

			this.User = null;
			this.Message = null;
		}

		public MessageUser(uint userId)
		{
			this.MessageId = 0;
			this.UserId = userId;
			this.IsRead = false;

			this.User = null;
			this.Message = null;
		}
		#endregion

		public ulong MessageId
		{
			get;
			set;
		}

		public IMessage Message
		{
			get;
			set;
		}

		public uint UserId
		{
			get;
			set;
		}

		public IUserProfile User
		{
			get;
			set;
		}

		public bool IsRead
		{
			get;
			set;
		}
	}
}
