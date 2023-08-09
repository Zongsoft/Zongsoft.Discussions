# Zongsoft.Community(Discussions) 社区系统

## 消息表 `Community.Message`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
MessageId   | bigint   | 8   | ✗ | 主键，消息编号
SiteId      | int      | 4   | ✗ | 站点编号
Subject     | nvarchar | 50  | ✗ | 消息主题
Content     | nvarchar | 500 | ✗ | 消息内容
ContentType | varchar  | 100 | ✓ | 内容类型(text/plain+embedded, text/html)
MessageType | varchar  | 50  | ✓ | 消息类型
Referer     | varchar  | 50  | ✓ | 消息来源
Tags        | nvarchar | 100 | ✓ | 标签集(以逗号分隔)
CreatorId   | int      | 4   | ✗ | 创建人编号(零表示系统消息)
CreatedTime | datetime | -   | ✗ | 创建时间


## 意见反馈表 `Community.Feedback`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
FeedbackId  | bigint   | 8   | ✗ | 主键，反馈编号
SiteId      | int      | 4   | ✗ | 站点编号
Kind        | byte     | 1   | ✗ | 反馈种类(0:None)
Subject     | nvarchar | 50  | ✗ | 反馈标题
Content     | nvarchar | 500 | ✗ | 反馈内容
ContentType | varchar  | 100 | ✓ | 内容类型(text/plain+embedded, text/html)
ContactName | nvarchar | 50  | ✓ | 联系人名
ContactText | nvarchar | 50  | ✓ | 联系方式
CreatedTime | datetime | -   | ✗ | 反馈时间


## 目录表 `Community.Folder`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
FolderId     | int      | 4   | ✗ | 主键，目录编号
SiteId       | int      | 4   | ✗ | 所属站点编号
Name         | nvarchar | 50  | ✗ | 目录名称
Icon         | varchar  | 50  | ✓ | 图标标识
Acronym      | varchar  | 50  | ✓ | 名称缩写
Shareability | byte     | 1   | ✗ | 共享性
CreatorId    | int      | 4   | ✗ | 创建人编号
CreatedTime  | datetime | -   | ✗ | 创建时间
Description  | varchar  | 500 | ✓ | 备注描述


## 目录用户表 `Community.FolderUser`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
FolderId   | int      | 4 | ✗ | 主键，目录编号
UserId     | int      | 4 | ✗ | 主键，用户编号
Permission | byte     | 1 | ✗ | 权限定义(0:None, 1:Read, 2:Write)
Expiration | datetime | - | ✓ | 过期时间


## 文件表 `Community.File`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
FileId      | bigint   | 8   | ✗ | 主键，文件编号
SiteId      | int      | 4   | ✗ | 所属站点编号
FolderId    | int      | 4   | ✗ | 所属目录编号
Name        | nvarchar | 50  | ✗ | 文件名称
Path        | varchar  | 200 | ✗ | 文件路径
Acronym     | varchar  | 50  | ✓ | 名称缩写
Type        | varchar  | 100 | ✗ | 文件类型(MIME类型)
Size        | int      | 4   | ✗ | 文件大小(单位：字节)
Tags        | nvarchar | 100 | ✓ | 标签集(以逗号分隔)
CreatorId   | int      | 4   | ✓ | 创建人编号
CreatedTime | datetime | -   | ✗ | 创建时间(上传时间)
Description | nvarchar | 100 | ✓ | 描述说明


## 站点表 `Community.Site`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
SiteId      | int      | 4   | ✗ | 主键，站点编号
SiteNo      | varchar  | 50  | ✗ | 站点代号
Name        | nvarchar | 50  | ✗ | 站点名称
Host        | varchar  | 50  | ✗ | 站点域名
Icon        | varchar  | 100 | ✓ | 站点图标
Domain      | varchar  | 50  | ✗ | 所属领域
Description | nvarchar | 500 | ✓ | 描述信息


## 站点用户表 `Community.SiteUser`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
SiteId      | int  | 4 | ✗ | 主键，站点编号
UserId      | int  | 4 | ✗ | 主键，用户编号


## 论坛组表 `Community.ForumGroup`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
SiteId      | int      | 4   | ✗ | 主键，站点编号
GroupId     | smallint | 2   | ✗ | 主键，论坛分组编号
Name        | nvarchar | 50  | ✗ | 论坛组名
Icon        | varchar  | 100 | ✓ | 显示图标
Ordinal     | smallint | 2   | ✗ | 排列顺序
Description | nvarchar | 500 | ✓ | 描述信息


## 论坛表 `Community.Forum`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
SiteId                       | int      | 4   | ✗ | 主键，站点编号
ForumId                      | smallint | 2   | ✗ | 主键，论坛编号
GroupId                      | smallint | 2   | ✗ | 论坛组编号
Name                         | nvarchar | 50  | ✗ | 论坛名称
Description                  | nvarchar | 500 | ✓ | 描述文本
CoverPicturePath             | varchar  | 200 | ✓ | 封面路径
Ordinal                      | smallint | 2   | ✗ | 排列顺序
IsPopular                    | bool     | -   | ✗ | 是否热门版块
Approvable                   | bool     | -   | ✗ | 发帖是否审核
Visibility                   | byte     | 1   | ✗ | 可见范围
Accessibility                | byte     | 1   | ✗ | 可访问性
TotalPosts                   | int      | 4   | ✗ | 累计帖子总数
TotalThreads                 | int      | 4   | ✗ | 累计主题总数
MostRecentThreadId           | bigint   | 8   | ✓ | 最新主题编号
MostRecentThreadTitle        | nvarchar | 100 | ✓ | 最新主题标题
MostRecentThreadAuthorId     | int      | 4   | ✓ | 最新主题作者编号
MostRecentThreadAuthorName   | nvarchar | 50  | ✓ | 最新主题作者名称
MostRecentThreadAuthorAvatar | varchar  | 100 | ✓ | 最新主题作者头像
MostRecentThreadTime         | datetime | -   | ✓ | 最新主题发布时间
MostRecentPostId             | bigint   | 8   | ✓ | 最后回帖帖子编号
MostRecentPostAuthorId       | int      | 4   | ✓ | 最后回帖用户编号
MostRecentPostAuthorName     | nvarchar | 50  | ✓ | 最后回帖用户名称
MostRecentPostAuthorAvatar   | varchar  | 100 | ✓ | 最后回帖用户头像
MostRecentPostTime           | datetime | -   | ✓ | 最后回帖时间
CreatorId                    | int      | 4   | ✗ | 创建人编号
CreatedTime                  | datetime | -   | ✗ | 创建时间


## 论坛用户表 `Community.ForumUser`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
SiteId      | int      | 4 | ✗ | 主键，站点编号
ForumId     | smallint | 2 | ✗ | 主键，论坛编号
UserId      | int      | 4 | ✗ | 主键，用户编号
Permission  | byte     | 1 | ✗ | 权限定义(0:None, 1:Read, 2:Write)
IsModerator | bool     | - | ✗ | 是否版主


## 主题表 `Community.Thread`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
ThreadId                   | bigint   | 8   | ✗ | 主键，主题编号
SiteId                     | int      | 4   | ✗ | 所属站点编号
ForumId                    | smallint | 2   | ✗ | 所属论坛编号
Title                      | nvarchar | 50  | ✗ | 文章标题
Acronym                    | varchar  | 50  | ✓ | 标题缩写
Summary                    | nvarchar | 500 | ✓ | 文章摘要
Tags                       | nvarchar | 100 | ✓ | 标签集(以逗号分隔)
PostId                     | bigint   | 8   | ✗ | 主帖编号
CoverPicturePath           | varchar  | 200 | ✓ | 封面路径
LinkUrl                    | varchar  | 200 | ✓ | 主题链接
Visible                    | bool     | -   | ✗ | 是否可见(默认为真)
Approved                   | bool     | -   | ✗ | 是否审核通过
IsLocked                   | bool     | -   | ✗ | 已被锁定(锁定则不允许回复)
IsPinned                   | bool     | -   | ✗ | 是否置顶
IsValued                   | bool     | -   | ✗ | 是否精华帖
IsGlobal                   | bool     | -   | ✗ | 是否全局贴
TotalViews                 | int      | 4   | ✗ | 累计阅读数
TotalReplies               | int      | 4   | ✗ | 累计回帖数
ApprovedTime               | datetime | -   | ✓ | 审核通过时间
ViewedTime                 | datetime | -   | ✓ | 最后被阅读时间
MostRecentPostId           | bigint   | 8   | ✓ | 最后回帖编号
MostRecentPostAuthorId     | int      | 4   | ✓ | 最后回帖用户编号
MostRecentPostAuthorName   | nvarchar | 50  | ✓ | 最后回帖用户名称
MostRecentPostAuthorAvatar | varchar  | 100 | ✓ | 最后回帖用户头像
MostRecentPostTime         | datetime | -   | ✓ | 最后回帖时间
CreatorId                  | int      | 4   | ✗ | 创建人编号
CreatedTime                | datetime | -   | ✗ | 创建时间


## 帖子表 `Community.Post`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
PostId             | bigint   | 8   | ✗ | 主键，帖子编号
SiteId             | int      | 4   | ✗ | 所属站点编号
ThreadId           | bigint   | 8   | ✗ | 所属主题编号
RefererId          | bigint   | 8   | ✗ | 回帖引用帖子编号
Content            | varchar  | 500 | ✗ | 帖子内容
ContentType        | varchar  | 50  | ✓ | 内容类型(text/plain+embedded, text/html)
Visible            | bool     | -   | ✗ | 是否可见(默认为真)
Approved           | bool     | -   | ✗ | 是否已审核
IsLocked           | bool     | -   | ✗ | 是否已锁定(锁定则不允许回复)
IsValued           | bool     | -   | ✗ | 是否精华帖
TotalUpvotes       | int      | 4   | ✗ | 累计点赞数
TotalDownvotes     | int      | 4   | ✗ | 累计被踩数
VisitorAddress     | nvarchar | 100 | ✓ | 访客地址
VisitorDescription | nvarchar | 500 | ✓ | 访客描述(浏览器代理信息等)
CreatorId          | int      | 4   | ✗ | 发帖人编号
CreatedTime        | datetime | -   | ✗ | 发帖时间


## 帖子附件表 `Community.PostAttachment`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
PostId             | bigint | 8 | ✗ | 主键，物料编号
AttachmentId       | bigint | 8 | ✗ | 主键，附件编号
AttachmentFolderId | int    | 4 | ✗ | 附件目录编号
Ordinal            | short  | 2 | ✗ | 排列顺序


## 帖子投票表 `Community.PostVoting`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
PostId     | bigint   | 8   | ✗ | 主键，帖子编号
UserId     | int      | 4   | ✗ | 主键，用户编号
UserName   | nvarchar | 50  | ✓ | 用户名称
UserAvatar | varchar  | 100 | ✓ | 用户头像
Value      | byte     | 1   | ✗ | 投票数(正数为Upvote，负数为Downvote)
Tiemstamp  | datetime | -   | ✗ | 投票时间


## 访问历史表 `Community.History`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
UserId               | int      | 4 | ✗ | 主键，用户编号
ThreadId             | bigint   | 8 | ✗ | 主键，主题编号
Count                | int      | 4 | ✗ | 浏览次数
FirstViewedTime      | datetime | - | ✗ | 首次浏览时间
MostRecentViewedTime | datetime | - | ✗ | 最后浏览时间


## 用户信息表 `Community.UserProfile`

字段名称 | 数据类型 | 长度 | 可空 | 备注
------- |:-------:|:---:|:---:| ----
UserId                | int      | 4   | ✗ | 主键，用户编号
SiteId                | int      | 4   | ✗ | 默认站点编号
Name                  | varchar  | 50  | ✗ | 用户名称
Nickname              | nvarchar | 50  | ✓ | 用户昵称
Email                 | varchar  | 50  | ✓ | 邮箱地址
Phone                 | varchar  | 50  | ✓ | 手机号码
Avatar                | varchar  | 100 | ✓ | 用户头像
Gender                | byte     | 1   | ✗ | 用户性别
Grade                 | byte     | 1   | ✗ | 用户等级
PhotoPath             | varchar  | 200 | ✓ | 照片文件路径
TotalPosts            | int      | 4   | ✗ | 累计回复总数
TotalThreads          | int      | 4   | ✗ | 累计主题总数
MostRecentPostId      | bigint   | 8   | ✓ | 最后回帖编号
MostRecentPostTime    | datetime | -   | ✓ | 最后回帖时间
MostRecentThreadId    | bigint   | 8   | ✓ | 最新发布主题编号
MostRecentThreadTitle | nvarchar | 100 | ✓ | 最新发布主题标题
MostRecentThreadTime  | datetime | -   | ✓ | 最新主题发布时间
Creation              | datetime | -   | ✗ | 创建时间
Modification          | datetime | -   | ✓ | 修改时间
Description           | nvarchar | 500 | ✓ | 描述信息
