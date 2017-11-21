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
		public IEnumerable<Folder.FolderUser> GetUsers(uint folderId, UserKind? userKind = null, Paging paging = null)
		{
			ICondition conditions = Condition.Equal("FolderId", folderId);

			if(userKind.HasValue)
				conditions = ConditionCollection.And(conditions, Condition.Equal("UserKind", userKind.Value));

			return this.DataAccess.Select<Folder.FolderUser>(conditions, "User, User.User", paging);
		}

		public bool SetIcon(uint folderId, string icon)
		{
			if(string.IsNullOrWhiteSpace(icon))
				icon = null;

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

		protected override int OnInsert(DataDictionary<Folder> data, string scope, IDictionary<string, object> states)
        {
            using(var transaction = new Transactions.Transaction())
            {
                //调用基类同名方法
                var count = base.OnInsert(data, scope, states);

                if(count < 1)
                    return count;

                //获取新增的文件夹用户集，并尝试插入该用户集
                data.TryGet(p => p.Users, (key, users) =>
                {
                    if(users == null)
                        return;

                    this.DataAccess.InsertMany(users);
                });

                //提交事务
                transaction.Commit();

                //返回主表插入的记录数
                return count;
            }
		}

		protected override int OnUpdate(DataDictionary<Folder> data, ICondition condition, string scope)
        {
            using(var transaction = new Transactions.Transaction())
            {
                //调用基类同名方法
                var count = base.OnUpdate(data, condition, scope);

                if(count < 1)
                    return count;

                //获取新增的文件夹用户集，并尝试插入该用户集
                data.TryGet(p => p.Users, (key, users) =>
                {
                    if(users == null)
                        return;

					var folderId = data.Get(p => p.FolderId);

                    //首先清除该文件夹的所有用户集
                    this.DataAccess.Delete<Folder.FolderUser>(Condition.Equal("FolderId", folderId));

                    //新增该文件夹的用户集
                    this.DataAccess.InsertMany(users.Where(p => p.FolderId == folderId));
                });

                //提交事务
                transaction.Commit();

                //返回主表插入的记录数
                return count;
            }
        }
		#endregion
	}
}
