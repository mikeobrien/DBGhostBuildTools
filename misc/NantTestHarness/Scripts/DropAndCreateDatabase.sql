USE master
GO

IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'Initech')
DROP DATABASE [Initech]
GO

/********************************************************************************************************************************************************************

   This script may be manually reused by carefully search/replacing the database name that this script originally created.

********************************************************************************************************************************************************************/

/********************************************************************************************************************************************************************

   The create database and the alter database statements were created using the settings from the database [Initech] on the server [HAL].

   It is necessary to change some settings to enable the build to progress smoothly. The following settings are commented out and or changed.

   SIZE - This attribute of the database and log files will not be used so the minimum amount of disk space is used as generally the build
           does not require any more disk space than the default setting.
   MAXSIZE - This attribute of the database and log files is not used so the database and log files can expand when necessary.
   FILEGROWTH - This attribute of the database and log files is not used so the database and log files can expand using as minimal space as is necessary.
   RECOVERY - The recovery mode will always be set to SIMPLE so log space is not used up for this process.
   READ_ONLY - This setting will always be READ_WRITE and the original setting will be commented out so the build can proceed.
   READONLY FILEGROUP - File groups will not have this setting as we may there may be tables and or indexes that are associated with the filegroup before the filegroup was set to READONLY.

********************************************************************************************************************************************************************/

CREATE DATABASE [Initech] ON PRIMARY
( NAME = N'Initech', FILENAME = N'd:\Data\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Initech.mdf'/*, SIZE = 100352KB, MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB*/)
 LOG ON
( NAME = N'Initech_log', FILENAME = N'd:\Data\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Initech_log.ldf'/*, SIZE = 15040KB, MAXSIZE = 2097152MB, FILEGROWTH = 10%*/)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

IF(SELECT is_default FROM [Initech].sys.filegroups WHERE [name]=N'PRIMARY')=0	ALTER DATABASE [Initech] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO

USE [Initech]
GO

/********************************************************************************************************************************************************************

   The alter database statements are created using the settings from the database [Initech] on the server [HAL].

********************************************************************************************************************************************************************/

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET COMPATIBILITY_LEVEL = 100'
EXEC (@alterStmt)


GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
BEGIN
DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'EXEC [' + @dbName + '].[dbo].[sp_fulltext_database] @action = ''enable'''
EXEC (@alterStmt)

END
GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ANSI_NULL_DEFAULT OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ANSI_NULLS OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ANSI_PADDING OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ANSI_WARNINGS OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ARITHABORT OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET AUTO_CLOSE OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET AUTO_CREATE_STATISTICS ON'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET AUTO_SHRINK OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET AUTO_UPDATE_STATISTICS ON'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET CURSOR_CLOSE_ON_COMMIT OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET CURSOR_DEFAULT GLOBAL'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET CONCAT_NULL_YIELDS_NULL OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET NUMERIC_ROUNDABORT OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET QUOTED_IDENTIFIER OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET RECURSIVE_TRIGGERS OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET DISABLE_BROKER'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET AUTO_UPDATE_STATISTICS_ASYNC OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET DATE_CORRELATION_OPTIMIZATION OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET TRUSTWORTHY OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET ALLOW_SNAPSHOT_ISOLATION OFF'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET PARAMETERIZATION SIMPLE'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET READ_WRITE'
EXEC (@alterStmt)

GO

/*********************************************************************
   Original setting based on template database has been commented out.
ALTER DATABASE [] SET RECOVERY FULL
*********************************************************************/
DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET RECOVERY SIMPLE'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET MULTI_USER'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET PAGE_VERIFY CHECKSUM'
EXEC (@alterStmt)

GO

DECLARE @dbName sysname SET @dbName = db_name()
DECLARE @alterStmt NVARCHAR(4000)
SET @alterStmt = 'ALTER DATABASE [' + @dbName + '] SET DB_CHAINING OFF'
EXEC (@alterStmt)

GO

/*** DBG PACKAGE INCLUDE ***/

GO

