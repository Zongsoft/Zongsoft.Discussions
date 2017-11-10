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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("Community:FolderId", 100000)]
	[DataSearchKey("Key:Name")]
	public class FolderService : ServiceBase<Folder>
	{
		#region 构造函数
		public FolderService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public string GetFolderDirectory(uint folderId)
		{
			return Zongsoft.IO.Path.Combine(
				this.Configuration.BasePath,
				$"site-{this.GetSiteId()}/folder-{folderId}/{DateTime.Today.ToString("yyyyMMdd")}/");
		}

		public IEnumerable<Folder.FolderUser> GetUsers(uint folderId, UserKind? userKind = null, Paging paging = null)
		{
			ICondition conditions = Condition.Equal("FolderId", folderId);

			if(userKind.HasValue)
				conditions = ConditionCollection.And(conditions, Condition.Equal("UserKind", userKind.Value));

			return this.DataAccess.Select<Folder.FolderUser>(conditions, "User, User.User", paging);
		}

		public bool SetIcon(uint folderId, string icon)
		{
			return this.DataAccess.Update(this.DataAccess.Naming.Get<Folder>(), new
			{
				FolderId = folderId,
				Icon = icon,
			}) > 0;
		}

		public bool SetVisiblity(uint folderId, Visiblity visiblity)
		{
			return this.DataAccess.Update(this.DataAccess.Naming.Get<Folder>(), new
			{
				FolderId = folderId,
				Visiblity = visiblity,
			}) > 0;
		}

		public bool SetAccessibility(uint folderId, Accessibility accessibility)
		{
			return this.DataAccess.Update(this.DataAccess.Naming.Get<Folder>(), new
			{
				FolderId = folderId,
				Accessibility = accessibility,
			}) > 0;
		}
		#endregion

		#region 重写方法
		protected override Folder OnGet(ICondition condition, string scope)
		{
			if(string.IsNullOrWhiteSpace(scope))
				scope = "Creator, Creator.User";

			//调用基类同名方法
			var folder = base.OnGet(condition, scope);

			if(folder == null)
				return null;

			return folder;
		}

		protected override int OnInsert(DataDictionary<Folder> data, string scope)
		{
			try
			{
				//调用基类同名方法
				var count = base.OnInsert(data, scope);

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

		protected override int OnUpdate(DataDictionary<Feedback> data, ICondition condition, string scope)
		{
			//更新内容到文本文件中
			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//根据当前反馈编号，获得其对应的内容文件存储路径
				var filePath = this.GetContentFilePath(data.Get(p => p.FeedbackId), data.Get(p => p.ContentType));

				//将反馈内容写入到对应的存储文件中
				Utility.WriteTextFile(filePath, value);

				//更新当前反馈的内容文件存储路径属性
				data.Set(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.Set(p => p.ContentType, Utility.GetContentType(data.Get(p => p.ContentType), false));
			});

			//调用基类同名方法
			var count = base.OnUpdate(data, condition, scope);

			if(count < 1)
				return count;

			return count;
		}
		#endregion
	}
}
