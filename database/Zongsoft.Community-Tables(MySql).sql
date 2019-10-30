SET NAMES utf8;
SET TIME_ZONE='+08:00';


CREATE TABLE IF NOT EXISTS `Community_Feedback`
(
  `FeedbackId` bigint UNSIGNED NOT NULL COMMENT '主键，反馈编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Kind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '反馈种类(0:None)',
  `Subject` nvarchar(100) NOT NULL COMMENT '反馈标题',
  `Content` nvarchar(500) NOT NULL COMMENT '反馈内容文件',
  `ContentType` varchar(50) NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `ContactName` nvarchar(50) NOT NULL COMMENT '联系人名称',
  `ContactText` nvarchar(50) NOT NULL COMMENT '联系人方式',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`FeedbackId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='意见反馈表';

CREATE TABLE IF NOT EXISTS `Community_Message`
(
  `MessageId` bigint UNSIGNED NOT NULL COMMENT '主键，消息编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Subject` nvarchar(100) NOT NULL COMMENT '消息标题',
  `Content` nvarchar(500) NOT NULL COMMENT '消息内容',
  `ContentType` varchar(50) NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `MessageType` varchar(50) NULL COMMENT '消息类型',
  `Referer` varchar(50) NULL COMMENT '消息来源',
  `Tags` nvarchar(100) NULL COMMENT '标签集(以逗号分隔)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创建人编号(零表示系统消息)',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息(站内通知)表';

CREATE TABLE IF NOT EXISTS `Community_MessageUser`
(
  `MessageId` bigint UNSIGNED NOT NULL COMMENT '主键，消息编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `IsRead` tinyint(1) UNSIGNED NOT NULL DEFAULT 0 COMMENT '是否已读',
  PRIMARY KEY (`MessageId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息接收人员表';

CREATE TABLE IF NOT EXISTS `Community_Folder`
(
  `FolderId` int UNSIGNED NOT NULL COMMENT '主键，文件夹编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Name` nvarchar(100) NOT NULL COMMENT '文件夹名称',
  `PinYin` narchar(200) NOT NULL COMMENT '名称拼音',
  `Icon` varchar(50) NULL COMMENT '显示图标',
  `Shareability` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '共享性',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创建人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `Description` nvarchar(500) NULL COMMENT '描述文本',
  PRIMARY KEY (`FolderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件夹表';

CREATE TABLE IF NOT EXISTS `Community_FolderUser`
(
  `FolderId` int UNSIGNED NOT NULL COMMENT '主键，文件夹编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `Permission` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '权限定义',
  `Expiration` datetime NULL COMMENT '过期时间',
  PRIMARY KEY (`FolderId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件夹用户表';

CREATE TABLE IF NOT EXISTS `Community_File`
(
  `FileId` bigint UNSIGNED NOT NULL COMMENT '主键，文件编号',
  `SiteId` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属站点编号',
  `FolderId` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属文件夹编号(零表示不属于文件夹)',
  `Name` nvarchar(100) NOT NULL COMMENT '文件名称',
  `Path` nvarchar(200) NOT NULL COMMENT '文件路径',
  `Type` varchar(50) NULL COMMENT '文件类型(MIME类型)',
  `Size` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '文件大小(单位：字节)',
  `Tags` nvarchar(100) NULL COMMENT '标签集(以逗号分隔)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创建人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `Description` nvarchar(500) NULL COMMENT '描述文本',
  PRIMARY KEY (`FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件（附件）表';

CREATE TABLE IF NOT EXISTS `Community_ForumGroup`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `GroupId` smallint NOT NULL COMMENT '主键，分组编号',
  `Name` nvarchar(50) NOT NULL COMMENT '论坛组名称',
  `Icon` varchar(100) NULL COMMENT '显示图标',
  `SortOrder` smallint NOT NULL DEFAULT 0 COMMENT '排列顺序',
  `Description` nvarchar(500) NULL COMMENT '描述文本',
  PRIMARY KEY (`SiteId`, `GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛分组表';

CREATE TABLE IF NOT EXISTS `Community_Forum`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '主键，论坛编号',
  `GroupId` smallint NOT NULL COMMENT '论坛组编号',
  `Name` nvarchar(50) NOT NULL COMMENT '论坛名称',
  `Description` nvarchar(500) NULL COMMENT '描述文本',
  `CoverPicturePath` varchar(200) NULL COMMENT '封面图片文件路径',
  `SortOrder` smallint NOT NULL DEFAULT 0 COMMENT '排列顺序',
  `IsPopular` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否热门版块',
  `Approvable` tinyint(1) NOT NULL DEFAULT 0 COMMENT '发帖是否需要审核',
  `Visibility` tinyint UNSIGNED NOT NULL DEFAULT 2 COMMENT '可见范围',
  `Accessibility` tinyint UNSIGNED NOT NULL DEFAULT 2 COMMENT '可访问性',
  `TotalPosts` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计帖子总数',
  `TotalThreads` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计主题总数',
  `MostRecentThreadId` bigint UNSIGNED NULL COMMENT '最新主题的编号',
  `MostRecentThreadTitle` nvarchar(100) NULL COMMENT '最新主题的标题',
  `MostRecentThreadAuthorId` int UNSIGNED NULL COMMENT '最新主题的作者编号',
  `MostRecentThreadAuthorName` nvarchar(50) NULL COMMENT '最新主题的作者名',
  `MostRecentThreadAuthorAvatar` varchar(150) NULL COMMENT '最新主题的作者头像',
  `MostRecentThreadTime` datetime NULL NULL COMMENT '最新主题的发布时间',
  `MostRecentPostId` bigint UNSIGNED NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostAuthorId` int UNSIGNED NULL COMMENT '最后回帖的作者编号',
  `MostRecentPostAuthorName` nvarchar(50) NULL COMMENT '最后回帖的作者名',
  `MostRecentPostAuthorAvatar` varchar(150) NULL COMMENT '最后回帖的作者头像',
  `MostRecentPostTime` datetime NULL NULL COMMENT '最后回帖的时间',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`SiteId`, `ForumId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛表';

CREATE TABLE IF NOT EXISTS `Community_ForumUser`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '主键，论坛编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `Permission` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '权限定义',
  `IsModerator` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否版主',
  PRIMARY KEY (`SiteId`, `ForumId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛用户表';

CREATE TABLE IF NOT EXISTS `Community_Post`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '所属站点编号',
  `ThreadId` bigint UNSIGNED NOT NULL COMMENT '所属主题编号',
  `RefererId` bigint UNSIGNED NOT NULL DEFAULT 0 COMMENT '回帖引用编号',
  `Content` nvarchar(500) NOT NULL COMMENT '帖子内容',
  `ContentType` varchar(50) NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `Visible` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否可见',
  `Approved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否已审核通过',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已锁定',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `TotalUpvotes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计点赞数',
  `TotalDownvotes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计被踩数',
  `VisitorAddress` varchar(100) NULL COMMENT '访客地址',
  `VisitorDescription` varchar(500) NULL COMMENT '访客描述(浏览器代理信息等)',
  `AttachmentMark` varchar(100) NULL COMMENT '附件标记(以逗号分隔)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '发帖人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发帖时间',
  PRIMARY KEY (`PostId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子/回帖表';

CREATE TABLE IF NOT EXISTS `Community_PostVoting`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `UserName` nvarchar(50) NULL COMMENT '用户名称',
  `UserAvatar` varchar(150) NULL COMMENT '用户头像',
  `Value` tinyint NOT NULL COMMENT '投票数(正数为Upvote，负数为Downvote)',
  `Tiemstamp` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '投票时间',
  PRIMARY KEY (`PostId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子投票表';

CREATE TABLE IF NOT EXISTS `Community_History`
(
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `ThreadId` bigint UNSIGNED NOT NULL COMMENT '主键，主题编号',
  `Count` int UNSIGNED NOT NULL COMMENT '浏览次数',
  `FirstViewedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '首次浏览时间',
  `MostRecentViewedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '最后浏览时间',
  PRIMARY KEY (`UserId`, `ThreadId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户浏览记录表';

CREATE TABLE IF NOT EXISTS `Community_Thread`
(
  `ThreadId` bigint UNSIGNED NOT NULL COMMENT '主键，主题(文章)编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '所属站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '所属论坛编号',
  `Title` nvarchar(100) NOT NULL COMMENT '文章标题',
  `Summary` nvarchar(500) NOT NULL COMMENT '文章摘要',
  `Tags` nvarchar(100) NULL COMMENT '标签集(以逗号分隔)',
  `PostId` bigint UNSIGNED NOT NULL COMMENT '内容帖子编号',
  `CoverPicturePath` varchar(200) NULL COMMENT '封面图片文件路径',
  `ArticleUrl` varchar(200) NULL COMMENT '文章链接',
  `Visible` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否可见',
  `Approved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否审核通过',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否锁定(锁定则不允许回复)',
  `IsPinned` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否置顶',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `IsGlobal` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否全局贴',
  `TotalViews` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计阅读数',
  `TotalReplies` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计回帖数',
  `ApprovedTime` datetime NULL COMMENT '审核通过时间',
  `ViewedTime` datetime NULL COMMENT '最后被查看时间',
  `MostRecentPostId` bigint UNSIGNED NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostAuthorId` int UNSIGNED NULL COMMENT '最后回帖的作者编号',
  `MostRecentPostAuthorName` nvarchar(50) NULL COMMENT '最后回帖的作者名',
  `MostRecentPostAuthorAvatar` varchar(150) NULL COMMENT '最后回帖的作者头像',
  `MostRecentPostTime` datetime NULL NULL COMMENT '最后回帖的时间',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '作者编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`ThreadId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文章/主题表';

CREATE TABLE IF NOT EXISTS `Community_UserProfile`
(
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '用户所属的站点编号',
  `Name` nvarchar(50) NOT NULL COMMENT '用户名称',
  `Nickname` nvarchar(50) NULL COMMENT '用户昵称',
  `Email` varchar(50) NULL COMMENT '用户绑定的邮箱地址',
  `Phone` varchar(50) NULL COMMENT '用户绑定的手机号码',
  `Avatar` varchar(200) NULL COMMENT '用户头像',
  `Gender` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '用户性别(0:Female, 1:Male)',
  `Grade` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '用户等级',
  `TotalPosts` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计回复总数',
  `TotalThreads` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计主题总数',
  `MostRecentPostId` bigint UNSIGNED NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostTime` datetime NULL COMMENT '最后回帖的时间',
  `MostRecentThreadId` bigint UNSIGNED NULL COMMENT '最新主题的编号',
  `MostRecentThreadTitle` nvarchar(100) NULL COMMENT '最新主题的标题',
  `MostRecentThreadTime` datetime NULL COMMENT '最新主题的发布时间',
  `Creation` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `Modification` datetime NULL COMMENT '最后修改时间',
  `Description` nvarchar(500) NULL COMMENT '描述信息',
  PRIMARY KEY (`UserId`),
  UNIQUE INDEX `UX_User_Name` (`SiteId`, `Name`),
  UNIQUE INDEX `UX_User_Email` (`SiteId`, `Email`),
  UNIQUE INDEX `UX_User_Phone` (`SiteId`, `Phone`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户配置表';
