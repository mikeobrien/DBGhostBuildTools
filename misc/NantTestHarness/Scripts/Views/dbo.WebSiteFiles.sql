IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[WebSiteFiles]'))
DROP VIEW [dbo].[WebSiteFiles]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[WebSiteFiles]
AS
SELECT DISTINCT
F.Id AS FileId,
D.Name AS Domain, 
P.Path AS [Path],
FN.Name AS [Name]
FROM ((([File] F INNER JOIN [Domain] D ON F.DomainId=D.Id) 
		INNER JOIN [Path] P ON F.PathId=P.Id) 
			INNER JOIN [Filename] FN ON F.FilenameId=FN.Id)

GO

