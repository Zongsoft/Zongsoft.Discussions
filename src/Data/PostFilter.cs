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
using System.Collections;

using Zongsoft.Data;

namespace Zongsoft.Community.Data
{
	public class PostFilter : DataAccessFilterBase
	{
		#region 构造函数
		public PostFilter() : base(nameof(Models.Post), DataAccessMethod.Select)
		{
		}
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取当前安全主体对应的用户。
		/// </summary>
		public Models.UserProfile User
		{
			get
			{
				if(Zongsoft.Services.ApplicationContext.Current?.Principal is Zongsoft.Security.CredentialPrincipal principal &&
				  principal.Identity.IsAuthenticated &&
				  principal.Identity.Credential.HasParameters &&
				  principal.Identity.Credential.Parameters.TryGetValue("Zongsoft.Community.UserProfile", out var parameter))
					return parameter as Models.UserProfile;

				return null;
			}
		}
		#endregion

		#region 重写方法
		protected override void OnSelecting(DataSelectContextBase context)
		{
			base.OnSelecting(context);

			//设置结果过滤器
			context.ResultFilter = this.OnResultFilter;
		}
		#endregion

		#region 结果过滤
		private bool OnResultFilter(DataSelectContextBase context, ref object data)
		{
			var dictionary = DataDictionary.GetDictionary<Models.Post>(data);

			if(dictionary.TryGetValue(p => p.Approved, out var approved) && !approved &&
			  (!context.Principal.Identity.IsAuthenticated || context.Principal.Identity.Credential.User.UserId != dictionary.GetValue(p => p.CreatorId, 0U)))
			{
				dictionary.TrySetValue(p => p.Content, string.Empty);
			}
			else if(dictionary.TryGetValue(p => p.Content, out var content) && dictionary.TryGetValue(p => p.ContentType, out var contentType))
			{
				if(!Utility.IsContentEmbedded(contentType))
					dictionary.SetValue(p => p.Content, Utility.ReadTextFile(content));
			}

			return true;
		}
		#endregion
	}
}
