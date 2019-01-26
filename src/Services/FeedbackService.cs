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
using System.Linq;
using System.Collections.Generic;

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("Community:FeedbackId", 100000)]
	[DataSearchKey("Key:Subject,ContactName,ContactText")]
	public class FeedbackService : DataService<IFeedback>
	{
		#region 构造函数
		public FeedbackService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 重写方法
		protected override IFeedback OnGet(ICondition condition, ISchema schema, object state, out IPaginator paginator)
		{
			//调用基类同名方法
			var feedback = base.OnGet(condition, schema, state, out paginator);

			if(feedback == null)
				return null;

			//如果内容类型是外部文件（即非嵌入格式），则读取文件内容
			if(!Utility.IsContentEmbedded(feedback.ContentType))
				feedback.Content = Utility.ReadTextFile(feedback.Content);

			return feedback;
		}

		protected override int OnInsert(IDataDictionary<IFeedback> data, ISchema schema, object state)
		{
			string filePath = null;

			//获取原始的内容类型
			var rawType = data.GetValue(p => p.ContentType, null);

			//调整内容类型为嵌入格式
			data.SetValue(p => p.ContentType, Utility.GetContentType(rawType, true));

			data.TryGetValue(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//设置内容文件的存储路径
				filePath = this.GetContentFilePath(data.GetValue(p => p.FeedbackId), data.GetValue(p => p.ContentType));

				//将内容文本写入到文件中
				Utility.WriteTextFile(filePath, value);

				//更新内容文件的存储路径
				data.SetValue(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.SetValue(p => p.ContentType, Utility.GetContentType(data.GetValue(p => p.ContentType), false));
			});

			try
			{
				//调用基类同名方法
				var count = base.OnInsert(data, schema, state);

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

		protected override int OnUpdate(IDataDictionary<IFeedback> data, ICondition condition, ISchema schema, object state)
		{
			//更新内容到文本文件中
			data.TryGetValue(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//根据当前反馈编号，获得其对应的内容文件存储路径
				var filePath = this.GetContentFilePath(data.GetValue(p => p.FeedbackId), data.GetValue(p => p.ContentType));

				//将反馈内容写入到对应的存储文件中
				Utility.WriteTextFile(filePath, value);

				//更新当前反馈的内容文件存储路径属性
				data.SetValue(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.SetValue(p => p.ContentType, Utility.GetContentType(data.GetValue(p => p.ContentType), false));
			});

			//调用基类同名方法
			var count = base.OnUpdate(data, condition, schema, state);

			if(count < 1)
				return count;

			return count;
		}
		#endregion

		#region 虚拟方法
		protected virtual string GetContentFilePath(ulong feedbackId, string contentType)
		{
			return Utility.GetFilePath(string.Format("feedbacks/feedback-{0}.txt", feedbackId.ToString()));
		}
		#endregion
	}
}
