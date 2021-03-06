IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Server]') AND type in (N'U'))
DROP TABLE [dbo].[Server]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Server](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[Name] [varchar] (50) NOT NULL CONSTRAINT [DF_Servers_ServerName] DEFAULT ('')
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Server] ADD CONSTRAINT [PK_Server] PRIMARY KEY
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

CREATE NONCLUSTERED INDEX [IX_Name] ON [dbo].[Server]
(
	[Name] ASC
)ON [PRIMARY]
GO

