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
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("SiteId, GroupId", 101)]
	[DataSearchKey("Key:Name")]
	public class ForumsService : ServiceBase<ForumGroup>
	{
		#region 构造函数
		public ForumsService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 重写方法
		protected override ICondition GetKey(object[] values, out bool singleton)
		{
			if(values.Length == 1)
			{
				singleton = false;
				return Condition.Equal("SiteId", values[0]);
			}

			return base.GetKey(values, out singleton);
		}

		protected override ForumGroup OnGet(ICondition condition, string scope)
		{
			//调用基类同名方法
			var group = base.OnGet(condition, scope);

			if(group != null)
			{
				group.Forums = this.DataAccess.Select<Forum>(Condition.Equal("GroupId", group.GroupId), Paging.Disable, Sorting.Ascending("SortOrder"));
			}

			return group;
		}

		protected override IEnumerable<ForumGroup> OnSelect(ICondition condition, Grouping grouping, string scope, Paging paging, params Sorting[] sortings)
		{
			//调用基类同名方法
			var groups = base.OnSelect(condition, grouping, scope, paging, sortings);

			//获取所有论坛组的所有论坛
			var forums = this.DataAccess.Select<Forum>(Condition.In("GroupId", groups.Select(p => p.GroupId)), Paging.Disable).ToArray();

			foreach(var group in groups)
			{
				group.Forums = forums.Where(p => p.GroupId == group.GroupId).OrderBy(p => p.SortOrder);
			}

			return groups;
		}
		#endregion
	}
}
