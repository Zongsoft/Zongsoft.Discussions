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

namespace Zongsoft.Community.Services
{
	public abstract class ServiceBase<TEntity> : Zongsoft.Data.DataService<TEntity>
	{
		#region 构造函数
		protected ServiceBase(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		protected ServiceBase(string name, Zongsoft.Services.IServiceProvider serviceProvider) : base(name, serviceProvider)
		{
		}
		#endregion

		#region 保护属性
		protected Security.Credential Credential
		{
			get
			{
				if(this.Principal == null || this.Principal.Identity == null)
					return null;

				return new Security.Credential(base.Principal.Identity.Credential);
			}
		}
		#endregion

		#region 虚拟方法
		protected virtual bool EnsureRequiredCondition(ref ICondition condition)
		{
			bool isRequired = this.Credential != null && !this.Credential.InAdministrators;

			if(isRequired && !string.IsNullOrWhiteSpace(this.Credential.User.PrincipalId))
			{
				var requires = Condition.Equal("SiteId", this.Credential.User.PrincipalId);

				if(condition == null)
					condition = requires;
				else
					condition = new ConditionCollection(ConditionCombination.And, requires, condition);
			}

			return isRequired;
		}

		protected virtual void EnsureDefaultValues(DataDictionary<TEntity> data)
		{
			//设置创建时间
			data.TrySet("CreatedTime", DateTime.Now);

			if(this.Credential != null && this.Credential.User != null)
			{
				//尝试设置创建人编号
				data.TrySet("CreatorId", () => this.Credential.UserId, value =>
				{
					return value == null || Zongsoft.Common.Convert.ConvertValue(value, 0) == 0;
				});

				//尝试设置所属站点编号
				data.TrySet("SiteId", () => this.Credential.User.PrincipalId, value =>
				{
					return value == null || Zongsoft.Common.Convert.ConvertValue(value, 0) == 0;
				});
			}
		}
		#endregion

		#region 重写方法
		protected override TEntity OnGet(ICondition condition, string scope)
		{
			//确认必需的查询条件
			this.EnsureRequiredCondition(ref condition);

			//调用基类同名方法
			return base.OnGet(condition, scope);
		}

		protected override IEnumerable<TEntity> OnSelect(ICondition condition, Grouping grouping, string scope, Paging paging, params Sorting[] sortings)
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
			return base.OnSelect(condition, grouping, scope, paging, sortings);
		}

		protected override int OnInsert(DataDictionary<TEntity> data, string scope)
		{
			//更新相关字段的默认值
			this.EnsureDefaultValues(data);

			//调用基类同名方法
			return base.OnInsert(data, scope);
		}

		protected override int OnInsertMany(IEnumerable<DataDictionary<TEntity>> items, string scope)
		{
			foreach(var item in items)
			{
				//更新相关字段的默认值
				this.EnsureDefaultValues(item);
			}

			//调用基类同名方法
			return base.OnInsertMany(items, scope);
		}

		protected override int OnUpdate(DataDictionary<TEntity> data, ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "!SiteId, !CreatorId, !CreatedTime";
			else
				scope += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdate(data, condition, scope);
		}

		protected override int OnUpdateMany(IEnumerable<DataDictionary<TEntity>> items, ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "!SiteId, !CreatorId, !CreatedTime";
			else
				scope += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdateMany(items, condition, scope);
		}
		#endregion
	}
}
