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
 * Copyright (C) 2015-2018 Zongsoft Corporation. All rights reserved.
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
	/// 表示帖子附件的结构。
	/// </summary>
	public struct PostAttachment
	{
		#region 成员字段
		private ulong _postId;
		private ulong _fileId;
		private IFile _file;
		#endregion

		#region 构造函数
		public PostAttachment(ulong postId, ulong fileId)
		{
			_postId = postId;
			_fileId = fileId;
			_file = null;
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置帖子编号。
		/// </summary>
		public ulong PostId
		{
			get
			{
				return _postId;
			}
			set
			{
				_postId = value;
			}
		}

		/// <summary>
		/// 获取或设置帖子的文件编号。
		/// </summary>
		public ulong FileId
		{
			get
			{
				return _fileId;
			}
			set
			{
				_fileId = value;
			}
		}

		/// <summary>
		/// 获取或设置帖子的文件对象。
		/// </summary>
		public IFile File
		{
			get
			{
				return _file;
			}
			set
			{
				_file = value;
			}
		}
		#endregion
	}
}
