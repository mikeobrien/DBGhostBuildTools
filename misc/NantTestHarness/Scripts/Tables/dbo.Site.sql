IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Site]') AND type in (N'U'))
DROP TABLE [dbo].[Site]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Site](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[Name] [varchar] (50) NOT NULL,
	[Description] [varchar] (1000) NOT NULL CONSTRAINT [DF_Site_Description] DEFAULT ('')
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Site] ADD CONSTRAINT [PK_Site] PRIMARY KEY
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

CREATE NONCLUSTERED INDEX [IX_Name] ON [dbo].[Site]
(
	[Name] ASC
)ON [PRIMARY]
GO

