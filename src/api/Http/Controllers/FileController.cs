/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
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
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Zongsoft.Web;
using Zongsoft.Security.Membership;
using Zongsoft.Community.Models;
using Zongsoft.Community.Services;
using Zongsoft.IO;

namespace Zongsoft.Community.Web.Http.Controllers
{
	[Authorization]
	[Area("Community")]
	[Route("[area]/Files")]
	public class FileController : ApiControllerBase<File, FileService>
	{
		#region 常量定义
		private static readonly DateTime EPOCH = new DateTime(2000, 1, 1);
		#endregion

		#region 成员字段
		private WebFileAccessor _accessor;
		#endregion

		#region 构造函数
		public FileController(IServiceProvider serviceProvider) : base(serviceProvider)
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

		protected override bool CanCreate => false;
		protected override bool CanDelete => false;
		protected override bool CanUpdate => false;
		#endregion

		#region 公共方法
		[HttpGet("{id}")]
		public IActionResult Download(string id)
		{
			var file = base.Get(id) as File;

			if(file == null || string.IsNullOrWhiteSpace(file.Path))
				return null;

			return _accessor.Read(file.Path);
		}

		[HttpPost("{id?}")]
		public async Task<IEnumerable<File>> Upload(uint? id = null)
		{
			var files = new List<File>();
			var infos = _accessor.Write(this.Request,
				                          this.DataService.GetDirectory(id),
			                              args => args.FileName = (DateTime.Now - EPOCH).Days.ToString() + "-" + Zongsoft.Common.Randomizer.GenerateString());

			await foreach(var info in infos)
			{
				if(info == null || !info.IsFile)
					continue;

				object name = null;

				if(info.HasProperties && !info.Properties.TryGetValue("FileName", out name))
					info.Properties.TryGetValue("DispositionName", out name);

				if(string.IsNullOrWhiteSpace(name as string))
					name = info.Name;

				var attachment = Zongsoft.Data.Model.Build<File>(file =>
				{
					file.FolderId = id.HasValue ? id.Value : 0;
					file.Name = info.Name;
					file.Path = (PathLocation)info.Path.Url;
					file.Type = info.Type;
					file.Size = (uint)Math.Max(0, info.Size);
				});

				if(this.DataService.Insert(attachment) > 0)
					files.Add(attachment);
				else
					await _accessor.Delete(info.Path.FullPath);
			}

			return files;
		}
		#endregion
	}
}
