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
	public class FileController : Zongsoft.Web.Http.HttpControllerBase<File, FileConditional, FileService>
	{
		#region 常量定义
		private static readonly DateTime EPOCH = new DateTime(2000, 1, 1);
		#endregion

		#region 成员字段
		private WebFileAccessor _accessor;
		#endregion

		#region 构造函数
		public FileController(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
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
		[HttpGet]
		public HttpResponseMessage Download(string id)
		{
			var file = base.Get(id) as File;

			if(file == null || string.IsNullOrWhiteSpace(file.Path))
				return null;

			var response = _accessor.Read(file.Path);

			if(response != null && response.Content != null && !string.IsNullOrWhiteSpace(file.Type))
				response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.Type);

			return response;
		}

		[HttpPost]
		public async Task<IEnumerable<File>> Upload(uint? id = null)
		{
			var files = new List<File>();
			var infos = await _accessor.Write(this.Request,
				                          this.DataService.GetDirectory(id),
			                              args => args.FileName = (DateTime.Now - EPOCH).Days.ToString() + "-" + Zongsoft.Common.RandomGenerator.GenerateString());

			foreach(var info in infos)
			{
				if(info == null || !info.IsFile)
					continue;

				object name = null;

				if(info.HasProperties && !info.Properties.TryGetValue("FileName", out name))
					info.Properties.TryGetValue("DispositionName", out name);

				if(string.IsNullOrWhiteSpace(name as string))
					name = info.Name;

				var attachment = new File()
				{
					FolderId = id.HasValue ? id.Value : 0,
					Name = info.Name,
					Path = info.Path.Url,
					Type = info.Type,
					Size = (uint)Math.Max(0, info.Size),
				};

				if(this.DataService.Insert(attachment) > 0)
					files.Add(attachment);
				else
					await _accessor.Delete(info.Path.FullPath);
			}

			return files;
		}

		public override File Post(File model)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		public override void Put(File model)
		{
			throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
		}

		public override void Patch(string id, IDictionary<string, object> data)
		{
			int mark = 0;
			object siteId, folderId, name, description;

			if(data.TryGetValue("SiteId", out siteId))
				mark = 1;
			if(data.TryGetValue("FolderId", out folderId))
				mark = 2;
			if(data.TryGetValue("Name", out name))
				mark &= 4;
			if(data.TryGetValue("Description", out description))
				mark &= 8;

			data.Clear();

			if((mark & 1) == 1)
				data["SiteId"] = siteId;
			if((mark & 2) == 2)
				data["FolderId"] = folderId;
			if((mark & 4) == 4)
				data["Name"] = name;
			if((mark & 8) == 8)
				data["Description"] = description;

			if(data.Count > 0)
				base.Patch(id, data);
		}
		#endregion
	}
}
