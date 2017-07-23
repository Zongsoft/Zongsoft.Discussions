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

using Zongsoft.Services;

namespace Zongsoft.Community.Services.Commands
{
	public class MessageSendCommand : CommandBase<CommandContext>
	{
		#region 成员字段
		private MessageService _service;
		#endregion

		#region 构造函数
		public MessageSendCommand() : base("Send")
		{
		}

		public MessageSendCommand(string name) : base(name)
		{
		}
		#endregion

		#region 公共属性
		[ServiceDependency(Provider = "Community")]
		public MessageService Service
		{
			get
			{
				return _service;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_service = value;
			}
		}
		#endregion

		#region 执行方法
		protected override object OnExecute(CommandContext context)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
