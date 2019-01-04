/*
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
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Services;

namespace Zongsoft.Community.Services
{
	public abstract class DataService<TEntity> : Zongsoft.Data.DataServiceBase<TEntity>
	{
		#region 成员字段
		private Configuration.IConfiguration _configuration;
		#endregion

		#region 构造函数
		protected DataService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		protected DataService(string name, Zongsoft.Services.IServiceProvider serviceProvider) : base(name, serviceProvider)
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

		/// <summary>
		/// 获取当前安全主体对应的用户。
		/// </summary>
		protected Models.UserProfile User
		{
			get
			{
				return this.Credential?.User?.Principal as Models.UserProfile;
			}
		}
		#endregion

		#region 验证方法
		protected override ICondition OnValidate(DataAccessMethod method, ICondition condition)
		{
			var user = this.User ?? throw new Zongsoft.Security.Membership.AuthorizationException();
			var requires = ConditionCollection.And(Condition.Equal("SiteId", user.SiteId));

			if(condition != null)
				requires.Add(condition);

			return requires;
		}

		protected override void OnValidate(DataAccessMethod method, IDataDictionary<TEntity> data)
		{
			var user = this.User ?? throw new Zongsoft.Security.Membership.AuthorizationException();

			switch(method)
			{
				case DataAccessMethod.Insert:
					//尝试设置所属站点编号
					data.TrySetValue("SiteId", user.SiteId);

					//尝试设置创建人编号
					data.TrySetValue("CreatorId", user.UserId, value => value == 0);

					//尝试设置创建时间
					data.TrySetValue("CreatedTime", DateTime.Now, value => value.Year <= 1900);
					break;
				case DataAccessMethod.Update:
					//尝试设置修改人编号
					data.TrySetValue("ModifierId", user.UserId, value => value == 0);

					//尝试设置修改时间
					data.TrySetValue("ModifiedTime", DateTime.Now, value => value.Year <= 1900);
					break;
			}
		}
		#endregion

		#region 重写方法
		protected override IEnumerable<TEntity> OnSelect(ICondition condition, string scope, Paging paging, Sorting[] sortings, object state)
		{
			if(sortings == null || sortings.Length == 0)
			{
				var keys = this.DataAccess.GetKey<TEntity>();

				if(keys != null && keys.Length > 0)
				{
					sortings = new Sorting[keys.Length];

					for(var i = 0; i < keys.Length; i++)
					{
						sortings[i] = Sorting.Descending(keys[i]);
					}
				}
			}

			//调用基类同名方法
			return base.OnSelect(condition, scope, paging, sortings, state);
		}

		protected override int OnUpdate(IDataDictionary<TEntity> data, ICondition condition, string schema, object state)
		{
			if(string.IsNullOrWhiteSpace(schema))
				schema = "!SiteId, !CreatorId, !CreatedTime";
			else
				schema += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdate(data, condition, schema, state);
		}

		protected override int OnUpdateMany(IEnumerable<IDataDictionary<TEntity>> items, string schema, object state)
		{
			if(string.IsNullOrWhiteSpace(schema))
				schema = "!SiteId, !CreatorId, !CreatedTime";
			else
				schema += ", !SiteId, !CreatorId, !CreatedTime";

			//调用基类同名方法
			return base.OnUpdateMany(items, schema, state);
		}
		#endregion
	}
}
