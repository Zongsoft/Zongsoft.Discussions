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
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using Zongsoft.Web;
using Zongsoft.Web.Http;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization(AuthorizationMode.Identity)]
	public class FolderController : Zongsoft.Web.Http.HttpControllerBase<Folder, FolderConditional, FolderService>
	{
		private static readonly DateTime EPOCH = new DateTime(2000, 1, 1);

		#region 成员字段
		private WebFileAccessor _accessor;
		#endregion

		#region 构造函数
		public FolderController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共属性
		[Zongsoft.Services.ServiceDependency]
		public WebFileAccessor Accessor
		{
			get
			{
				return _accessor;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_accessor = value;
			}
		}
		#endregion

		#region 公共方法
		[HttpPost]
		public async Task<IEnumerable<File>> Upload(uint id)
		{
			var infos = await _accessor.Write(this.Request, this.DataService.GetFolderDirectory(id), args => args.FileName = (DateTime.Now - EPOCH).Seconds + "-" + Zongsoft.Common.RandomGenerator.GenerateString(8));
			var files = new List<File>();

			foreach(var info in infos)
			{
				if(info == null || !info.IsFile)
					continue;

				object name = null;

				if(info.HasProperties && !info.Properties.TryGetValue("FileName", out name))
					info.Properties.TryGetValue("DispositionName", out name);

				if(string.IsNullOrWhiteSpace(name as string))
					name = info.Name;

				var file = new File()
				{
					FolderId = id,
					Name = info.Name,
					Path = info.Path.Url,
					Type = info.Type,
					Size = (uint)Math.Max(0, info.Size),
				};

				if(this.DataService.Insert(file) > 0)
					files.Add(file);
				else
					await _accessor.Delete(info.Path.FullPath);
			}

			return files;
		}
		#endregion
	}
}
