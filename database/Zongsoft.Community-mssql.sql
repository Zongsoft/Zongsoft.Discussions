CREATE TABLE [dbo].[Community_Feedback] (
    [FeedbackId]  BIGINT         NOT NULL,
    [SiteId]      INT            NOT NULL,
    [Kind]        TINYINT        DEFAULT (0) NOT NULL,
    [Subject]     NVARCHAR(100) NOT NULL,
    [Content]     NVARCHAR(500) NOT NULL,
    [ContentType] VARCHAR(50)   NULL,
    [ContactName] NVARCHAR(50)  NULL,
    [ContactText] NVARCHAR(50)  NULL,
    [CreatedTime] DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([FeedbackId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，反馈编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'FeedbackId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'反馈种类', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'Kind';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'反馈标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'Subject';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'反馈内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'Content';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内容类型(MIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'ContentType';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'ContactName';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'ContactText';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'反馈时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Feedback', @level2type = N'COLUMN', @level2name = N'CreatedTime';
GO

CREATE TABLE [dbo].[Community_File] (
    [FileId]      BIGINT         NOT NULL,
    [SiteId]      INT            NOT NULL,
    [FolderId]    INT            DEFAULT (0) NOT NULL,
    [Name]        NVARCHAR(50)  NOT NULL,
    [PinYin]      VARCHAR(200)  NULL,
    [Path]        VARCHAR(200)  NOT NULL,
    [Type]        VARCHAR(50)   NOT NULL,
    [Size]        INT            NOT NULL,
    [Tags]        NVARCHAR(100) NULL,
    [CreatorId]   INT            NOT NULL,
    [CreatedTime] DATETIME       DEFAULT (getdate()) NOT NULL,
    [Description] NVARCHAR(100) NULL,
    PRIMARY KEY CLUSTERED ([FileId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，文件编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'FileId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属文件夹编号(零表示独立文件)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'FolderId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Name';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称拼音', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'PinYin';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Path';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件类型(MIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Type';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件大小', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Size';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标签集(以逗号分隔)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Tags';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'CreatedTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_File', @level2type = N'COLUMN', @level2name = N'Description';
GO

CREATE TABLE [dbo].[Community_Folder] (
    [FolderId]     INT            NOT NULL,
    [SiteId]       INT            NOT NULL,
    [Name]         NVARCHAR(100) NOT NULL,
    [PinYin]       VARCHAR(200)  NULL,
    [Icon]         VARCHAR(50)   NULL,
    [Shareability] TINYINT        DEFAULT (0) NOT NULL,
    [CreatorId]    INT            NOT NULL,
    [CreatedTime]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [Description]  NVARCHAR(500) NULL,
    PRIMARY KEY CLUSTERED ([FolderId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，文件夹编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'FolderId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目录名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'Name';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称拼音', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'PinYin';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文件夹图标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'Icon';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'共享性', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'Shareability';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'CreatedTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Folder', @level2type = N'COLUMN', @level2name = N'Description';
GO

CREATE TABLE [dbo].[Community_FolderUser] (
    [FolderId]   INT      NOT NULL,
    [UserId]     INT      NOT NULL,
    [Permission] TINYINT  DEFAULT (0) NOT NULL,
    [Expiration] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([FolderId] ASC, [UserId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，文件夹编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_FolderUser', @level2type = N'COLUMN', @level2name = N'FolderId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_FolderUser', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限定义', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_FolderUser', @level2type = N'COLUMN', @level2name = N'Permission';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'过期时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_FolderUser', @level2type = N'COLUMN', @level2name = N'Expiration';
GO

CREATE TABLE [dbo].[Community_Forum] (
    [SiteId]                       INT            NOT NULL,
    [ForumId]                      SMALLINT       NOT NULL,
    [GroupId]                      SMALLINT       DEFAULT (0) NOT NULL,
    [Name]                         NVARCHAR(50)  NOT NULL,
    [Description]                  NVARCHAR(500) NULL,
    [CoverPicturePath]             VARCHAR(200)  NULL,
    [Ordinal]                      SMALLINT       DEFAULT (0) NOT NULL,
    [IsPopular]                    BIT            DEFAULT (0) NOT NULL,
    [Approvable]                   BIT            DEFAULT (0) NOT NULL,
    [Visibility]                   TINYINT        DEFAULT (0) NOT NULL,
    [Accessibility]                TINYINT        DEFAULT (0) NOT NULL,
    [TotalPosts]                   INT            DEFAULT (0) NOT NULL,
    [TotalThreads]                 INT            DEFAULT (0) NOT NULL,
    [MostRecentThreadId]           BIGINT         NULL,
    [MostRecentThreadTitle]        NVARCHAR(100) NULL,
    [MostRecentThreadAuthorId]     INT            NULL,
    [MostRecentThreadAuthorName]   NVARCHAR(50)  NULL,
    [MostRecentThreadAuthorAvatar] VARCHAR(100)  NULL,
    [MostRecentThreadTime]         DATETIME       NULL,
    [MostRecentPostId]             BIGINT         NULL,
    [MostRecentPostAuthorId]       INT            NULL,
    [MostRecentPostAuthorName]     NVARCHAR(50)  NULL,
    [MostRecentPostAuthorAvatar]   VARCHAR(100)  NULL,
    [MostRecentPostTime]           DATETIME       NULL,
    [CreatorId]                    INT            NOT NULL,
    [CreatedTime]                  DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([SiteId] ASC, [ForumId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，论坛编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'ForumId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属论坛组编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'GroupId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'论坛名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Name';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Description';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'封面图片地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'CoverPicturePath';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序权重', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Ordinal';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否热门板块', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'IsPopular';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发帖是否需要审核', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Approvable';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可见范围', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Visibility';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可访问性', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'Accessibility';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累计帖子总数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'TotalPosts';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累计主题总数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'TotalThreads';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadTitle';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的作者编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadAuthorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的作者名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadAuthorName';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的作者头像', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadAuthorAvatar';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的发布时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentThreadTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentPostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorName';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户头像', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorAvatar';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'MostRecentPostTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Forum', @level2type = N'COLUMN', @level2name = N'CreatedTime';
GO

CREATE TABLE [dbo].[Community_ForumGroup] (
    [SiteId]      INT            NOT NULL,
    [GroupId]     SMALLINT       NOT NULL,
    [Name]        NVARCHAR(50)  NOT NULL,
    [Icon]        VARCHAR(100)  NULL,
    [Ordinal]     SMALLINT       DEFAULT (0) NOT NULL,
    [Description] NVARCHAR(500) NULL,
    PRIMARY KEY CLUSTERED ([SiteId] ASC, [GroupId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，论坛组编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'GroupId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'论坛组名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'Name';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'论坛组图标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'Icon';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序权重', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'Ordinal';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumGroup', @level2type = N'COLUMN', @level2name = N'Description';
GO

CREATE TABLE [dbo].[Community_ForumUser] (
    [SiteId]      INT      NOT NULL,
    [ForumId]     SMALLINT NOT NULL,
    [UserId]      INT      NOT NULL,
    [Permission]  TINYINT  DEFAULT (0) NOT NULL,
    [IsModerator] BIT      DEFAULT (0) NOT NULL,
    PRIMARY KEY CLUSTERED ([SiteId] ASC, [ForumId] ASC, [UserId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumUser', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，论坛编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumUser', @level2type = N'COLUMN', @level2name = N'ForumId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumUser', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限定义', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumUser', @level2type = N'COLUMN', @level2name = N'Permission';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否为版主', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_ForumUser', @level2type = N'COLUMN', @level2name = N'IsModerator';
GO

CREATE TABLE [dbo].[Community_History] (
    [UserId]               INT      NOT NULL,
    [ThreadId]             BIGINT   NOT NULL,
    [Count]                INT      DEFAULT (1) NOT NULL,
    [FirstViewedTime]      DATETIME DEFAULT (getdate()) NOT NULL,
    [MostRecentViewedTime] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC, [ThreadId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_History', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，主题编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_History', @level2type = N'COLUMN', @level2name = N'ThreadId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'浏览次数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_History', @level2type = N'COLUMN', @level2name = N'Count';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'首次浏览时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_History', @level2type = N'COLUMN', @level2name = N'FirstViewedTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最近浏览时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_History', @level2type = N'COLUMN', @level2name = N'MostRecentViewedTime';
GO

CREATE TABLE [dbo].[Community_Message] (
    [MessageId]   BIGINT         NOT NULL,
    [SiteId]      INT            NOT NULL,
    [Subject]     NVARCHAR(100) NOT NULL,
    [Content]     NVARCHAR(500) NOT NULL,
    [ContentType] VARCHAR(50)   NULL,
    [MessageType] VARCHAR(50)   NULL,
    [Referer]     VARCHAR(50)   NULL,
    [Tags]        NVARCHAR(100) NULL,
    [CreatorId]   INT            DEFAULT (0) NOT NULL,
    [CreatedTime] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([MessageId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，消息编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'MessageId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息主题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'Subject';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'Content';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内容类型(MIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'ContentType';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'MessageType';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息来源', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'Referer';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标签集(以逗号分隔)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'Tags';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人编号(零表示系统消息)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Message', @level2type = N'COLUMN', @level2name = N'CreatedTime';
GO

CREATE TABLE [dbo].[Community_MessageUser] (
    [MessageId] BIGINT NOT NULL,
    [UserId]    INT    NOT NULL,
    [IsRead]    BIT    DEFAULT (0) NOT NULL,
    PRIMARY KEY CLUSTERED ([MessageId] ASC, [UserId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，消息编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_MessageUser', @level2type = N'COLUMN', @level2name = N'MessageId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_MessageUser', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已读', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_MessageUser', @level2type = N'COLUMN', @level2name = N'IsRead';
GO

CREATE TABLE [dbo].[Community_Post] (
    [PostId]             BIGINT         NOT NULL,
    [SiteId]             INT            NOT NULL,
    [ThreadId]           BIGINT         NOT NULL,
    [RefererId]          BIGINT         DEFAULT (0) NOT NULL,
    [Content]            NVARCHAR(500)   NOT NULL,
    [ContentType]        VARCHAR(50)    NULL,
    [Visible]            BIT            DEFAULT (1) NOT NULL,
    [Approved]           BIT            NOT NULL,
    [IsLocked]           BIT            DEFAULT (0) NOT NULL,
    [IsValued]           BIT            DEFAULT (0) NOT NULL,
    [TotalUpvotes]       INT            DEFAULT (0) NOT NULL,
    [TotalDownvotes]     INT            DEFAULT (0) NOT NULL,
    [VisitorAddress]     NVARCHAR(100)  NULL,
    [VisitorDescription] NVARCHAR(500)  NULL,
    [AttachmentMark]     VARCHAR(100)   NULL,
    [CreatorId]          INT            NOT NULL,
    [CreatedTime]        DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([PostId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'PostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属主题编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'ThreadId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'回复引用的帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'RefererId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'帖子内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'Content';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内容类型(MIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'ContentType';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否可见', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'Visible';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否审核通过', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'Approved';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否锁定', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'IsLocked';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否精华帖', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'IsValued';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总计点赞数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'TotalUpvotes';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总计被踩数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'TotalDownvotes';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'访客地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'VisitorAddress';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'访客描述(浏览器代理信息等)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'VisitorDescription';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'附件标识(以逗号分隔)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'AttachmentMark';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发帖人编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发帖时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Post', @level2type = N'COLUMN', @level2name = N'CreatedTime';
GO

CREATE TABLE [dbo].[Community_PostVoting] (
    [PostId]     BIGINT        NOT NULL,
    [UserId]     INT           NOT NULL,
    [UserName]   NVARCHAR(50) NULL,
    [UserAvatar] VARCHAR(100) NULL,
    [Value]      TINYINT       NOT NULL,
    [Tiemstamp]  DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([PostId] ASC, [UserId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'PostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'UserName';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户头像', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'UserAvatar';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票数(正数为Upvote，负数为Downvote)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'Value';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投票时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_PostVoting', @level2type = N'COLUMN', @level2name = N'Tiemstamp';
GO

CREATE TABLE [dbo].[Community_Thread] (
    [ThreadId]                   BIGINT         NOT NULL,
    [SiteId]                     INT            NOT NULL,
    [ForumId]                    SMALLINT       NOT NULL,
    [Title]                      NVARCHAR(50)  NOT NULL,
    [PinYin]                     VARCHAR(200)  NULL,
    [Summary]                    NVARCHAR(500) NULL,
    [Tags]                       NVARCHAR(100) NULL,
    [PostId]                     BIGINT         NOT NULL,
    [CoverPicturePath]           VARCHAR(200)  NULL,
    [ArticleUrl]                 VARCHAR(200)  NULL,
    [Visible]                    BIT            DEFAULT (1) NOT NULL,
    [Approved]                   BIT            NOT NULL,
    [IsLocked]                   BIT            DEFAULT (0) NOT NULL,
    [IsPinned]                   BIT            DEFAULT (0) NOT NULL,
    [IsValued]                   BIT            DEFAULT (0) NOT NULL,
    [IsGlobal]                   BIT            DEFAULT (0) NOT NULL,
    [TotalViews]                 INT            DEFAULT (0) NOT NULL,
    [TotalReplies]               INT            DEFAULT (0) NOT NULL,
    [ApprovedTime]               DATETIME       NULL,
    [ViewedTime]                 DATETIME       NULL,
    [MostRecentPostId]           BIGINT         NULL,
    [MostRecentPostAuthorId]     INT            NULL,
    [MostRecentPostAuthorName]   NVARCHAR(50)  NULL,
    [MostRecentPostAuthorAvatar] NVARCHAR(150) NULL,
    [MostRecentPostTime]         DATETIME       NULL,
    [CreatorId]                  INT            NOT NULL,
    [CreatedTime]                DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ThreadId] ASC)
);

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，主题编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'ThreadId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所属论坛编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'ForumId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'文章标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'Title';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标题拼音', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'PinYin';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内容概述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'Summary';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标签集(以逗号分隔)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'Tags';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内容帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'PostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'封面图片路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'CoverPicturePath';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外部文章链接', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'ArticleUrl';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否可见', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'Visible';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否已经审核通过', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'Approved';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否锁定', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'IsLocked';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否置顶', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'IsPinned';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否精华帖', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'IsValued';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否全局帖', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'IsGlobal';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累计被浏览数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'TotalViews';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'回帖总数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'TotalReplies';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'审核通过时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'ApprovedTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后被浏览时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'ViewedTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'MostRecentPostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorName';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的用户头像', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'MostRecentPostAuthorAvatar';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'MostRecentPostTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'CreatorId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_Thread', @level2type = N'COLUMN', @level2name = N'CreatedTime';
GO

CREATE TABLE [dbo].[Community_UserProfile] (
    [UserId]                INT            NOT NULL,
    [SiteId]                INT            NOT NULL,
    [Name]                  NVARCHAR(50)  NOT NULL,
    [Nickname]              NVARCHAR(50)  NULL,
    [Email]                 VARCHAR(50)   NULL,
    [Phone]                 VARCHAR(50)   NULL,
    [Avatar]                VARCHAR(100)  NULL,
    [Gender]                TINYINT        DEFAULT (0) NOT NULL,
    [Grade]                 TINYINT        DEFAULT (0) NOT NULL,
    [PhotoPath]             VARCHAR(200)  NULL,
    [TotalPosts]            INT            DEFAULT (0) NOT NULL,
    [TotalThreads]          INT            DEFAULT (0) NOT NULL,
    [MostRecentPostId]      BIGINT         NULL,
    [MostRecentPostTime]    DATETIME       NULL,
    [MostRecentThreadId]    BIGINT         NULL,
    [MostRecentThreadTitle] NVARCHAR(100) NULL,
    [MostRecentThreadTime]  DATETIME       NULL,
    [Creation]              DATETIME       DEFAULT (getdate()) NOT NULL,
    [Modification]          DATETIME       NULL,
    [Description]           NVARCHAR(500) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [UX_Community_UserProfile_Name] UNIQUE NONCLUSTERED ([SiteId] ASC, [Name] ASC)
);

CREATE UNIQUE NONCLUSTERED INDEX [UX_Community_UserProfile_Phone]
    ON [dbo].[Community_UserProfile]([SiteId] ASC, [Phone] ASC) WHERE ([Phone] IS NOT NULL);
CREATE UNIQUE NONCLUSTERED INDEX [UX_Community_UserProfile_Email]
    ON [dbo].[Community_UserProfile]([SiteId] ASC, [Email] ASC) WHERE ([Email] IS NOT NULL);

GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，用户编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'UserId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'站点编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'SiteId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Name';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户昵称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Nickname';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户绑定的邮箱地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Email';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户绑定的手机号码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Phone';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户头像', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Avatar';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户性别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Gender';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户等级', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Grade';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'照片路径', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'PhotoPath';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累计回复总数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'TotalPosts';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累计主题总数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'TotalThreads';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的帖子编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'MostRecentPostId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后回帖的时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'MostRecentPostTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新发布的主题编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'MostRecentThreadId';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新发布的主题标题', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'MostRecentThreadTitle';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新主题的发布时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'MostRecentThreadTime';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Creation';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后修改时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Modification';
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Community_UserProfile', @level2type = N'COLUMN', @level2name = N'Description';
GO
