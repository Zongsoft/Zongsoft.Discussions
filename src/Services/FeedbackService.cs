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
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSearcher("Subject,ContactName,ContactText")]
	public class FeedbackService : DataService<Feedback>
	{
		#region 构造函数
		public FeedbackService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 重写方法
		protected override Feedback OnGet(ICondition condition, ISchema schema, IDictionary<string, object> states, out IPaginator paginator)
		{
			//调用基类同名方法
			var feedback = base.OnGet(condition, schema, states, out paginator);

			if(feedback == null)
				return null;

			//如果内容类型是外部文件（即非嵌入格式），则读取文件内容
			if(!Utility.IsContentEmbedded(feedback.ContentType))
				feedback.Content = Utility.ReadTextFile(feedback.Content);

			return feedback;
		}

		protected override void OnValidate(Method method, IDataDictionary<Feedback> data)
		{
			if(method.IsWriting)
			{
				//更新内容及内容类型
				var contentFile = Utility.SetContent(data, () => this.GetContentFilePath(data.GetValue(p => p.FeedbackId)));
			}

			base.OnValidate(method, data);
		}

		protected override int OnInsert(IDataDictionary<Feedback> data, ISchema schema, IDictionary<string, object> states)
		{
			//更新内容及内容类型
			var contentFile = Utility.SetContent(data, () => this.GetContentFilePath(data.GetValue(p => p.FeedbackId)));

			try
			{
				//调用基类同名方法
				var count = base.OnInsert(data, schema, states);

				if(count < 1)
				{
					//如果新增记录失败则删除刚创建的内容文件
					if(contentFile != null && contentFile.Length > 0)
						Utility.DeleteFile(contentFile);
				}

				return count;
			}
			catch
			{
				//删除新建的内容文件
				if(contentFile != null && contentFile.Length > 0)
					Utility.DeleteFile(contentFile);

				throw;
			}
		}

		protected override int OnUpdate(IDataDictionary<Feedback> data, ICondition condition, ISchema schema, IDictionary<string, object> states)
		{
			//更新内容及内容类型
			Utility.SetContent(data, () => this.GetContentFilePath(data.GetValue(p => p.FeedbackId)));

			//调用基类同名方法
			return base.OnUpdate(data, condition, schema, states);
		}

		protected override int OnUpsert(IDataDictionary<Feedback> data, ISchema schema, IDictionary<string, object> states)
		{
			//更新内容及内容类型
			var contentFile = Utility.SetContent(data, () => this.GetContentFilePath(data.GetValue(p => p.FeedbackId)));

			try
			{
				//调用基类同名方法
				var count = base.OnUpsert(data, schema, states);

				if(count < 1)
				{
					//如果更新记录失败则删除刚创建的内容文件
					if(contentFile != null && contentFile.Length > 0)
						Utility.DeleteFile(contentFile);
				}

				return count;
			}
			catch
			{
				//删除新建的内容文件
				if(contentFile != null && contentFile.Length > 0)
					Utility.DeleteFile(contentFile);

				throw;
			}
		}
		#endregion

		#region 虚拟方法
		protected virtual string GetContentFilePath(ulong feedbackId)
		{
			return Utility.GetFilePath($"feedbacks/feedback-{feedbackId.ToString()}.txt");
		}
		#endregion
	}
}
