CREATE TABLE IF NOT EXISTS `Community_Message`
(
  `MessageId`   bigint unsigned NOT NULL COMMENT '主键，消息编号',
  `SiteId`      int unsigned    NOT NULL COMMENT '站点编号',
  `Subject`     nvarchar(50)    NOT NULL COMMENT '消息标题',
  `Content`     nvarchar(500)   NOT NULL COMMENT '消息内容',
  `ContentType` varchar(50)     NULL     COMMENT '内容类型',
  `MessageType` varchar(50)     NULL     COMMENT '消息类型',
  `Referer`     varchar(50)     NULL     COMMENT '消息来源',
  `Tags`        nvarchar(100)   NULL     COMMENT '标签集',
  `CreatorId`   int unsigned    NOT NULL COMMENT '创建人编号',
  `CreatedTime` datetime NOT    NULL DEFAULT NOW() COMMENT '创建时间',
  PRIMARY KEY (`MessageId`)
) ENGINE=MergeTree COMMENT '消息表';
