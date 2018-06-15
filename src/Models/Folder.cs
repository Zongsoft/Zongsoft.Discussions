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
using System.Collections.Generic;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示文件夹的业务实体类。
	/// </summary>
	public class Folder : Zongsoft.Common.ModelBase
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置文件夹编号。
		/// </summary>
		public uint FolderId
		{
			get
			{
				return this.GetPropertyValue<uint>(nameof(FolderId));
			}
			set
			{
				this.SetPropertyValue(nameof(FolderId), value);
			}
		}

		/// <summary>
		/// 获取或设置文件夹名称。
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
		/// 获取或设置名称的拼音。
		/// </summary>
		public string PinYin
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(PinYin));
			}
			set
			{
				this.SetPropertyValue(nameof(PinYin), value);
			}
		}

		/// <summary>
		/// 获取或设置图标名。
		/// </summary>
		public string Icon
		{
			get
			{
				return this.GetPropertyValue<string>(nameof(Icon));
			}
			set
			{
				this.SetPropertyValue(nameof(Icon), value);
			}
		}

		/// <summary>
		/// 获取或设置所属站点编号。
		/// </summary>
		public uint SiteId
		{
			get
			{
				return this.GetPropertyValue<uint>(nameof(SiteId));
			}
			set
			{
				this.SetPropertyValue(nameof(SiteId), value);
			}
		}

		/// <summary>
		/// 获取或设置可见性。
		/// </summary>
		public Visiblity Visiblity
		{
			get
			{
				return this.GetPropertyValue<Visiblity>(nameof(Visiblity));
			}
			set
			{
				this.SetPropertyValue(nameof(Visiblity), value);
			}
		}

		/// <summary>
		/// 获取或设置可访问性。
		/// </summary>
		public Accessibility Accessibility
		{
			get
			{
				return this.GetPropertyValue<Accessibility>(nameof(Accessibility));
			}
			set
			{
				this.SetPropertyValue(nameof(Accessibility), value);
			}
		}

		/// <summary>
		/// 获取或设置创建人编号。
		/// </summary>
		public uint CreatorId
		{
			get
			{
				return this.GetPropertyValue<uint>(nameof(CreatorId));
			}
			set
			{
				this.SetPropertyValue(nameof(CreatorId), value);
			}
		}

		/// <summary>
		/// 获取或设置创建人对象。
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
		/// 获取或设置创建时间。
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
		#endregion

		#region 关联属性
		/// <summary>
		/// 获取或设置文件夹指定用户集合。
		/// </summary>
		public IEnumerable<FolderUser> Users
		{
			get
			{
				return this.GetPropertyValue<ICollection<FolderUser>>(nameof(Users));
			}
			set
			{
				this.SetPropertyValue(nameof(Users), value);
			}
		}
		#endregion

		#region 嵌套子类
		public class FolderUser
		{
			#region 成员字段
			private uint _folderId;
			private uint _userId;
			private UserKind _userKind;
			private UserProfile _user;
			#endregion

			#region 公共属性
			public uint FolderId
			{
				get
				{
					return _folderId;
				}
				set
				{
					_folderId = value;
				}
			}

			public uint UserId
			{
				get
				{
					return _userId;
				}
				set
				{
					_userId = value;
				}
			}

			public UserProfile User
			{
				get
				{
					return _user;
				}
				set
				{
					_user = value;
				}
			}

			public UserKind UserKind
			{
				get
				{
					return _userKind;
				}
				set
				{
					_userKind = value;
				}
			}
			#endregion
		}
		#endregion
	}
}
