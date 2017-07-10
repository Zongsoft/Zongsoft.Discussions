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
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Zongsoft.IO;
using Zongsoft.Data;

namespace Zongsoft.Community
{
	internal static class Utility
	{
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
	}
}
