# Web API 说明

### 文件处理

- [POST] `api/Community/File/{folderId}/Upload`
> 上传一个或多个文件到指定的文件夹中，其中`{folderId}`参数不指定或为零则表示不对应到特定的文件夹。


### 消息处理

- [GET] `api/Community/Message/{messageId}/Users?IsRead=[true|false]`
> 获取指定消息编号的接收用户集，其中`IsRead`参数可选。


### 用户信息

- [GET] `api/Community/User/{userId}/Count/message-unread`
> 获取指定用户编号的未读消息数。

- [GET] `api/Community/User/{userId}/Count/message-total`
> 获取指定用户编号的消息总数。

- [GET] `api/Community/User/{userId}/Messages?IsRead=[true|false]`
> 获取指定用户编号的消息记录，其中`IsRead`参数可选。

- [GET] `api/Community/User/{userId}/Histories`
> 获取指定用户编号的浏览历史记录。


### 论坛组与论坛

- [GET] `api/Community/ForumGroup`
> 获取当前登录用户所属站点中的论坛组列表；如果当前登录用户为管理员，则可选`{siteId}`参数来指定要获取的论坛组所属的站点编号。

- [GET] `api/Community/ForumGroup/{siteId}-{groupId}`
> 获取指定站点中特定论坛组的详细信息。

- [GET] `api/Community/ForumGroup/{siteId}-{groupId}/Forums`
> 获取指定站点中特定论坛组的论坛列表。

------------

- [GET] `api/Community/Forum`
> 获取当前登录用户所属站点中的论坛组列表；如果当前登录用户为管理员，则可选`{siteId}`参数来指定要获取的论坛所属的站点编号。

- [GET] `api/Community/Forum/{siteId}-{forumId}`
> 获取指定站点中特定论坛的详细信息。

- [GET] `api/Community/Forum/{siteId}-{forumId}/Moderators`
> 获取指定站点中特定论坛的版主列表。

- [GET] `api/Community/Forum/{siteId}/Globals`
> 获取指定站点中可见的全局主题列表。

- [GET] `api/Community/Forum/{siteId}-{forumId}/Pinneds`
> 获取指定论坛中被置顶的主题列表。

- [GET] `api/Community/Forum/{siteId}-{forumId}/Topmosts/{count}`
> 获取指定论坛中顶部显示的主题列表，`{count}`参数为全局或置顶主题的最大记录数，默认为10。
> 顶部显示列表即为“全局主题(Globals)”和“置顶主题(Pinneds)”，即顶部显示主题数量最多为`{count}`的两倍。

- [GET] `api/Community/Forum/{siteId}-{forumId}/Threads`
> 获取指定论坛中主题列表，注意：该列表不包含全局主题和置顶主题。

### 主题与帖子

- [GET] `api/Community/Thread/{threadId}`
> 获取指定编号的主题详细信息。

- [GET] `api/Community/Thread/{threadId}/Posts`
> 获取指定主题编号的顶级回帖列表。

- [POST] `api/Community/Thread`
> 发布一个新的主题，新增主题属性定义如下：。

```json
{
	"SiteId":1,
	"ForumId":101,
	"Subject":"主题的标题文本(必选)",
	"Summary":"主题的概要说明(可选)",
	"Post":{
		"Content":"主题的内容",
		"ContentType":"主题的内容类型（参考MIME标准，譬如：text/html）"
	}
}
```

- [PUT] `api/Community/Thread`
> 修改一个指定编号的主题内容，修改主题定义如下：

```json
{
	"ThreadId":100001,
	"Subject":"新的主题标题文本(可选)",
	"Summary":"新的主题概要说明(可选)",
	"Post":{
		"Content":"新的主题的内容(可选)",
		"ContentType":"新的主题的内容类型(可选)"
	}
}
```

- [DELETE] `api/Community/Thread/{threadId}`
> 删除指定编号的主题及其相关的所有回帖。

-------------

- [GET] `api/Community/Post/{postId}`
> 获取指定编号的帖子详细信息。

- [GET] `api/Community/Post/{postId}/Comments`
> 获取指定编号的帖子的回复列表。

- [POST] `api/Community/Post/{postId}/Upvote/{value}`
> 为指定编号的帖子点赞，其中`{value}`参数可选，默认为1。

- [POST] `api/Community/Post/{postId}/Downvote/{value}`
> 为指定编号的帖子点踩，其中`{value}`参数可选，默认为1。

- [POST] `api/Community/Post`
> 发布一个新的帖子，新增帖子属性定义如下：。

```json
{
	"SiteId":1,
	"ThreadId":100001,
	"ParentId":"父帖子编号(可选)",
	"Content":"帖子的内容",
	"ContentType":"帖子的内容类型（参考MIME标准，譬如：text/html）"
}
```

- [PUT] `api/Community/Post`
> 修改一个指定编号的帖子内容，修改帖子定义如下：

```json
{
	"PostId":100001,
	"Content":"新的帖子的内容(可选)",
	"ContentType":"新的帖子的内容类型(可选)"
}
```

- [DELETE] `api/Community/Post/{postId}`
> 删除指定编号的帖子及其相关的所有回复。

