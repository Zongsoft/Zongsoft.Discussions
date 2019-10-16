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
 * Copyright (C) 2015-2019 Zongsoft Corporation. All rights reserved.
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
using System.Collections;

using Zongsoft.Data;
using Zongsoft.Data.Metadata;

namespace Zongsoft.Community.Data
{
	public class DataValidationFilter : DataAccessFilterBase
	{
		#region 构造函数
		public DataValidationFilter() :
			base(string.Empty,
				DataAccessMethod.Count,
				DataAccessMethod.Exists,
				DataAccessMethod.Delete,
				DataAccessMethod.Update,
				DataAccessMethod.Select,
				DataAccessMethod.Increment)
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取当前安全主体对应的用户。
		/// </summary>
		public Models.UserProfile User
		{
			get
			{
				return this.Principal.Identity?.Credential?.User as Models.UserProfile;
			}
		}
		#endregion

		#region 重写方法
		protected override void OnCounting(DataCountContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}

		protected override void OnExisting(DataExistContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}

		protected override void OnDeleting(DataDeleteContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}

		protected override void OnUpdating(DataUpdateContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}

		protected override void OnSelecting(DataSelectContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}

		protected override void OnIncrementing(DataIncrementContextBase context)
		{
			if(context.Entity.Properties.Contains("SiteId"))
				context.Condition = this.GetCondition(context.Condition);
		}
		#endregion

		#region 私有方法
		private ICondition GetCondition(ICondition condition)
		{
			if(condition is ConditionCollection conditions && conditions.Combination == ConditionCombination.And)
			{
				conditions.Add(Condition.Equal("SiteId", this.User.SiteId));
				return conditions;
			}

			return ConditionCollection.And(Condition.Equal("SiteId", this.User.SiteId), condition);
		}
		#endregion
	}
}
