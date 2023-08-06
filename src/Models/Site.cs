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
using System.Collections.Generic;

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示站点的实体类。
	/// </summary>
	public abstract class Site
	{
		#region 普通属性
		/// <summary>获取或设置站点编号。</summary>
		public abstract uint SiteId { get; set; }
		/// <summary>获取或设置站点代号。</summary>
		public abstract string SiteNo { get; set; }
		/// <summary>获取或设置站点名称。</summary>
		public abstract string Name { get; set; }
		/// <summary>获取或设置站点域名。</summary>
		public abstract string Host { get; set; }
		/// <summary>获取或设置站点图标。</summary>
		public abstract string Icon { get; set; }
		/// <summary>获取或设置所属领域。</summary>
		public abstract string Domain { get; set; }
		/// <summary>获取或设置描述信息。</summary>
		public abstract string Description { get; set; }
		#endregion

		#region 集合属性
		/// <summary>获取或设置论坛组集合。</summary>
		public abstract ICollection<ForumGroup> ForumGroups { get; set; }
		/// <summary>获取或设置论坛集合。</summary>
		public abstract ICollection<Forum> Forums { get; set; }
		#endregion
	}

	/// <summary>
	/// 表示站点查询条件的实体类。
	/// </summary>
	public abstract class SiteCriteria : CriteriaBase
	{
		/// <summary>获取或设置站点代号。</summary>
		public abstract string SiteNo { get; set; }
		/// <summary>获取或设置站点名称。</summary>
		public abstract string Name { get; set; }
		/// <summary>获取或设置站点域名。</summary>
		public abstract string Host { get; set; }
		/// <summary>获取或设置所属领域。</summary>
		public abstract string Domain { get; set; }
	}
}