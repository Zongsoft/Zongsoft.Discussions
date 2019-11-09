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
using System.Collections;

using Zongsoft.Data;

namespace Zongsoft.Community.Data
{
	public class ThreadFilter : DataAccessFilterBase
	{
		#region 构造函数
		public ThreadFilter() : base(nameof(Models.Thread), DataAccessMethod.Select)
		{
		}
		#endregion

		#region 重写方法
		protected override void OnSelecting(DataSelectContextBase context)
		{
			base.OnSelecting(context);

			//设置结果过滤器
			context.ResultFilter = this.OnResultFilter;
		}
		#endregion

		#region 结果过滤
		private bool OnResultFilter(DataSelectContextBase context, ref object data)
		{
			var dictionary = DataDictionary.GetDictionary<Models.Thread>(data);

			if(!dictionary.TryGetValue(p => p.Post, out var post) || post == null)
				return true;

			if(dictionary.TryGetValue(p => p.Approved, out var approved) && !approved &&
			  (!context.Principal.Identity.IsAuthenticated || context.Principal.Identity.Credential.User.UserId != dictionary.GetValue(p => p.CreatorId, 0U)))
			{
				post.Content = string.Empty;
			}
			else if(!string.IsNullOrEmpty(post.Content) && !string.IsNullOrEmpty(post.ContentType))
			{
				if(!Utility.IsContentEmbedded(post.ContentType))
					post.Content = Utility.ReadTextFile(post.Content);
			}

			return true;
		}
		#endregion
	}
}
