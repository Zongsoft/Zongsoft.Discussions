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

using Zongsoft.Data;
using Zongsoft.Community.Models;

namespace Zongsoft.Community.Services
{
	[DataSequence("Community:FeedbackId", 100000)]
	[DataSearchKey("Key:Subject,ContactName,ContactText")]
	public class FeedbackService : ServiceBase<Feedback>
	{
		#region 成员字段
		private Configuration.IConfiguration _configuration;
		#endregion

		#region 构造函数
		public FeedbackService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共属性
		public Configuration.IConfiguration Configuration
		{
			get
			{
				return _configuration;
			}
			set
			{
				if(value == null)
					throw new ArgumentNullException();

				_configuration = value;
			}
		}
		#endregion

		#region 重写方法
		protected override void EnsureDefaultValues(DataDictionary<Feedback> data)
		{
			//设置创建时间
			data.TrySet("CreatedTime", DateTime.Now);

			//尝试更新当前反馈的所属站点编号
			data.TrySet(p => p.SiteId, _ => this.GetSiteId(), value => value == 0);
		}

		protected override Feedback OnGet(ICondition condition, string scope)
		{
			//调用基类同名方法
			var feedback = base.OnGet(condition, scope);

			if(feedback != null && !string.IsNullOrWhiteSpace(feedback.ContentPath))
				feedback.Content = Utility.ReadTextFile(feedback.ContentPath);

			return feedback;
		}

		protected override int OnDelete(ICondition condition, string[] cascades)
		{
			var paths = this.Select(condition, "ContentPath").Select(p => p.ContentPath).ToArray();

			//调用基类同名方法
			var count = base.OnDelete(condition, cascades);

			if(count > 0)
			{
				foreach(var path in paths)
				{
					try
					{
						Zongsoft.IO.FileSystem.File.DeleteAsync(path);
					}
					catch { }
				}
			}

			return count;
		}

		protected override int OnInsert(DataDictionary<Feedback> data, string scope)
		{
			//调用基类同名方法
			var count = base.OnInsert(data, scope);

			if(count < 1)
				return count;

			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value))
					return;

				//设置内容文件的存储路径
				var filePath = this.GetContentFilePath(data.Get(p => p.FeedbackId));

				//将内容文本写入到文件中
				Utility.WriteTextFile(filePath, value);

				//更新内容文件的存储路径
				this.Update(new
				{
					FeedbackId = data.Get(p => p.FeedbackId),
					ContentPath = filePath,
				});
			});

			return count;
		}

		protected override int OnUpdate(DataDictionary<Feedback> data, ICondition condition, string scope)
		{
			//更新内容到文本文件中
			data.TryGet(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value))
					return;

				var filePath = data.Get(p => p.ContentPath, string.Empty);

				if(string.IsNullOrWhiteSpace(filePath))
				{
					//根据当前反馈编号，获得其对应的内容文件存储路径
					filePath = this.GetContentFilePath(data.Get(p => p.FeedbackId));

					//更新当前反馈的内容文件存储路径属性
					data.Set(p => p.ContentPath, filePath);
				}

				//将反馈内容写入到对应的存储文件中
				Utility.WriteTextFile(filePath, value);
			});

			//调用基类同名方法
			var count = base.OnUpdate(data, condition, scope);

			if(count < 1)
				return count;

			return count;
		}
		#endregion

		#region 私有方法
		private uint GetSiteId()
		{
			var credential = this.EnsureCredential(false);

			if(credential != null && credential.SiteId.HasValue)
				return credential.SiteId.Value;

			var configuration = this.Configuration;

			if(configuration == null)
				throw new InvalidOperationException("");

			return configuration.SiteId;
		}

		private string GetContentFilePath(ulong feedbackId)
		{
			var configuration = this.Configuration;

			if(configuration == null)
				throw new InvalidOperationException();

			var siteId = configuration.SiteId;
			var credential = this.EnsureCredential(false);

			if(credential != null && credential.SiteId.HasValue)
				siteId = credential.SiteId.Value;

			return Zongsoft.IO.Path.Combine(configuration.GetSitePath(siteId), "feedbacks/feedback-" + feedbackId.ToString() + ".txt");
		}
		#endregion
	}
}
