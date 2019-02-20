﻿/*
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
	[DataSequence("PostId", 100000)]
	[DataSearchKey("Thread,ThreadId:ThreadId")]
	public class PostService : DataService<IPost>
	{
		#region 构造函数
		public PostService(Zongsoft.Services.IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}
		#endregion

		#region 公共方法
		public bool Upvote(ulong postId, byte value = 1)
		{
			if(value == 0)
				value = 1;

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				this.DataAccess.Delete<PostVoting>(Condition.Equal("PostId", postId) & Condition.Equal("UserId", this.User.UserId));
				this.DataAccess.Insert(new PostVoting(postId, this.User.UserId, (sbyte)Math.Min(value, (byte)100))
				{
					UserName = this.User.FullName,
					UserAvatar = this.User.Avatar,
				});

				//如果帖子投票统计信息更新成功
				if(this.SetPostVotes(postId))
				{
					//提交事务
					transaction.Commit();

					//返回成功
					return true;
				}
			}

			return false;
		}

		public bool Downvote(ulong postId, byte value = 1)
		{
			if(value == 0)
				value = 1;

			using(var transaction = new Zongsoft.Transactions.Transaction())
			{
				this.DataAccess.Delete<PostVoting>(Condition.Equal("PostId", postId) & Condition.Equal("UserId", this.User.UserId));
				this.DataAccess.Insert(new PostVoting(postId, this.User.UserId, (sbyte)-Math.Min(value, (byte)100))
				{
					UserName = this.User.FullName,
					UserAvatar = this.User.Avatar,
				});

				//如果帖子投票统计信息更新成功
				if(this.SetPostVotes(postId))
				{
					//提交事务
					transaction.Commit();

					//返回成功
					return true;
				}
			}

			return false;
		}

		public IEnumerable<PostVoting> GetUpvotes(ulong postId, Paging paging = null)
		{
			return this.DataAccess.Select<PostVoting>(Condition.Equal("PostId", postId) & Condition.GreaterThan("Value", 0), paging);
		}

		public IEnumerable<PostVoting> GetDownvotes(ulong postId, Paging paging = null)
		{
			return this.DataAccess.Select<PostVoting>(Condition.Equal("PostId", postId) & Condition.LessThan("Value", 0), paging);
		}

		public IEnumerable<PostComment> GetComments(ulong postId, Paging paging = null)
		{
			return this.DataAccess.Select<PostComment>(Condition.Equal("PostId", postId), paging, Sorting.Descending("SerialId"));
		}
		#endregion

		#region 重写方法
		protected override IPost OnGet(ICondition condition, ISchema schema, object state, out IPaginator paginator)
		{
			//调用基类同名方法
			var post = base.OnGet(condition, schema, state, out paginator);

			if(post == null)
				return null;

			//如果内容类型是外部文件（即非嵌入格式），则读取文件内容
			if(!Utility.IsContentEmbedded(post.ContentType))
				post.Content = Utility.ReadTextFile(post.Content);

			return post;
		}

		protected override int OnInsert(IDataDictionary<IPost> data, ISchema schema, object state)
		{
			string filePath = null;

			//获取原始的内容类型
			var rawType = data.GetValue(p => p.ContentType, null);

			//调整内容类型为嵌入格式
			data.SetValue(p => p.ContentType, Utility.GetContentType(rawType, true));

			//尝试更新帖子内容
			data.TryGetValue(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//设置内容文件的存储路径
				filePath = this.GetContentFilePath(data.GetValue(p => p.PostId), data.GetValue(p => p.ContentType));

				//将内容文本写入到文件中
				Utility.WriteTextFile(filePath, value);

				//更新内容文件的存储路径
				data.SetValue(p => p.Content, filePath);

				//更新内容类型为非嵌入格式（即外部文件）
				data.SetValue(p => p.ContentType, Utility.GetContentType(data.GetValue(p => p.ContentType), false));
			});

			//定义附加数据是否为关联的主题对象
			var thread = state as IDataDictionary<IThread>;

			if(thread != null)
			{
				//判断当前用户是否是新增主题所在论坛的版主
				var isModerator = this.ServiceProvider.ResolveRequired<ForumService>().IsModerator(thread.GetValue(p => p.SiteId), thread.GetValue(p => p.ForumId));

				if(isModerator)
				{
					data.SetValue(p => p.IsApproved, true);
				}
				else
				{
					var forum = this.DataAccess.Select<IForum>(Condition.Equal("SiteId", thread.GetValue(p => p.SiteId)) & Condition.Equal("ForumId", thread.GetValue(p => p.ForumId))).FirstOrDefault();

					if(forum == null)
						throw new InvalidOperationException("The specified forum is not existed about the new thread.");

					data.SetValue(p => p.IsApproved, forum.ApproveEnabled ? false : true);
				}
			}

			try
			{
				using(var transaction = new Zongsoft.Transactions.Transaction())
				{
					//调用基类同名方法
					var count = base.OnInsert(data, schema.Include(nameof(IPost.Attachments)), state);

					if(count > 0)
					{
						//更新发帖人的关联帖子统计信息
						//注意：只有当前帖子不是主题贴才需要更新对应的统计信息
						if(thread == null)
							this.SetMostRecentPost(data);

						//提交事务
						transaction.Commit();
					}
					else
					{
						//如果新增记录失败则删除刚创建的文件
						if(filePath != null && filePath.Length > 0)
							Utility.DeleteFile(filePath);
					}

					return count;
				}
			}
			catch
			{
				//删除新建的文件
				if(filePath != null && filePath.Length > 0)
					Utility.DeleteFile(filePath);

				throw;
			}
		}

		protected override int OnUpdate(IDataDictionary<IPost> data, ICondition condition, ISchema schema, object state)
		{
			//更新内容到文本文件中
			data.TryGetValue(p => p.Content, (key, value) =>
			{
				if(string.IsNullOrWhiteSpace(value) || value.Length < 500)
					return;

				//根据当前反馈编号，获得其对应的内容文件存储路径
				var filePath = this.GetContentFilePath(data.GetValue(p => p.PostId), data.GetValue(p => p.ContentType));

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

			//尝试更新当前帖子的附件集
			data.TryGetValue(p => p.Attachments, (key, attachments) =>
			{
				if(attachments == null)
					return;

				var postId = data.GetValue(p => p.PostId);

				this.DataAccess.Delete<PostAttachment>(Condition.Equal("PostId", postId));
				this.DataAccess.InsertMany(attachments.Where(p => p.PostId == postId));
			});

			return count;
		}
		#endregion

		#region 虚拟方法
		protected virtual string GetContentFilePath(ulong postId, string contentType)
		{
			return Utility.GetFilePath(string.Format("posts/post-{0}-{1}.txt", postId.ToString(), Zongsoft.Common.RandomGenerator.GenerateString()));
		}
		#endregion

		#region 私有方法
		private bool SetPostVotes(ulong postId)
		{
			//获取当前帖子的点赞总数，即统计帖子投票表中投票数大于零的记录数
			var upvotes = this.DataAccess.Count<PostVoting>(Condition.Equal("PostId", postId) & Condition.GreaterThan("Value", 0));

			//获取当前帖子的被踩总数，即统计帖子投票表中投票数小于零的记录数
			var downvotes = this.DataAccess.Count<PostVoting>(Condition.Equal("PostId", postId) & Condition.LessThan("Value", 0));

			//更新指定帖子的累计点赞总数和累计被踩总数
			return this.DataAccess.Update(this.DataAccess.Naming.Get<IPost>(), new
			{
				PostId = postId,
				TotalUpvotes = upvotes,
				TotalDownvotes = downvotes,
			}) > 0;
		}

		private bool SetMostRecentPost(IDataDictionary<IPost> data)
		{
			//注意：如果当前帖子是主题内容贴则不需要更新对应的统计信息
			if(data == null)
				return false;

			//如果当前帖子没有指定对应的主题编号，则返回失败
			var threadId = data.GetValue(p => p.ThreadId, (ulong)0);

			if(threadId == 0)
				return false;

			//如果当前帖子对应的主题是不存在的，则返回失败
			var thread = this.DataAccess.Select<IThread>(Condition.Equal("ThreadId", threadId)).FirstOrDefault();

			if(thread == null)
				return false;

			//递增新增贴所属的主题的累计回帖总数
			if(this.DataAccess.Increment<IThread>("TotalReplies", Condition.Equal("ThreadId", threadId)) < 0)
				return false;

			var userId = data.GetValue(p => p.CreatorId);
			var user = this.DataAccess.Select<IUserProfile>(Condition.Equal("UserId", userId)).FirstOrDefault();
			var count = 0;

			//更新当前帖子所属主题的最后回帖信息
			count += this.DataAccess.Update(this.DataAccess.Naming.Get<IThread>(), new
			{
				ThreadId = threadId,
				MostRecentPostId = data.GetValue(p => p.PostId),
				MostRecentPostTime = data.GetValue(p => p.CreatedTime),
				MostRecentPostAuthorId = userId,
				MostRecentPostAuthorName = user?.FullName,
				MostRecentPostAuthorAvatar = user?.Avatar,
			});

			//更新当前帖子所属论坛的最后回帖信息
			count += this.DataAccess.Update(this.DataAccess.Naming.Get<IForum>(), new
			{
				SiteId = thread.SiteId,
				ForumId = thread.ForumId,
				MostRecentPostId = data.GetValue(p => p.PostId),
				MostRecentPostTime = data.GetValue(p => p.CreatedTime),
				MostRecentPostAuthorId = userId,
				MostRecentPostAuthorName = user?.FullName,
				MostRecentPostAuthorAvatar = user?.Avatar,
			});

			//递增当前发帖人的累计回帖数，并且更新发帖人的最后回帖信息
			if(this.DataAccess.Increment<IUserProfile>("TotalPosts", Condition.Equal("UserId", data.GetValue(p => p.CreatorId))) > 0)
			{
				count += this.DataAccess.Update(this.DataAccess.Naming.Get<IUserProfile>(), new
				{
					UserId = data.GetValue(p => p.CreatorId),
					MostRecentPostId = data.GetValue(p => p.PostId),
					MostRecentPostTime = data.GetValue(p => p.CreatedTime),
				});
			}

			return count > 0;
		}
		#endregion
	}
}