SET NAMES utf8;
SET TIME_ZONE='+08:00';


CREATE TABLE IF NOT EXISTS `Community_Feedback`
(
  `FeedbackId` bigint UNSIGNED NOT NULL COMMENT '主键，反馈编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Kind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '反馈种类(0:None)',
  `Subject` varchar(100) NOT NULL COMMENT '反馈标题',
  `Content` varchar(500) NOT NULL COMMENT '反馈内容文件',
  `ContentType` varchar(50) DEFAULT NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `ContactName` varchar(50) NOT NULL COMMENT '联系人名称',
  `ContactText` varchar(50) NOT NULL COMMENT '联系人方式',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`FeedbackId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='意见反馈表';

CREATE TABLE IF NOT EXISTS `Community_Message`
(
  `MessageId` bigint UNSIGNED NOT NULL COMMENT '主键，消息编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Subject` varchar(100) NOT NULL COMMENT '消息标题',
  `Content` varchar(500) NOT NULL COMMENT '消息内容',
  `ContentType` varchar(50) DEFAULT NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `MessageType` varchar(50) DEFAULT NULL COMMENT '消息类型',
  `Status` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '状态',
  `StatusTimestamp` datetime NULL COMMENT '状态更改时间',
  `StatusDescription` varchar(100) DEFAULT NULL COMMENT '状态描述信息',
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
  `PinYin` nvarchar(200) NOT NULL COMMENT '名称拼音',
  `Icon` varchar(50) NULL COMMENT '显示图标',
  `Visiblity` tinyint UNSIGNED NOT NULL DEFAULT 1 COMMENT '可见范围(0:禁用,即不可见; 1:站内用户可见; 2:所有人可见; 3:指定用户)',
  `Accessibility` tinyint UNSIGNED NOT NULL DEFAULT 1 COMMENT '可访问性(0:无限制; 1:注册用户; 2:仅限管理员; 3:指定用户)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创建人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `Description` varchar(500) DEFAULT NULL COMMENT '描述文本',
  PRIMARY KEY (`FolderId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件夹表';

CREATE TABLE IF NOT EXISTS `Community_FolderUser`
(
  `FolderId` int UNSIGNED NOT NULL COMMENT '主键，文件夹编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `UserKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '用户种类(0:None, 1:Administrator, 2:Writer, 3:Reader)',
  PRIMARY KEY (`FolderId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件夹用户表';

CREATE TABLE IF NOT EXISTS `Community_File`
(
  `FileId` bigint UNSIGNED NOT NULL COMMENT '主键，文件编号',
  `FolderId` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属文件夹编号(零表示不属于文件夹)',
  `SiteId` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '所属站点编号',
  `Name` nvarchar(100) NOT NULL COMMENT '文件名称',
  `Path` nvarchar(200) NOT NULL COMMENT '文件路径',
  `Type` varchar(50) NULL COMMENT '文件类型(MIME类型)',
  `Size` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '文件大小(单位：字节)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创建人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `Description` varchar(500) DEFAULT NULL COMMENT '描述文本',
  PRIMARY KEY (`FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文件（附件）表';

CREATE TABLE IF NOT EXISTS `Community_ForumGroup`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `GroupId` smallint NOT NULL COMMENT '主键，分组编号',
  `Name` varchar(50) NOT NULL COMMENT '论坛组名称',
  `Icon` varchar(100) NULL COMMENT '显示图标',
  `Visiblity` tinyint UNSIGNED NOT NULL DEFAULT 1 COMMENT '可见范围(0:禁用,即不可见; 1:站内用户可见; 2: 所有人可见)',
  `SortOrder` smallint NOT NULL DEFAULT 0 COMMENT '排列顺序',
  `Description` varchar(500) DEFAULT NULL COMMENT '描述文本',
  PRIMARY KEY (`SiteId`, `GroupId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛分组表';

CREATE TABLE IF NOT EXISTS `Community_Forum`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '主键，论坛编号',
  `GroupId` smallint NOT NULL COMMENT '论坛组编号',
  `Name` varchar(50) NOT NULL COMMENT '论坛名称',
  `Description` varchar(500) DEFAULT NULL COMMENT '描述文本',
  `CoverPicturePath` varchar(200) DEFAULT NULL COMMENT '封面图片文件路径',
  `SortOrder` smallint NOT NULL DEFAULT 0 COMMENT '排列顺序',
  `IsPopular` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否热门版块',
  `ApproveEnabled` tinyint(1) NOT NULL DEFAULT 0 COMMENT '发帖是否需要审核',
  `Visiblity` tinyint UNSIGNED NOT NULL DEFAULT 1 COMMENT '可见范围(0:禁用,即不可见; 1:站内用户可见; 2: 所有人可见)',
  `Accessibility` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '可访问性(0:无限制; 1:注册用户; 2:仅限版主)',
  `TotalPosts` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计帖子总数',
  `TotalThreads` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计主题总数',
  `MostRecentThreadId` bigint UNSIGNED NULL COMMENT '最新主题的编号',
  `MostRecentThreadSubject` nvarchar(100) NULL COMMENT '最新主题的标题',
  `MostRecentThreadAuthorId` int UNSIGNED NULL COMMENT '最新主题的作者编号',
  `MostRecentThreadAuthorName` nvarchar(50) NULL COMMENT '最新主题的作者名',
  `MostRecentThreadAuthorAvatar` varchar(150) NULL COMMENT '最新主题的作者头像',
  `MostRecentThreadTime` datetime NULL DEFAULT NULL COMMENT '最新主题的发布时间',
  `MostRecentPostId` bigint UNSIGNED NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostAuthorId` int UNSIGNED NULL COMMENT '最后回帖的作者编号',
  `MostRecentPostAuthorName` nvarchar(50) NULL COMMENT '最后回帖的作者名',
  `MostRecentPostAuthorAvatar` varchar(150) NULL COMMENT '最后回帖的作者头像',
  `MostRecentPostTime` datetime NULL DEFAULT NULL COMMENT '最后回帖的时间',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`SiteId`, `ForumId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛表';

CREATE TABLE IF NOT EXISTS `Community_ForumUser`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '主键，论坛编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `UserKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '用户种类(0:None, 1:Administrator, 2:Writer, 3:Reader)',
  PRIMARY KEY (`SiteId`, `ForumId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛用户表';

CREATE TABLE IF NOT EXISTS `Community_Post`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '所属站点编号',
  `ThreadId` bigint UNSIGNED NOT NULL COMMENT '所属主题编号',
  `Content` varchar(500) NOT NULL COMMENT '帖子内容',
  `ContentType` varchar(50) DEFAULT NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `Disabled` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已禁用',
  `IsApproved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否已审核通过',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已锁定',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `TotalUpvotes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计点赞数',
  `TotalDownvotes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计被踩数',
  `VisitorAddress` varchar(100) NULL COMMENT '访客地址(IP和地址信息)',
  `VisitorDescription` varchar(500) NULL COMMENT '访客描述(浏览器代理信息)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '发帖人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发帖时间',
  PRIMARY KEY (`PostId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子/回帖表';

CREATE TABLE IF NOT EXISTS `Community_PostComment`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `SerialId` smallint UNSIGNED NOT NULL COMMENT '主键，回复序号',
  `SourceId` smallint UNSIGNED NULL COMMENT '关联的回复序号',
  `Content` varchar(500) NOT NULL COMMENT '帖子内容',
  `ContentType` varchar(50) DEFAULT NULL COMMENT '内容类型(text/plain+embedded, text/html, application/json)',
  `VisitorAddress` varchar(100) NULL COMMENT '访客地址(IP和地址信息)',
  `VisitorDescription` varchar(500) NULL COMMENT '访客描述(浏览器代理信息)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '回复人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '回复时间',
  PRIMARY KEY (`PostId`, `SerialId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子回复表';

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

CREATE TABLE IF NOT EXISTS `Community_PostAttachment`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `FileId` bigint UNSIGNED NOT NULL COMMENT '主键，文件编号',
  PRIMARY KEY (`PostId`, `FileId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子附件表';

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
  `Subject` varchar(100) NOT NULL COMMENT '文章标题',
  `Summary` varchar(500) NOT NULL COMMENT '文章摘要',
  `Tags` varchar(100) DEFAULT NULL COMMENT '标签集',
  `PostId` bigint UNSIGNED NOT NULL COMMENT '内容帖子编号',
  `CoverPicturePath` varchar(200) DEFAULT NULL COMMENT '封面图片文件路径',
  `LinkUrl` varchar(200) DEFAULT NULL COMMENT '文章跳转链接',
  `Status` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '状态(0:未发送, 1:发送中, 2:已发送, 3:已取消)',
  `StatusTimestamp` datetime DEFAULT NULL COMMENT '状态更改时间',
  `StatusDescription` varchar(100) DEFAULT NULL COMMENT '状态描述信息',
  `Disabled` tinyint(1) NOT NULL DEFAULT 0 COMMENT '已被禁用',
  `Visible` tinyint(1) NOT NULL DEFAULT 1 COMMENT '已被可见',
  `IsApproved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否锁定(锁定则不允许回复)',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否锁定(锁定则不允许回复)',
  `IsPinned` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否置顶',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `IsGlobal` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否全局贴',
  `TotalViews` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计阅读数',
  `TotalReplies` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计回帖数',
  `PinnedTime` datetime DEFAULT NULL COMMENT '置顶时间',
  `GlobalTime` datetime DEFAULT NULL COMMENT '全局时间',
  `ViewedTime` datetime DEFAULT NULL COMMENT '最后被查看时间',
  `MostRecentPostId` bigint UNSIGNED NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostAuthorId` int UNSIGNED NULL COMMENT '最后回帖的作者编号',
  `MostRecentPostAuthorName` nvarchar(50) NULL COMMENT '最后回帖的作者名',
  `MostRecentPostAuthorAvatar` varchar(150) NULL COMMENT '最后回帖的作者头像',
  `MostRecentPostTime` datetime NULL DEFAULT NULL COMMENT '最后回帖的时间',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '作者编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`ThreadId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='文章/主题表';

CREATE TABLE IF NOT EXISTS `Community_UserProfile`
(
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '用户所属的站点编号',
  `Gender` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '用户性别(0:Female, 1:Male)',
  `PhotoPath` varchar(200) DEFAULT NULL COMMENT '照片文件路径',
  `TotalPosts` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计回复总数',
  `TotalThreads` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计主题总数',
  `MostRecentPostId` bigint UNSIGNED DEFAULT NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostTime` datetime DEFAULT NULL COMMENT '最后回帖的时间',
  `MostRecentThreadId` bigint UNSIGNED DEFAULT NULL COMMENT '最新主题的编号',
  `MostRecentThreadSubject` nvarchar(100) DEFAULT NULL COMMENT '最新主题的标题',
  `MostRecentThreadTime` datetime DEFAULT NULL COMMENT '最新主题的发布时间',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`UserId`),
  INDEX `IX_SiteId` (`SiteId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户配置表';
