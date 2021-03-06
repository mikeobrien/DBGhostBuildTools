IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[WebSiteActivity]'))
DROP VIEW [dbo].[WebSiteActivity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[WebSiteActivity]
AS
SELECT 
ST.Description AS Site,
D.Name AS Domain, 
P.Path AS [Path],
FN.Name AS [Name],
(
SELECT TOP 1 Code 
FROM Activity INNER JOIN WebSiteFiles 
ON Activity.FileId=WebSiteFiles.FileId AND
WebSiteFiles.Domain=D.Name AND
WebSiteFiles.Path=P.Path AND
WebSiteFiles.Name=FN.Name AND
Activity.Date=MAX(A.Date) INNER JOIN Status
ON Activity.StatusId=Status.Id
) AS [Status],
SUM(CASE WHEN Port=443 THEN Hits ELSE 0 END) AS SSLHits,
SUM(CASE WHEN Port <> 443 THEN Hits ELSE 0 END) AS NonSSLHits,
SUM(CASE WHEN S.Name='RMWEB1' THEN Hits ELSE 0 END) AS RMWEB1Hits,
SUM(CASE WHEN S.Name='RMWEB2' THEN Hits ELSE 0 END) AS RMWEB2TotalHits,
SUM(Hits) AS TotalHits,
MIN(Date) AS StartDate,
MAX(Date) AS EndDate
FROM ((((([File] F INNER JOIN [Domain] D ON F.DomainId=D.Id) 
		INNER JOIN [Path] P ON F.PathId=P.Id) 
			INNER JOIN [Filename] FN ON F.FilenameId=FN.Id) 
				INNER JOIN [Server] S ON F.ServerId=S.Id ) 
					INNER JOIN [Site] ST ON F.SiteId=ST.Id )
						INNER JOIN Activity A ON A.FileId=F.Id
GROUP BY ST.Description, D.Name, P.Path, FN.Name

GO

