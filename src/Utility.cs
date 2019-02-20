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
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Zongsoft.IO;
using Zongsoft.Services;
using Zongsoft.Security;
using Zongsoft.Security.Membership;

namespace Zongsoft.Community
{
	internal static class Utility
	{
		private const string EMBEDDED_TYPE_SUFFIX = "+embedded";

		public static bool IsContentEmbedded(string type)
		{
			if(string.IsNullOrWhiteSpace(type))
				return true;

			return type.TrimEnd().EndsWith(EMBEDDED_TYPE_SUFFIX, StringComparison.OrdinalIgnoreCase);
		}

		public static string GetContentType(string rawType, bool embedded)
		{
			if(string.IsNullOrWhiteSpace(rawType))
				rawType = "text/plain";
			else
				rawType = rawType.Trim();

			if(embedded)
			{
				if(rawType.EndsWith(EMBEDDED_TYPE_SUFFIX))
					return rawType;
				else
					return rawType + EMBEDDED_TYPE_SUFFIX;
			}
			else
			{
				if(rawType.EndsWith(EMBEDDED_TYPE_SUFFIX))
					return rawType.Substring(0, rawType.Length - EMBEDDED_TYPE_SUFFIX.Length);
				else
					return rawType;
			}
		}

		public static void DeleteFile(string path)
		{
			if(string.IsNullOrWhiteSpace(path))
				return;

			try
			{
				Zongsoft.IO.FileSystem.File.DeleteAsync(path);
			}
			catch { }
		}

		public static string ReadTextFile(string path)
		{
			if(string.IsNullOrWhiteSpace(path))
				throw new ArgumentNullException(nameof(path));

			try
			{
				if(!FileSystem.File.Exists(path))
					return string.Empty;

				using(var stream = FileSystem.File.Open(path, FileMode.Open, FileAccess.Read))
				{
					using(var reader = new StreamReader(stream, System.Text.Encoding.UTF8))
					{
						return reader.ReadToEnd();
					}
				}
			}
			catch
			{
				return string.Empty;
			}
		}

		public static bool WriteTextFile(string path, string content)
		{
			if(string.IsNullOrWhiteSpace(path))
				throw new ArgumentNullException(nameof(path));

			if(string.IsNullOrWhiteSpace(content))
				return false;

			using(var stream = FileSystem.File.Open(path, FileMode.Create, FileAccess.Write))
			{
				using(var writer = new StreamWriter(stream, System.Text.Encoding.UTF8))
				{
					writer.Write(content);
				}
			}

			return true;
		}

		public static string GetFilePath(string relativePath = null)
		{
			return GetFilePath(0, 0, relativePath);
		}

		public static string GetFilePath(uint siteId, string relativePath = null)
		{
			return GetFilePath(siteId, 0, relativePath);
		}

		/// <summary>
		/// 获取特定规则的文件存储路径。
		/// </summary>
		/// <param name="siteId">指定的站点编号，零表示当前用户所在的站点。</param>
		/// <param name="userId">指定的用户编号，零表示当前用户。</param>
		/// <param name="relativePath">相对路径，如果以斜杠符“/”打头表示忽略编号为零的层级。</param>
		/// <returns>返回的文件存储路径。</returns>
		/// <remarks>
		///		<list type="list">
		///			<item>根目录下的指定子目录：<code>GetFilePath(0, 0, "/apps");</code></item>
		///	
		///			<item>指定站点下的子目录：<code>GetFilePath(1, 0, "/sub");</code></item>
		///			<item>指定站点下的当前用户子目录：<code>GetFilePath(1, 0, "sub");</code></item>
		///			<item>指定站点下的指定用户子目录：<code>GetFilePath(1, 100, "sub");</code></item>
		///	
		///			<item>指定用户下的子目录：<code>GetFilePath(0, 100, "/sub");</code></item>
		///			<item>当前站点下的当前用户子目录：<code>GetFilePath(0, 0, "sub");</code></item>
		///			<item>当前站点下的指定用户子目录：<code>GetFilePath(0, 100, "sub");</code></item>
		///		</list>
		/// </remarks>
		public static string GetFilePath(uint siteId, uint userId, string relativePath = null)
		{
			var basePath = ApplicationContext.Current.Options.GetOptionValue("/Community/General.BasePath") as string;

			if(string.IsNullOrWhiteSpace(basePath))
				return string.Empty;

			if(relativePath != null && relativePath.StartsWith("/"))
			{
				relativePath = relativePath.TrimStart('/');

				if(siteId == 0)
				{
					if(userId == 0)
						return Zongsoft.IO.Path.Combine(basePath, relativePath);
					else
						return Zongsoft.IO.Path.Combine(basePath, "user-" + userId.ToString(), relativePath);
				}
				else
				{
					if(userId == 0)
						return Zongsoft.IO.Path.Combine(basePath, "site-" + siteId.ToString(), relativePath);
					else
						return Zongsoft.IO.Path.Combine(basePath, "site-" + siteId.ToString(), "user-" + userId.ToString(), relativePath);
				}
			}

			if(siteId == 0 || userId == 0)
			{
				var principal = ApplicationContext.Current.Principal as Zongsoft.Security.CredentialPrincipal;

				if(principal != null && principal.Identity.IsAuthenticated)
				{
					if(userId == 0)
						userId = principal.Identity.Credential.User.UserId;

					if(siteId == 0)
					{
						if(principal.Identity.Credential.Parameters.TryGetValue("Zongsoft.Community.UserProfile", out var value) && value is Models.IUserProfile profile)
							siteId = profile.SiteId;
					}
				}
			}

			if(siteId == 0)
			{
				if(userId == 0)
					return Zongsoft.IO.Path.Combine(basePath, "anonymous", relativePath);
				else
					return Zongsoft.IO.Path.Combine(basePath, "user-" + userId.ToString(), relativePath);
			}
			else
			{
				if(userId == 0)
					return Zongsoft.IO.Path.Combine(basePath, "site-" + siteId.ToString(), relativePath);
				else
					return Zongsoft.IO.Path.Combine(basePath, "site-" + siteId.ToString(), "user-" + userId.ToString(), relativePath);
			}
		}
	}
}
