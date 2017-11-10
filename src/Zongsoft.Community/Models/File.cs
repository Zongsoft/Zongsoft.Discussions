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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示文件的实体类。
	/// </summary>
	public class File : Zongsoft.Common.ModelBase
	{
		#region 构造函数
		public File()
		{
			this.CreatedTime = DateTime.Now;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置文件编号。
		/// </summary>
		public ulong FileId
		{
			get
			{
				return this.GetPropertyValue<ulong>(nameof(FileId));
			}
			set
			{
				this.SetPropertyValue(nameof(FileId), value);
			}
		}

		/// <summary>
		/// 获取或设置所属的站点编号。
		/// </summary>
		public uint? SiteId
		{
			get
			{
				return this.GetPropertyValue<uint?>(nameof(SiteId));
			}
			set
			{
				this.SetPropertyValue(nameof(SiteId), value);
			}
		}

		/// <summary>
		/// 获取或设置用户所属文件夹编号。
		/// </summary>
		public uint? FolderId
		{
			get
			{
				return this.GetPropertyValue<uint?>(nameof(FolderId));
			}
			set
			{
				this.SetPropertyValue(nameof(FolderId), value);
			}
		}

		/// <summary>
		/// 获取或设置用户所属的文件夹对象。
		/// </summary>
		public Folder Folder
		{
			get
			{
				return this.GetPropertyValue<Folder>(nameof(Folder));
			}
			set
			{
				this.SetPropertyValue(nameof(Folder), value);
			}
		}

		/// <summary>
		/// 获取或设置附件的原始文件名。
		/// </summary>
		public string Name
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Name));
			}
			set
			{
				this.SetPropertyValue(nameof(Name), value);
			}
		}

		/// <summary>
		/// 获取或设置附件文件的路径。
		/// </summary>
		public string Path
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Path));
			}
			set
			{
				this.SetPropertyValue(nameof(Path), value);
			}
		}

		/// <summary>
		/// 获取附件文件的访问URL。
		/// </summary>
		public string Url
		{
			get
			{
				return Zongsoft.IO.FileSystem.GetUrl(this.Path);
			}
		}

		/// <summary>
		/// 获取或设置附件的文件类型(MIME类型)。
		/// </summary>
		public string Type
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Type));
			}
			set
			{
				this.SetPropertyValue(nameof(Type), value);
			}
		}

		/// <summary>
		/// 获取或设置附件文件的大小(单位：Byte)。
		/// </summary>
		public uint Size
		{
			get
			{
				return this.GetPropertyValue<uint>(nameof(Size));
			}
			set
			{
				this.SetPropertyValue(nameof(Size), value);
			}
		}

		/// <summary>
		/// 获取或设置附件的上传用户编号。
		/// </summary>
		public uint? CreatorId
		{
			get
			{
				return this.GetPropertyValue<uint?>(nameof(CreatorId));
			}
			set
			{
				this.SetPropertyValue(nameof(CreatorId), value);
			}
		}

		/// <summary>
		/// 获取或设置创建人的<see cref="Models.UserProfile"/>用户信息。
		/// </summary>
		public UserProfile Creator
		{
			get
			{
				return this.GetPropertyValue<UserProfile>(nameof(Creator));
			}
			set
			{
				this.SetPropertyValue(nameof(Creator), value);
			}
		}

		/// <summary>
		/// 获取或设置附件的上传时间。
		/// </summary>
		public DateTime CreatedTime
		{
			get
			{
				return this.GetPropertyValue<DateTime>(nameof(CreatedTime));
			}
			set
			{
				this.SetPropertyValue(nameof(CreatedTime), value);
			}
		}

		/// <summary>
		/// 获取或设置描述信息。
		/// </summary>
		public string Description
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Description));
			}
			set
			{
				this.SetPropertyValue(nameof(Description), value);
			}
		}
		#endregion
	}
}
