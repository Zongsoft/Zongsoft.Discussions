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
 * Copyright (C) 2015-2018 Zongsoft Corporation. All rights reserved.
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

namespace Zongsoft.Community.Models
{
	/// <summary>
	/// 表示帖子的业务实体接口。
	/// </summary>
	public interface IPost : Zongsoft.Data.IEntity
	{
		/// <summary>
		/// 获取或设置帖子编号，主键。
		/// </summary>
		ulong PostId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的站点编号。
		/// </summary>
		uint SiteId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题编号。
		/// </summary>
		ulong ThreadId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子所属的主题对象。
		/// </summary>
		IThread Thread
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容。
		/// </summary>
		string Content
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的内容类型。
		/// </summary>
		string ContentType
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示是否禁用。
		/// </summary>
		bool Disabled
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被审核通过。
		/// </summary>
		bool IsApproved
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否被锁定，如果锁定则不允许回复。
		/// </summary>
		bool IsLocked
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置一个值，表示帖子是否为精华帖。
		/// </summary>
		bool IsValued
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计点赞总数。
		/// </summary>
		uint TotalUpvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的累计被踩总数。
		/// </summary>
		uint TotalDownvotes
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的位置信息。
		/// </summary>
		string VisitorAddress
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置访问者的描述信息。
		/// </summary>
		string VisitorDescription
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者编号。
		/// </summary>
		uint CreatorId
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的创建时间。
		/// </summary>
		DateTime CreatedTime
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子作者对应的用户对象。
		/// </summary>
		IUserProfile Creator
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的回复集。
		/// </summary>
		IEnumerable<IPostComment> Comments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的附件集。
		/// </summary>
		IEnumerable<PostAttachment> Attachments
		{
			get; set;
		}

		/// <summary>
		/// 获取或设置帖子的投票（点赞和被踩）记录集。
		/// </summary>
		IEnumerable<IPostVoting> Votes
		{
			get; set;
		}

		/// <summary>
		/// 获取帖子的点赞记录集。
		/// </summary>
		IEnumerable<IPostVoting> Upvotes
		{
			//this.Votes.Where(p => p.Value > 0)
			get;
		}

		/// <summary>
		/// 获取帖子的被踩记录集。
		/// </summary>
		IEnumerable<IPostVoting> Downvotes
		{
			//this.Votes.Where(p => p.Value < 0);
			get;
		}
	}
}
