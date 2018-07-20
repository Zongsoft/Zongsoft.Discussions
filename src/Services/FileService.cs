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

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("Community:FileId", 100000)]
	[DataSearchKey("Key:Name")]
	public class FileService : ServiceBase<IFile>
	{
		#region 构造函数
		public FileService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
        #endregion

        #region 公共方法
        public string GetDirectory(uint? folderId = null)
        {
            if(folderId.HasValue)
                return Utility.GetFilePath("/folders/{folderId}/");
            else
                return Utility.GetFilePath("files/");
        }
        #endregion

        #region 重写方法
        protected override IFile OnGet(ICondition condition, string scope, object state)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			return base.OnGet(condition, scope, state);
		}

		protected override int OnInsert(DataDictionary<IFile> data, string scope, object state)
		{
			var filePath = data.Get(p => p.Path);

			try
			{
				//调用基类同名方法
				var count = base.OnInsert(data, scope, state);

				if(count < 1)
				{
					//如果新增记录失败则删除刚创建的文件
					if(filePath != null && filePath.Length > 0)
						Utility.DeleteFile(filePath);
				}

				return count;
			}
			catch
			{
				//删除新建的文件
				if(filePath != null && filePath.Length > 0)
					Utility.DeleteFile(filePath);

				throw;
			}
		}

		protected override int OnUpdate(DataDictionary<IFile> data, ICondition condition, string scope, object state)
		{
			throw new NotSupportedException();
		}
		#endregion
	}
}
