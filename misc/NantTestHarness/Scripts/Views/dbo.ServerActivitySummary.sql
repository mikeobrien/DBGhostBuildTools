IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServerActivitySummary]'))
DROP VIEW [dbo].[ServerActivitySummary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ServerActivitySummary]
AS
SELECT 
[Server].Name,
MAX(Activity.Date) AS LatestActivity
FROM [Server] INNER JOIN [File]
	ON [Server].Id=[File].ServerId
	INNER JOIN Activity
		ON [File].Id = Activity.FileId
GROUP BY [Server].Id, [Server].Name

GO

