
## Zongsoft.Community 社区系统


### 消息表 `Community.Message`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
MessageId | bigint | 8 | False | 主键，消息编号
SiteId | int | 4 | False | 站点编号
Subject | nvarchar | 100 | False | 消息主题
Content | nvarchar | 500 | False | 消息内容
ContentType | varchar | 50 | True | 内容类型(text/plain+embedded, text/html, application/json)
MessageType | varchar | 50 | True | 消息类型
Status | byte | 1 | False | 状态
StatusTimestamp | datetime | - | True | 状态更新时间
StatusDescription | nvarchar | 100 | True | 状态描述
CreatorId | int | 4 | False | 发送人编号(零表示系统消息)
CreatedTime | datetime | - | False | 创建时间


### 消息接收人员表 `Community.MessageMember`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
MessageId | bigint | 8 | False | 主键，消息编号
UserId | int | 4 | False | 主键，用户编号
Status | byte | 1 | False | 状态(0:None, 1:Read)
StatusTimestamp | datetime | - | True | 状态更新时间


### 意见反馈表 `Community.Feedback`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
FeedbackId | bigint | 8 | False | 主键，反馈编号
SiteId | int | 4 | False | 站点编号
Subject | nvarchar | 100 | False | 反馈标题
Content | nvarchar | 500 | False | 反馈内容
ContentType | varchar | 50 | True | 内容类型(text/plain+embedded, text/html, application/json)
ContactName | varchar | 50 | True | 联系人名
ContactText | varchar | 50 | True | 联系方式
CreatedTime | datetime | - | False | 反馈时间


### 论坛分组表 `Community.ForumGroup`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
SiteId | int | 4 | False | 主键，站点编号(所属企业)
GroupId | smallint | 2 | False | 主键，论坛分组编号
Name | nvarchar | 50 | False | 论坛组名
Icon | varchar | 100 | True | 显示图标
Visiblity | byte | 1 | False | 可见范围(0:禁用,即不可见; 1:站内用户可见; 2:所有人可见)
SortOrder | smallint | 2 | False | 排列顺序
Description | nvarchar | 500 | True | 描述信息


### 论坛表 `Community.Forum`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
SiteId | int | 4 | False | 主键，站点编号
ForumId | smallint | 2 | False | 主键，论坛编号
GroupId | smallint | 2 | False | 论坛组编号
Name | nvarchar | 50 | False | 论坛名称
Description | nvarchar | 500 | True | 描述文本
CoverPicturePath | varchar | 200 | True | 封面图片路径
SortOrder | smallint | 2 | False | 排列顺序
IsPopular | bool | - | False | 是否热门版块
ApproveEnabled | bool | - | False | 发帖是否需要审核
Visiblity | byte | 1 | False | 可见范围(0:禁用,即不可见; 1:站内用户可见; 2:所有人可见)
Accessibility | byte | 1 | False | 可访问性(0:无限制; 1:注册用户; 2:仅限版主)
TotalPosts | int | 4 | False | 累计帖子总数
TotalThreads | int | 4 | False | 累计主题总数
MostRecentThreadId | bigint | 8 | True | 最新主题的编号
MostRecentThreadSubject | nvarchar | 100 | True | 最新主题的标题
MostRecentThreadAuthorId | int | 4 | True | 最新主题的作者编号
MostRecentThreadAuthorName | nvarchar | 50 | True | 最新主题的作者名
MostRecentThreadAuthorAvatar | varchar | 150 | True | 最新主题的作者头像
MostRecentThreadTime | datetime | - | True | 最新主题的发布时间
MostRecentPostId | bigint | 8 | True | 最后回帖的帖子编号
MostRecentPostAuthorId | int | 4 | True | 最后回帖的作者编号
MostRecentPostAuthorName | nvarchar | 50 | True | 最后回帖的作者名
MostRecentPostAuthorAvatar | varchar | 150 | True | 最后回帖的作者头像
MostRecentPostTime | datetime | - | True | 最后回帖的时间
CreatedTime | datetime | - | False | 创建时间


### 版主表 `Community.Moderator`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
SiteId | int | 4 | False | 主键，站点编号
ForumId | smallint | 2 | False | 主键，论坛编号
UserId | int | 4 | False | 主键，用户编号


### 主题表 `Community.Thread`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
ThreadId | bigint | 8 | False | 主键，主题编号
SiteId | int | 4 | False | 所属站点编号
ForumId | smallint | 2 | False | 所属论坛编号
Subject | nvarchar | 100 | False | 文章标题
Summary | nvarchar | 500 | True | 文章摘要
Tags | nvarchar | 100 | True | 标签集
PostId | bigint | 8 | False | 内容帖子编号
CoverPicturePath | varchar | 200 | True | 封面图片路径
LinkUrl | varchar | 200 | True | 文章跳转链接
Status | byte | 1 | False | 状态(0:未发送, 1:发送中, 2:已发送, 3:已取消)
StatusTimestamp | datetime | - | False | 状态更新时间
StatusDescription | nvarchar | 100 | True | 状态描述
Disabled | bool | - | False | 已被禁用(False)
IsApproved | bool | - | False | 是否审核通过
IsLocked | bool | - | False | 已被锁定（锁定则不允许回复）
IsPinned | bool | - | False | 是否置顶
IsValued | bool | - | False | 是否精华帖
IsGlobal | bool | - | False | 是否全局贴
TotalViews | int | 4 | False | 累计阅读数
TotalReplies | int | 4 | False | 累计回帖数
PinnedTime | datetime | - | True | 置顶时间
GlobalTime | datetime | - | True | 全局时间
ViewedTime | datetime | - | True | 最后被阅读时间
MostRecentPostId | bigint | 8 | True | 最后回帖的帖子编号
MostRecentPostAuthorId | int | 4 | True | 最后回帖的作者编号
MostRecentPostAuthorName | nvarchar | 50 | True | 最后回帖的作者名称
MostRecentPostAuthorAvatar | nvarchar | 150 | True | 最后回帖的作者头像
MostRecentPostTime | datetime | - | True | 最后回帖的时间
CreatorId | int | 4 | False | 创建人编号
CreatedTime | datetime | - | False | 创建时间


### 帖子表 `Community.Post`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
PostId | bigint | 8 | False | 主键，帖子编号
SiteId | int | 4 | False | 所属站点编号
ThreadId | bigint | 8 | False | 所属主题编号
Content | varchar | 500 | False | 帖子内容
ContentType | varchar | 50 | True | 内容类型(text/plain+embedded, text/html, application/json)
ParentId | bigint | 8 | True | 应答的回复编号
Disabled | bool | - | False | 已被禁用(False)
IsApproved | bool | - | False | 是否审核通过
IsLocked | bool | - | False | 是否已锁定(锁定则不允许回复)
IsValued | bool | _ | False | 是否精华帖
IsThread | bool | - | False | 是否主题内容贴(False)
TotalUpvotes | int | 4 | False | 累计点赞数
TotalDownvotes | int | 4 | False | 累计被踩数
VisitorAddress | nvarchar | 100 | True | 访客地址(IP和位置信息)(192.168.0.1 湖北省武汉市)
VisitorDescription | varchar | 500 | True | 访客描述(浏览器代理信息)
CreatorId | int | 4 | False | 发帖人编号
CreatedTime | datetime | - | False | 发帖时间


### 帖子投票表 `Community.PostVoting`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
PostId | bigint | 8 | False | 主键，帖子编号
UserId | int | 4 | False | 主键，用户编号
UserName | nvarchar | 50 | True | 用户名称
UserAvatar | nvarchar | 150 | True | 用户头像
Value | byte | 1 | False | 投票数(正数为Upvote，负数为Downvote)
Tiemstamp | datetime | - | False | 投票时间


### 用户浏览记录表 `Community.History`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
UserId | int | 4 | False | 主键，用户编号
ThreadId | bigint | 8 | False | 主键，主题编号
Count | int | 4 | False | 浏览次数
FirstViewedTime | datetime | - | False | 首次浏览时间
MostRecentViewedTime | datetime | - | False | 最后浏览时间


### 用户配置表 `Community.UserProfile`

字段名称|数据类型|长度|可空|备注
--------|:------:|:--:|:--:|----:
UserId | int | 4 | False | 主键，用户编号
SiteId | int | 4 | False | 用户所属站点编号
Gender | byte | 1 | False | 用户性别(0:Female, 1:Male)
PhotoPath | varchar | 200 | True | 照片文件路径
TotalPosts | int | 4 | False | 累计回复总数
TotalThreads | int | 4 | False | 累计主题总数
MostRecentPostId | bigint | 8 | True | 最后回帖的帖子编号
MostRecentPostTime | datetime | - | True | 最后回帖的时间
MostRecentThreadId | bigint | 8 | True | 最新主题的编号
MostRecentThreadSubject | nvarchar | 100 | True | 最新主题的标题
MostRecentThreadTime | datetime | - | True | 最新主题的发布时间
CreatedTime | datetime | - | False | 创建时间