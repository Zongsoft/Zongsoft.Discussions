SET NAMES utf8;
SET TIME_ZONE='+08:00';


CREATE TABLE IF NOT EXISTS `Community_Feedback`
(
  `FeedbackId` bigint UNSIGNED NOT NULL COMMENT '主键，反馈编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '站点编号',
  `Subject` varchar(100) NOT NULL COMMENT '反馈标题',
  `Content` varchar(500) NOT NULL COMMENT '反馈内容文件',
  `ContentKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '内容种类(0:Text, 1:File)',
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
  `ContentKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '内容种类(0:Text, 1:File)',
  `MessageKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '消息种类(0:None)',
  `Status` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '状态',
  `StatusTimestamp` datetime NULL COMMENT '状态更改时间',
  `StatusDescription` varchar(100) DEFAULT NULL COMMENT '状态描述信息',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '创人编号(零表示系统消息)',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息(站内通知)表';

CREATE TABLE IF NOT EXISTS `Community_MessageMember`
(
  `MessageId` bigint UNSIGNED NOT NULL COMMENT '主键，消息编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `Status` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '状态(0:未读, 1:已读)',
  `StatusTimestamp` datetime NULL COMMENT '状态更改时间',
  PRIMARY KEY (`MessageId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息接收人员表';

CREATE TABLE IF NOT EXISTS `Community_ForumGroup`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `GroupId` smallint NOT NULL COMMENT '主键，分组编号',
  `Name` varchar(50) NOT NULL COMMENT '论坛组名称',
  `Icon` varchar(100) NULL COMMENT '显示图标',
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
  `Visiblity` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '可见范围(0:禁用,即不可见; 1:企业范围可见; 2: 所有人可见; 3:所有人可发帖)',
  `Accessibility` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '可访问性(0:无限制; 1:注册用户; 2:仅限版主)',
  `TotalPosts` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计帖子总数',
  `TotalThreads` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计主题总数',
  `MostRecentThreadId` int UNSIGNED NULL COMMENT '最新主题的编号',
  `MostRecentThreadSubject` nvarchar(100) NULL COMMENT '最新主题的标题',
  `MostRecentThreadAuthorId` int UNSIGNED NULL COMMENT '最新主题的作者编号',
  `MostRecentThreadAuthorName` nvarchar(50) NULL COMMENT '最新主题的作者名',
  `MostRecentThreadAuthorAvatar` varchar(150) NULL COMMENT '最新主题的作者头像',
  `MostRecentThreadTime` datetime NULL DEFAULT NULL COMMENT '最新主题的发布时间',
  `MostRecentPostAuthorId` int UNSIGNED NULL COMMENT '最后回帖的作者编号',
  `MostRecentPostAuthorName` nvarchar(50) NULL COMMENT '最后回帖的作者名',
  `MostRecentPostAuthorAvatar` varchar(150) NULL COMMENT '最后回帖的作者头像',
  `MostRecentPostTime` datetime NULL DEFAULT NULL COMMENT '最后回帖的时间',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`SiteId`, `ForumId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='论坛表';

CREATE TABLE IF NOT EXISTS `Community_Moderator`
(
  `SiteId` int UNSIGNED NOT NULL COMMENT '主键，站点编号',
  `ForumId` smallint UNSIGNED NOT NULL COMMENT '主键，论坛编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  PRIMARY KEY (`SiteId`, `ForumId`, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='版主表';

CREATE TABLE IF NOT EXISTS `Community_Post`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `SiteId` int UNSIGNED NOT NULL COMMENT '所属站点编号',
  `ThreadId` bigint UNSIGNED NOT NULL COMMENT '所属主题编号',
  `Content` varchar(500) NOT NULL COMMENT '帖子内容',
  `ContentKind` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '内容种类(0:Text, 1:File)',
  `ParentId` bigint UNSIGNED DEFAULT NULL COMMENT '父帖子编号(应答的回复编号)',
  `Disabled` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已禁用',
  `IsApproved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否已审核通过',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否已锁定',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `TotalLikes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计点赞数',
  `VisitorAddress` varchar(100) NULL COMMENT '访客地址(IP和地址信息)',
  `VisitorDescription` varchar(500) NULL COMMENT '访客描述(浏览器代理信息)',
  `CreatorId` int UNSIGNED NOT NULL COMMENT '发帖人编号',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发帖时间',
  PRIMARY KEY (`PostId` DESC)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子/回帖表';

CREATE TABLE IF NOT EXISTS `Community_Liking`
(
  `PostId` bigint UNSIGNED NOT NULL COMMENT '主键，帖子编号',
  `UserId` int UNSIGNED NOT NULL COMMENT '主键，用户编号',
  `Points` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '赞助积分',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发帖时间',
  PRIMARY KEY (`PostId` DESC, `UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='帖子点赞表';

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
  `PostId` bigint UNSIGNED NOT NULL COMMENT '内容帖子编号',
  `CoverPicturePath` varchar(200) DEFAULT NULL COMMENT '封面图片文件路径',
  `LinkUrl` varchar(200) DEFAULT NULL COMMENT '文章跳转链接',
  `Status` tinyint UNSIGNED NOT NULL DEFAULT 0 COMMENT '状态(0:未发送, 1:发送中, 2:已发送, 3:已取消)',
  `StatusTimestamp` datetime NOT NULL COMMENT '状态更改时间',
  `StatusDescription` varchar(100) DEFAULT NULL COMMENT '状态描述信息',
  `Disabled` tinyint(1) NOT NULL DEFAULT 0 COMMENT '已被禁用',
  `IsApproved` tinyint(1) NOT NULL DEFAULT 1 COMMENT '是否锁定(锁定则不允许回复)',
  `IsLocked` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否锁定(锁定则不允许回复)',
  `IsPinned` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否置顶',
  `IsValued` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否精华帖',
  `IsGlobal` tinyint(1) NOT NULL DEFAULT 0 COMMENT '是否全局贴',
  `TotalViews` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计阅读数',
  `TotalLikes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计点赞数',
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
  `TotalInLikes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计收获的赞数',
  `TotalOutLikes` int UNSIGNED NOT NULL DEFAULT 0 COMMENT '累计点击的赞数',
  `MostRecentPostId` bigint UNSIGNED DEFAULT NULL COMMENT '最后回帖的帖子编号',
  `MostRecentPostTime` datetime DEFAULT NULL COMMENT '最后回帖的时间',
  `MostRecentThreadId` int UNSIGNED DEFAULT NULL COMMENT '最新主题的编号',
  `MostRecentThreadSubject` nvarchar(100) DEFAULT NULL COMMENT '最新主题的标题',
  `MostRecentThreadTime` datetime DEFAULT NULL COMMENT '最新主题的发布时间',
  `CreatedTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户配置表';
