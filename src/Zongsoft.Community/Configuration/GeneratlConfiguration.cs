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

using Zongsoft.Options;
using Zongsoft.Options.Configuration;

namespace Zongsoft.Community.Configuration
{
	public class GeneratlConfiguration : OptionConfigurationElement, IConfiguration
	{
		#region 常量定义
		private const string XML_BASEPATH_ATTRIBUTE = "basePath";
		#endregion

		#region 公共属性
		[OptionConfigurationProperty(XML_BASEPATH_ATTRIBUTE, OptionConfigurationPropertyBehavior.IsRequired)]
		public string BasePath
		{
			get
			{
				return (string)this[XML_BASEPATH_ATTRIBUTE];
			}
			set
			{
				if(string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException();

				this[XML_BASEPATH_ATTRIBUTE] = value;
			}
		}
		#endregion

		#region 公共方法
		public virtual string GetSitePath(uint siteId)
		{
			return Zongsoft.IO.Path.Combine(this.BasePath, "site-" + siteId.ToString());
		}
		#endregion
	}
}
