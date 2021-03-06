IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogFiles]') AND type in (N'U'))
DROP TABLE [dbo].[LogFiles]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[LogFiles](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[ServerId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[Filename] [varchar] (50) NOT NULL,
	[Timestamp] [datetime] NOT NULL CONSTRAINT [DF_LogFiles_Timestamp] DEFAULT (getdate())
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LogFiles] ADD CONSTRAINT [PK_LogFiles] PRIMARY KEY
	CLUSTERED
	(
		[Id] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_All] ON [dbo].[LogFiles]
(
	[ServerId] ASC,
	[SiteId] ASC,
	[Filename] ASC,
	[Timestamp] ASC
)ON [PRIMARY]
GO

