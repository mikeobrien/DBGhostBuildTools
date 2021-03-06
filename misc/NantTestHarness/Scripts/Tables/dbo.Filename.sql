IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Filename]') AND type in (N'U'))
DROP TABLE [dbo].[Filename]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Filename](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[Name] [varchar] (900) NOT NULL
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Name] ON [dbo].[Filename]
(
	[Name] ASC
)ON [PRIMARY]
GO

