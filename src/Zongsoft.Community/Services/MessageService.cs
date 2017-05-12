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
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("MessageId", 100000)]
	[DataSearchKey("Status:Stauts", "From,FromId:FromId", "To,ToId:ToId", "Key:Subject")]
	public class MessageService : ServiceBase<Message>
	{
		#region 构造函数
		public MessageService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 重写方法
		protected override Message OnGet(ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "From, From.User, To, To.User";

			//调用基类同名方法
			return base.OnGet(condition, scope);
		}
		#endregion
	}
}
