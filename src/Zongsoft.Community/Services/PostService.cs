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
	[DataSequence("PostId", 100000)]
	[DataSearchKey("Thread,ThreadId:ThreadId")]
	public class PostService : ServiceBase<Post>
	{
		#region 构造函数
		public PostService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 重写方法
		protected override Post OnGet(ICondition condition, string scope)
		{
			//调用基类同名方法
			var post = base.OnGet(condition, scope);

			if(post == null)
				return null;

			return post;
		}

		protected override IEnumerable<Post> OnSelect(ICondition condition, Grouping grouping, string scope, Paging paging, params Sorting[] sortings)
		{
			//调用基类同名方法
			var posts = base.OnSelect(condition, grouping, scope, paging, sortings);

			return posts;
		}
		#endregion
	}
}
