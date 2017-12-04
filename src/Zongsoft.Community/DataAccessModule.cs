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

using Zongsoft.Data;
using Zongsoft.ComponentModel;
using Zongsoft.Community.Models;

namespace Zongsoft.Community
{
	/// <summary>
	/// 提供 Zongsoft.Community™ 社区系统数据访问名映射的模块。
	/// </summary>
	public class DataAccessModule : IApplicationModule
	{
		#region 常量定义
		internal const string SECURITY_USER = "Security.User";

		internal const string COMMUNITY_FEEDBACK = "Community.Feedback";
		internal const string COMMUNITY_MESSAGE = "Community.Message";
		internal const string COMMUNITY_MESSAGEUSER = "Community.MessageUser";
		internal const string COMMUNITY_FILE = "Community.File";
		internal const string COMMUNITY_FOLDER = "Community.Folder";
		internal const string COMMUNITY_FOLDERUSER = "Community.FolderUser";
		internal const string COMMUNITY_FORUMGROUP = "Community.ForumGroup";
		internal const string COMMUNITY_FORUM = "Community.Forum";
		internal const string COMMUNITY_FORUMUSER = "Community.ForumUser";
		internal const string COMMUNITY_POST = "Community.Post";
		internal const string COMMUNITY_POSTVOTING = "Community.PostVoting";
		internal const string COMMUNITY_POSTCOMMENT = "Community.PostComment";
		internal const string COMMUNITY_POSTATTACHMENT = "Community.PostAttachment";
		internal const string COMMUNITY_HISTORY = "Community.History";
		internal const string COMMUNITY_THREAD = "Community.Thread";
		internal const string COMMUNITY_USERPROFILE = "Community.UserProfile";
		#endregion

		#region 公共属性
		public virtual string Name
		{
			get
			{
				return this.GetType().FullName;
			}
		}
		#endregion

		#region 初始方法
		public void Initialize(ApplicationContextBase context)
		{
			var namings = context.ServiceFactory.Default.ResolveAll<IDataAccessNaming>();

			if(namings != null)
			{
				foreach(var naming in namings)
				{
					if(naming != null)
						this.Map(naming);
				}
			}

			var dataAccesses = context.ServiceFactory.Default.ResolveAll<IDataAccess>();

			if(dataAccesses != null)
			{
				foreach(var dataAccess in dataAccesses)
				{
					if(dataAccess != null && dataAccess.Naming != null)
						this.Map(dataAccess.Naming);
				}
			}
		}
		#endregion

		#region 映射方法
		private void Map(Zongsoft.Data.IDataAccessNaming naming)
		{
			naming.Map<File>(COMMUNITY_FILE);
			naming.Map<Folder>(COMMUNITY_FOLDER);
			naming.Map<Folder.FolderUser>(COMMUNITY_FOLDERUSER);
			naming.Map<Feedback>(COMMUNITY_FEEDBACK);
			naming.Map<Forum>(COMMUNITY_FORUM);
			naming.Map<Forum.ForumUser>(COMMUNITY_FORUMUSER);
			naming.Map<ForumGroup>(COMMUNITY_FORUMGROUP);
			naming.Map<History>(COMMUNITY_HISTORY);
			naming.Map<Message>(COMMUNITY_MESSAGE);
			naming.Map<Message.MessageUser>(COMMUNITY_MESSAGEUSER);
			naming.Map<Post>(COMMUNITY_POST);
			naming.Map<Post.PostVoting>(COMMUNITY_POSTVOTING);
			naming.Map<Post.PostComment>(COMMUNITY_POSTCOMMENT);
			naming.Map<Post.PostAttachment>(COMMUNITY_POSTATTACHMENT);
			naming.Map<Thread>(COMMUNITY_THREAD);
			naming.Map<UserProfile>(COMMUNITY_USERPROFILE);
		}
		#endregion
	}
}
