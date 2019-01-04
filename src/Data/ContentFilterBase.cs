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
using System.Linq;
using System.Collections;

using Zongsoft.Data;

namespace Zongsoft.Community.Data
{
	public abstract class ContentFilterBase<TEntity> : DataAccessFilterBase where TEntity : class
	{
		#region 常量定义
		private readonly string DELETED_RESULTS;
		#endregion

		#region 私有变量
		private string _scope;
		#endregion

		#region 构造函数
		protected ContentFilterBase(string name, string scope = null) : base(new DataAccessMethod[] { DataAccessMethod.Delete }, name)
		{
			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

			_scope = scope;
			DELETED_RESULTS = "deleted:" + name.Trim().ToLowerInvariant();
		}
		#endregion

		#region 重写方法
		protected override void OnDeleting(DataDeleteContextBase context)
		{
			context.States[DELETED_RESULTS] = context.DataAccess.Select<TEntity>(context.Name, context.Condition, _scope, Paging.Disable).ToArray();
		}

		protected override void OnDeleted(DataDeleteContextBase context)
		{
			if(context.States.TryGetValue(DELETED_RESULTS, out var items))
				this.OnDeleted(items as IEnumerable);
		}
		#endregion

		#region 抽象方法
		protected abstract void DeleteContentFile(TEntity entity);
		#endregion

		#region 私有方法
		private void OnDeleted(IEnumerable items)
		{
			if(items == null)
				return;

			foreach(var item in items)
			{
				if(item != null && item is TEntity entity)
					this.DeleteContentFile(entity);
			}
		}
		#endregion
	}
}
