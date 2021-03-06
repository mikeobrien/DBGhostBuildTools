IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[File]') AND type in (N'U'))
DROP TABLE [dbo].[File]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[File](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[ServerId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
	[DomainId] [int] NOT NULL,
	[Port] [int] NOT NULL,
	[PathId] [int] NOT NULL,
	[FilenameId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[File] ADD CONSTRAINT [PK_File] PRIMARY KEY
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

CREATE NONCLUSTERED INDEX [IX_All] ON [dbo].[File]
(
	[ServerId] ASC,
	[SiteId] ASC,
	[DomainId] ASC,
	[Port] ASC,
	[PathId] ASC,
	[FilenameId] ASC
)ON [PRIMARY]
GO

