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
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Data
{
	public class FolderFilter : DataAccessFilterBase
	{
		#region 构造函数
		public FolderFilter() : base(nameof(IFolder))
		{
		}
		#endregion

		#region 重写方法
		protected override void OnSelected(DataSelectContextBase context)
		{
			//调用基类同名方法
			base.OnSelected(context);

			//设置查询结果的过滤器
			context.ResultFilter = (ctx, item) =>
			{
				var folder = item as IFolder;

				if(folder == null)
					return false;

				if(folder.Visiblity == Visiblity.Specifics)
				{
					var credential = ctx.Principal?.Identity?.Credential;

					if(credential == null || credential.IsEmpty)
						return false;

					return context.DataAccess.Exists<FolderUser>(
						Condition.Equal("FolderId", folder.FolderId) &
						Condition.Equal("UserId", credential.UserId));
				}

				return true;
			};
		}
		#endregion
	}
}
