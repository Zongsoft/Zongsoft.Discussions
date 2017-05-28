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
		internal const string SECURITY_USERPROFILE = "Security.UserProfile";

		internal const string COMMUNITY_FEEDBACK = "Community.Feedback";
		internal const string COMMUNITY_FORUM = "Community.Forum";
		internal const string COMMUNITY_FORUMGROUP = "Community.ForumGroup";
		internal const string COMMUNITY_LIKING = "Community.Liking";
		internal const string COMMUNITY_MESSAGE = "Community.Message";
		internal const string COMMUNITY_MODERATOR = "Community.Moderator";
		internal const string COMMUNITY_POST = "Community.Post";
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
			var mappers = context.ServiceFactory.Default.ResolveAll<IDataAccessMapper>();

			if(mappers != null)
			{
				foreach(var mapper in mappers)
				{
					if(mapper != null)
						this.Map(mapper);
				}
			}

			var dataAccesses = context.ServiceFactory.Default.ResolveAll<IDataAccess>();

			if(dataAccesses != null)
			{
				foreach(var dataAccess in dataAccesses)
				{
					if(dataAccess != null && dataAccess.Mapper != null)
						this.Map(dataAccess.Mapper);
				}
			}
		}
		#endregion

		#region 映射方法
		private void Map(Zongsoft.Data.IDataAccessMapper mapper)
		{
			mapper.Map<Feedback>(COMMUNITY_FEEDBACK);
			mapper.Map<Forum>(COMMUNITY_FORUM);
			mapper.Map<ForumGroup>(COMMUNITY_FORUMGROUP);
			mapper.Map<History>(COMMUNITY_HISTORY);
			mapper.Map<Liking>(COMMUNITY_LIKING);
			mapper.Map<Message>(COMMUNITY_MESSAGE);
			mapper.Map<Moderator>(COMMUNITY_MODERATOR);
			mapper.Map<Post>(COMMUNITY_POST);
			mapper.Map<Thread>(COMMUNITY_THREAD);
			mapper.Map<UserProfile>(COMMUNITY_USERPROFILE);
		}
		#endregion
	}
}
