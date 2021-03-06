IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnumerateLogFileHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[EnumerateLogFileHistory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EnumerateLogFileHistory] 
@ServerName varchar(20)
AS
BEGIN
	SELECT S.Name AS SiteName, [Filename] 
	FROM LogFiles L INNER JOIN [Site] S ON L.SiteID=S.Id INNER JOIN Server SVR ON L.ServerId=SVR.Id
	WHERE SVR.Name=@ServerName
END

GO

