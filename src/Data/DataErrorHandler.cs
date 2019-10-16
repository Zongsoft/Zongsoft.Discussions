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
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Common;

namespace Zongsoft.Community.Data
{
	public class DataErrorHandler : IEventHandler<DataAccessErrorEventArgs>
	{
		public void Handle(object source, DataAccessErrorEventArgs args)
		{
			if(args.Exception is DataConflictException conflict && conflict.Key != null)
			{
				conflict.Key = Zongsoft.Resources.ResourceUtility.GetString("Data:" + conflict.Key);
			}
		}

		void IEventHandler.Handle(object source, object args)
		{
			if(args is DataAccessErrorEventArgs error)
				this.Handle(source, error);
		}
	}
}
