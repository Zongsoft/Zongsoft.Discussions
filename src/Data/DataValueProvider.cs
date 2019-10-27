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
 * Copyright (C) 2015-2019 Zongsoft Corporation. All rights reserved.
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
using Zongsoft.Data.Metadata;

namespace Zongsoft.Community.Data
{
	public class DataValueProvider : IDataValueProvider
	{
		#region 静态字段
		private static readonly IDictionary<string, Func<IDataMutateContextBase, object>> _inserts;
		private static readonly IDictionary<string, Func<IDataMutateContextBase, object>> _updates;
		#endregion

		#region 静态构造
		static DataValueProvider()
		{
			_inserts = new Dictionary<string, Func<IDataMutateContextBase, object>>(StringComparer.OrdinalIgnoreCase)
			{
				{ "SiteId", GetSiteId },

				{ "CreatorId",  GetUserId },
				{ "CreatedTime", GetTimestamp },
				{ "Creation", GetTimestamp },
			};

			_updates = new Dictionary<string, Func<IDataMutateContextBase, object>>(StringComparer.OrdinalIgnoreCase)
			{
				{ "ModifierId",  GetUserId },
				{ "ModifiedTime", GetTimestamp },
				{ "Modification", GetTimestamp },
			};
		}
		#endregion

		#region 公共方法
		public bool TryGetValue(IDataMutateContextBase context, DataAccessMethod method, IDataEntityProperty property, out object value)
		{
			IDictionary<string, Func<IDataMutateContextBase, object>> provider = null;

			switch(method)
			{
				case DataAccessMethod.Insert:
					provider = _inserts;
					break;
				case DataAccessMethod.Update:
					provider = _updates;
					break;
			}

			if(provider != null && provider.TryGetValue(property.Name, out var factory))
			{
				value = factory(context);
				return true;
			}

			value = null;
			return false;
		}
		#endregion

		#region 私有方法
		private static object GetSiteId(IDataMutateContextBase context)
		{
			return GetUser()?.SiteId;
		}

		private static Models.UserProfile GetUser()
		{
			if(Zongsoft.Services.ApplicationContext.Current?.Principal is Zongsoft.Security.CredentialPrincipal principal)
				return principal.Identity?.Credential?.User as Models.UserProfile;

			return null;
		}

		private static object GetUserId(IDataMutateContextBase context)
		{
			return GetUser()?.UserId;
		}

		private static object GetTimestamp(IDataMutateContextBase context)
		{
			return DateTime.Now;
		}
		#endregion
	}
}
