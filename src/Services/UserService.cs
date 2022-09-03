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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Services;
using Zongsoft.Security;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[Service(nameof(UserService))]
	[DataService(typeof(UserProfileCriteria))]
	public class UserService : DataServiceBase<UserProfile>
	{
		#region 成员字段
		private string _basePath;
		#endregion

		#region 构造函数
		public UserService(IServiceProvider serviceProvider) : base(serviceProvider) { }
		#endregion

		#region 公共属性
		public string BasePath
		{
			get
			{
				return _basePath;
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException();

				_basePath = value.Trim();
			}
		}

		[ServiceDependency(IsRequired = true)]
		public IUserProvider<IUser> UserProvider { get; set; }
		#endregion

		#region 公共方法
		public IEnumerable<IHistory> GetHistories(uint userId, Paging paging = null)
		{
			if(userId == 0)
				userId = this.Principal.Identity.GetIdentifier<uint>();

			return this.DataAccess.Select<IHistory>(Condition.Equal("UserId", userId), "Thread", paging);
		}

		public int GetMessageTotalCount(uint userId = 0)
		{
			if(userId == 0)
				userId = this.Principal.Identity.GetIdentifier<uint>();

			return this.DataAccess.Count<Message.MessageUser>(Condition.Equal("UserId", userId));
		}

		public int GetMessageUnreadCount(uint userId = 0)
		{
			if(userId == 0)
				userId = this.Principal.Identity.GetIdentifier<uint>();

			return this.DataAccess.Count<Message.MessageUser>(Condition.Equal("UserId", userId) & Condition.Equal("IsRead", false));
		}

		public IEnumerable<Message> GetMessages(uint userId = 0, bool? isRead = null, Paging paging = null)
		{
			if(userId == 0)
				userId = this.Principal.Identity.GetIdentifier<uint>();

			var conditions = ConditionCollection.And(Condition.Equal("UserId", userId));

			if(isRead.HasValue)
				conditions.Add(Condition.Equal("IsRead", isRead.Value));

			return this.DataAccess.Select<Message.MessageUser>(conditions, "Message", paging).Select(p => p.Message);
		}

		public bool SetStatus(uint userId, UserStatus status)
		{
			return this.UserProvider.SetStatus(userId, status);
		}

		public bool SetAvatar(uint userId, string avatar)
		{
			return this.DataAccess.Update(this.Name, new
			{
				Avatar = avatar
			}, Condition.Equal(nameof(UserProfile.UserId), userId)) > 0;
		}

		public bool SetPhotoPath(uint userId, string path)
		{
			return this.DataAccess.Update(this.Name, new
			{
				PhotoPath = path
			}, Condition.Equal(nameof(UserProfile.UserId), userId)) > 0;
		}
		#endregion

		#region 重写方法
		protected override int OnInsert(IDataDictionary<UserProfile> data, ISchema schema, DataInsertOptions options)
		{
			//调用基类同名方法（新增用户配置信息）
			if(base.OnInsert(data, schema, options) > 0)
			{
				var user = Model.Build<IUser>(u =>
				{
					u.UserId = data.GetValue(p => p.UserId);
					u.Name = data.GetValue(p => p.Name);
				});

				user.Status = UserStatus.Active;

				//如果未显式指定用户的命名空间，则使用当前用户的命名空间
				if(string.IsNullOrWhiteSpace(user.Namespace))
				{
					user.Namespace = this.Principal.Identity.GetNamespace();
				}

				//创建基础用户账户
				if(!this.UserProvider.Create(user, user.Name.Trim().ToLowerInvariant()))
					throw new InvalidOperationException($"The '{user.Name}' user create failed.");

				//更新用户编号
				data.SetValue(p => p.UserId, user.UserId);
			}

			return 1;
		}

		protected override int OnUpdate(IDataDictionary<UserProfile> data, ICondition criteria, ISchema schema, DataUpdateOptions options)
		{
			//如果没有指定用户编号或指定的用户编号为零，则显式指定为当前用户编号
			if(!data.TryGetValue(p => p.UserId, out var userId) || userId == 0)
				data.SetValue(p => p.UserId, userId = this.Principal.Identity.GetIdentifier<uint>());

			//调用基类同名方法
			return base.OnUpdate(data, criteria, schema, options);
		}
		#endregion

		#region 文件路径
		public string GetFilePath(uint userId, string name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			if(string.Equals(name, "avatar", StringComparison.OrdinalIgnoreCase) || string.Equals(name, "photo", StringComparison.OrdinalIgnoreCase))
				return Zongsoft.IO.Path.Combine(_basePath, name.Trim() + "-" + userId.ToString());

			throw new ArgumentNullException($"Invalid '{name}' value of the name argument.");
		}
		#endregion
	}
}
