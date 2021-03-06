IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Status](
	[Id] [int] NOT NULL IDENTITY (1,1),
	[Code] [varchar] (50) NOT NULL,
	[Description] [varchar] (100) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Status] ADD CONSTRAINT [PK_StatusCode] PRIMARY KEY
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

CREATE NONCLUSTERED INDEX [IX_Code] ON [dbo].[Status]
(
	[Code] ASC
)ON [PRIMARY]
GO

