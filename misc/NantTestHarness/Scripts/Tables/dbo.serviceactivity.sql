IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceactivity]') AND type in (N'U'))
DROP TABLE [dbo].[serviceactivity]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[serviceactivity](
	[Site] [varchar] (1000) NOT NULL,
	[Domain] [varchar] (500) NOT NULL,
	[Path] [varchar] (900) NOT NULL,
	[Name] [varchar] (900) NOT NULL,
	[Status] [varchar] (50) NULL,
	[SSLHits] [int] NULL,
	[NonSSLHits] [int] NULL,
	[RMWEB1Hits] [int] NULL,
	[RMWEB2TotalHits] [int] NULL,
	[TotalHits] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL
) ON [PRIMARY]
GO

