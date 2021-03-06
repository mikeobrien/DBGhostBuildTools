IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Activity]') AND type in (N'U'))
DROP TABLE [dbo].[Activity]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Activity](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[FileId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Hits] [int] NOT NULL,
	[StatusId] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Activity] ADD CONSTRAINT [PK_Activity] PRIMARY KEY
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

CREATE NONCLUSTERED INDEX [IX_All] ON [dbo].[Activity]
(
	[FileId] ASC,
	[Date] ASC,
	[StatusId] ASC
)ON [PRIMARY]
GO

