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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示论坛分组的业务实体类。
	/// </summary>
	public class ForumGroup : Zongsoft.Common.ModelBase
	{
		#region 成员字段
		private uint _siteId;
		private ushort _groupId;
		private ushort _sortOrder;
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置论坛组所属的站点编号，复合主键。
		/// </summary>
		public uint SiteId
		{
			get
			{
				return _siteId;
			}
			set
			{
				this.SetPropertyValue(() => this.SiteId, ref _siteId, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组的编号，复合主键。
		/// </summary>
		public ushort GroupId
		{
			get
			{
				return _groupId;
			}
			set
			{
				this.SetPropertyValue(() => this.GroupId, ref _groupId, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组所在站点中的排列序号。
		/// </summary>
		public ushort SortOrder
		{
			get
			{
				return _sortOrder;
			}
			set
			{
				this.SetPropertyValue(() => this.SortOrder, ref _sortOrder, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组的名称。
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetPropertyValue(() => this.Name);
			}
			set
			{
				this.SetPropertyValue(() => this.Name, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组的图标。
		/// </summary>
		public string Icon
		{
			get
			{
				return this.GetPropertyValue(() => this.Icon);
			}
			set
			{
				this.SetPropertyValue(() => this.Icon, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组的描述文本。
		/// </summary>
		public string Description
		{
			get
			{
				return this.GetPropertyValue(() => this.Description);
			}
			set
			{
				this.SetPropertyValue(() => this.Description, value);
			}
		}

		/// <summary>
		/// 获取或设置论坛组中的论坛集。
		/// </summary>
		public IEnumerable<Forum> Forums
		{
			get
			{
				return this.GetPropertyValue(() => this.Forums);
			}
			set
			{
				this.SetPropertyValue(() => this.Forums, value);
			}
		}
		#endregion
	}
}
