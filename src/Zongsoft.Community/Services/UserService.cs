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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Services;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	public class UserService : ServiceBase<UserProfile>
	{
		#region 成员字段
		private string _basePath;
		private IUserProvider _userProvider;
		#endregion

		#region 构造函数
		public UserService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
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

		[ServiceDependency]
		public IUserProvider UserProvider
		{
			get
			{
				return _userProvider;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_userProvider = value;
			}
		}
		#endregion

		#region 公共方法
		public IEnumerable<History> GetHistories(uint userId, Paging paging = null)
		{
			if(userId == 0)
				userId = this.EnsureCredential().UserId;

			return this.DataAccess.Select<History>(Condition.Equal("UserId", userId), "Thread", paging);
		}

		public int GetMessageTotalCount(uint userId = 0)
		{
			if(userId == 0)
				userId = this.EnsureCredential().UserId;

			return this.DataAccess.Count<Message.MessageUser>(Condition.Equal("UserId", userId));
		}

		public int GetMessageUnreadCount(uint userId = 0)
		{
			if(userId == 0)
				userId = this.EnsureCredential().UserId;

			return this.DataAccess.Count<Message.MessageUser>(Condition.Equal("UserId", userId) & Condition.Equal("IsRead", false));
		}

		public IEnumerable<Message> GetMessages(uint userId = 0, bool? isRead = null, Paging paging = null)
		{
			if(userId == 0)
				userId = this.EnsureCredential().UserId;

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
			return this.UserProvider.SetAvatar(userId, avatar);
		}

		public bool SetPhotoPath(uint userId, string path)
		{
			return this.DataAccess.Update(this.Name, new { PhotoPath = path }, Condition.Equal("UserId", userId)) > 0;
		}
		#endregion

		#region 重写方法
		protected override UserProfile OnGet(ICondition condition, string scope, object state)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "User";

			//调用基类同名方法
			var profile = base.OnGet(condition, scope, state);

			if(profile == null)
				return null;

			if(profile.User == null)
				profile.User = this.UserProvider.GetUser(profile.UserId);

			return profile;
		}

		protected override int OnDelete(ICondition condition, string[] cascades, object state)
		{
			throw new NotSupportedException("Not supporte the delete user operation.");
		}

		protected override int OnInsert(DataDictionary<UserProfile> data, string scope, object state)
		{
			//获取用户导航属性值
			data.TryGet(p => p.User, (key, user) =>
			{
				//默认设置用户状态为可用
				user.Status = UserStatus.Active;

				//如果未显式指定用户的命名空间，则使用当前用户的命名空间
				if(string.IsNullOrWhiteSpace(user.Namespace))
				{
					user.Namespace = this.EnsureCredential().User.Namespace;
				}

				//创建基础用户账户
				if(!this.UserProvider.CreateUser(user, user.Name.Trim().ToLowerInvariant()))
					throw new InvalidOperationException($"The '{user.Name}' user create failed.");

				//更新用户编号
				data.Set(p => p.UserId, user.UserId);
			});

			//调用基类同名方法（新增用户配置信息）
			return base.OnInsert(data, scope, state);
		}

		protected override int OnUpdate(DataDictionary<UserProfile> data, ICondition condition, string scope, object state)
		{
			//如果没有指定用户编号或指定的用户编号为零，则显式指定为当前用户编号
			if(!data.TryGet(p => p.UserId, out var userId) || userId == 0)
				data.Set(p => p.UserId, userId = this.EnsureCredential().UserId);

			//获取用户导航属性值
			User user = data.Get(p => p.User, null);

			if(user != null && user.HasChanges())
			{
				//确认用户编号是否有效
				if(user.UserId == 0)
					user.UserId = userId;

				//更新用户数据（注意，只更新指定范围内的字段，以免覆盖掉其他字段）
				this.UserProvider.UpdateUsers(new[] { user }, "!, " + string.Join(",", user.GetChangedProperties().Keys));
			}

			//调用基类同名方法
			return base.OnUpdate(data, condition, scope, state);
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
