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
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSearcher("Name")]
	public class ForumGroupService : DataService<ForumGroup>
	{
		#region 构造函数
		public ForumGroupService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public IEnumerable<Forum> GetForums(uint siteId, ushort groupId)
		{
			return this.DataAccess.Select<Forum>(
				Condition.Equal("SiteId", siteId) & Condition.Equal("GroupId", groupId),
				Paging.Disable);
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

		protected override ICondition OnValidate(Method method, ICondition condition)
		{
			//调用基类同名方法
			condition = base.OnValidate(method, condition);

			//获取当前用户凭证
			var credential = this.Credential;

			ICondition requires = null;

			//如果凭证为空或匿名用户则只能获取公共数据
			if(credential == null || credential.IsEmpty)
				requires = Condition.Equal("Visiblity", (byte)Visibility.Public) ;
			//else if(!credential.InAdministrators) //如果不是管理员则只能获取内部或公共数据
			//	requires = Condition.In("Visiblity", (byte)Visiblity.Internal, (byte)Visiblity.Public);

			if(requires == null)
				return condition;

			if(condition == null)
				return requires;
			else
				return ConditionCollection.And(condition, requires);
		}
		#endregion

		#region 私有方法
		//private bool CanVisiblity(Forum forum)
		//{
		//	if(forum == null)
		//		return false;

		//	var credential = this.EnsureCredential();

		//	switch(forum.Visiblity)
		//	{
		//		case Visiblity.Hidden:
		//			return forum.
		//	}
		//}
		#endregion
	}
}
