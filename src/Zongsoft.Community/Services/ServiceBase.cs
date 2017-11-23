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

using Zongsoft.Data;
using Zongsoft.Services;

namespace Zongsoft.Community.Services
{
	public abstract class ServiceBase<TEntity> : Zongsoft.Data.DataService<TEntity>
	{
		#region 成员字段
		private Configuration.IConfiguration _configuration;
		#endregion

		#region 构造函数
		protected ServiceBase(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		protected ServiceBase(string name, Zongsoft.Services.IServiceProvider serviceProvider) : base(name, serviceProvider)
		{
		}
		#endregion

		#region 公共属性
		[ServiceDependency]
		public Configuration.IConfiguration Configuration
		{
			get
			{
				return _configuration;
			}
			set
			{
				_configuration = value ?? throw new ArgumentNullException();
			}
		}
		#endregion

		#region 保护方法
		protected Security.Credential EnsureCredential(bool throwException = true)
		{
			//获取当前操作的用户身份标识
			var identity = this.Principal?.Identity;

			//如果用户身份标识获取成功并且身份验证已通过
			if(identity != null && identity.IsAuthenticated)
				return new Security.Credential(identity.Credential);

			if(throwException)
				throw new Zongsoft.Security.Membership.AuthorizationException();

			return null;
		}

		protected uint GetSiteId()
		{
			var credential = this.EnsureCredential(false);

			if(credential == null || credential.IsEmpty)
				return _configuration.SiteId;
			else
				return credential.SiteId;
		}
		#endregion

		#region 虚拟方法
		protected virtual void EnsureRequiredCondition(ref ICondition condition)
		{
			ICondition requires = Condition.Equal("SiteId", this.GetSiteId());

			if(condition == null)
				condition = requires;
			else
				condition = ConditionCollection.And(condition, requires);
		}

		protected virtual void EnsureDefaultValues(DataDictionary<TEntity> data)
		{
			//设置创建时间
			data.TrySet("CreatedTime", DateTime.Now);

			//确认当前操作的用户凭证（获取失败则抛出授权异常）
			var credential = this.EnsureCredential();

			if(credential != null && credential.User != null)
			{
				//尝试设置创建人编号
				data.TrySet("CreatorId", () => credential.UserId, value =>
				{
					return value == null || Zongsoft.Common.Convert.ConvertValue(value, 0) == 0;
				});

				//尝试设置所属站点编号
				data.TrySet("SiteId", () => this.GetSiteId(), value =>
				{
					return value == null || Zongsoft.Common.Convert.ConvertValue(value, 0) == 0;
				});
			}
		}
		#endregion

		#region 重写方法
		protected override TEntity OnGet(ICondition condition, string scope, object state)
		{
			//确认必需的查询条件
			this.EnsureRequiredCondition(ref condition);

			//调用基类同名方法
			return base.OnGet(condition, scope, state);
		}

		protected override IEnumerable<TEntity> OnSelect(ICondition condition, string scope, Paging paging, Sorting[] sortings, object state)
		{
			//确认必需的查询条件
			this.EnsureRequiredCondition(ref condition);

			if(sortings == null || sortings.Length == 0)
			{
				var keys = this.DataAccess.GetKey<TEntity>();

				if(keys != null && keys.Length > 0)
					sortings = new Sorting[] { Sorting.Descending(keys) };
			}

			//调用基类同名方法
			return base.OnSelect(condition, scope, paging, sortings, state);
		}

		protected override int OnInsert(DataDictionary<TEntity> data, string scope, object state)
		{
			//更新相关字段的默认值
			this.EnsureDefaultValues(data);

			//调用基类同名方法
			return base.OnInsert(data, scope, state);
		}

		protected override int OnInsertMany(IEnumerable<DataDictionary<TEntity>> items, string scope, object state)
		{
			foreach(var item in items)
			{
				//更新相关字段的默认值
				this.EnsureDefaultValues(item);
			}

			//调用基类同名方法
			return base.OnInsertMany(items, scope, state);
		}

		protected override int OnUpdate(DataDictionary<TEntity> data, ICondition condition, string scope, object state)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "!SiteId, !CreatorId, !CreatedTime";
			else
				scope += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdate(data, condition, scope, state);
		}

		protected override int OnUpdateMany(IEnumerable<DataDictionary<TEntity>> items, string scope, object state)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "!SiteId, !CreatorId, !CreatedTime";
			else
				scope += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdateMany(items, scope, state);
		}
		#endregion
	}
}
