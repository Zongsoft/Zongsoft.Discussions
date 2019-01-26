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

using Zongsoft.Data;

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示文件的实体类。
	/// </summary>
	public interface IFile : Zongsoft.Data.IEntity
	{
		#region 公共属性
		/// <summary>
		/// 获取或设置文件编号。
		/// </summary>
		ulong FileId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置所属的站点编号。
		/// </summary>
		uint? SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户所属文件夹编号。
		/// </summary>
		uint? FolderId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置用户所属的文件夹对象。
		/// </summary>
		IFolder Folder
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件的原始文件名。
		/// </summary>
		string Name
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件文件的路径。
		/// </summary>
		string Path
		{
			get; set;
		}

		/// <summary>
		/// 获取附件文件的访问URL。
		/// </summary>
		[Zongsoft.Data.Entity.Property(Zongsoft.Data.Entity.PropertyImplementationMode.Extension, typeof(FileExtension))]
		string Url
		{
			get;
		}

		/// <summary>
		/// 获取或设置附件的文件类型(MIME类型)。
		/// </summary>
		string Type
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件文件的大小(单位：Byte)。
		/// </summary>
		uint Size
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件的上传用户编号。
		/// </summary>
		uint? CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置创建人的<see cref="Models.IUserProfile"/>用户信息。
		/// </summary>
		IUserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置附件的上传时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置描述信息。
		/// </summary>
		string Description
		{
			get; set;
		}
		#endregion
	}

	public interface IFileConditional : IEntity
	{
		#region 公共属性
		[Conditional("Name", "Description")]
		string Key
		{
			get; set;
		}

		ulong? FileId
		{
			get; set;
		}

		[Conditional("Name")]
		string Name
		{
			get; set;
		}

		string Type
		{
			get; set;
		}

		Range<uint> Size
		{
			get; set;
		}

		uint? FolderId
		{
			get; set;
		}

		uint? CreatorId
		{
			get; set;
		}

		Range<DateTime> CreatedTime
		{
			get; set;
		}
		#endregion
	}

	internal static class FileExtension
	{
		public static string GetUrl(IFile file)
		{
			return Zongsoft.IO.FileSystem.GetUrl(file.Path);
		}
	}
}
