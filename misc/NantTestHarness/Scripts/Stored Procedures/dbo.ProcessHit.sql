IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcessHit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcessHit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProcessHit]
@ServerName varchar(50),
@SiteName varchar(50),
@DomainName varchar(500),
@Path varchar(900),
@Filename varchar(200),
@Port int,
@Date datetime,
@StatusCode varchar(50),
@LogFilename varchar(50)
AS

DECLARE 
@ServerId int, 
@SiteId int, 
@DomainId int, 
@PathId int, 
@FilenameId int,
@StatusId int,
@FileId int

SET @ServerName = LTRIM(RTRIM(@ServerName))
SET @SiteName = LTRIM(RTRIM(@SiteName))
SET @DomainName = LTRIM(RTRIM(@DomainName))
SET @Path = LTRIM(RTRIM(@Path))
SET @Filename = LTRIM(RTRIM(@Filename))
SET @StatusCode = LTRIM(RTRIM(@StatusCode))
SET @LogFilename = LTRIM(RTRIM(@LogFilename))
SET @Date = CAST(FLOOR(CAST(@Date AS FLOAT)) AS datetime)

SELECT @ServerId=Id FROM [Server] WHERE [Name]=@ServerName
IF @ServerId IS NULL 
BEGIN
	INSERT INTO [Server] ([Name]) VALUES (@ServerName)
	SET @ServerId = SCOPE_IDENTITY()
END

SELECT @SiteId=Id FROM [Site] WHERE [Name]=@SiteName
IF @SiteId IS NULL 
BEGIN
	INSERT INTO [Site] ([Name]) VALUES (@SiteName)
	SET @SiteId = SCOPE_IDENTITY()
END

SELECT @DomainId=Id FROM [Domain] WHERE [Name]=@DomainName
IF @DomainId IS NULL 
BEGIN
	INSERT INTO [Domain] ([Name]) VALUES (@DomainName)
	SET @DomainId = SCOPE_IDENTITY()
END

SELECT @PathId=Id FROM [Path] WHERE [Path]=@Path
IF @PathId IS NULL 
BEGIN
	INSERT INTO [Path] ([Path]) VALUES (@Path)
	SET @PathId = SCOPE_IDENTITY()
END

SELECT @FilenameId=Id FROM [Filename] WHERE [Name]=@Filename
IF @FilenameId IS NULL 
BEGIN
	INSERT INTO [Filename] ([Name]) VALUES (@Filename)
	SET @FilenameId = SCOPE_IDENTITY()
END

SELECT @StatusId=Id FROM [Status] WHERE [Code]=@StatusCode
IF @StatusId IS NULL 
BEGIN
	INSERT INTO [Status] ([Code]) VALUES (@StatusCode)
	SET @StatusId = SCOPE_IDENTITY()
END

SELECT @FileId=Id FROM [File] WHERE 
ServerId=@ServerId AND
SiteId=@SiteId AND
DomainId=@DomainId AND
Port=@Port AND
PathId=@PathId AND
FilenameId=@FilenameId
IF @FileId IS NULL 
BEGIN
	INSERT INTO [File] (ServerId, SiteId, DomainId, Port, PathId, FilenameId) VALUES 
	(@ServerId, @SiteId, @DomainId, @Port, @PathId, @FilenameId)
	SET @FileId = SCOPE_IDENTITY()
END

IF NOT EXISTS (SELECT Id FROM LogFiles WHERE ServerId=@ServerId AND SiteId=@SiteId AND [Filename]=@LogFilename)
	INSERT INTO LogFiles (ServerId, SiteId, [Filename]) VALUES
	(@ServerId, @SiteId, @LogFilename)

DECLARE @ActivityId int 

SELECT @ActivityId=Id 
FROM Activity 
WHERE FileID=@FileId AND Date=@Date

IF NOT @ActivityId IS NULL
	UPDATE Activity SET Hits=Hits + 1, StatusId=@StatusId WHERE Id=@ActivityId
ELSE
	INSERT INTO Activity 
	(FileId, Date, Hits, StatusId) VALUES
	(@FileId, @Date, 1, @StatusId)

GO

