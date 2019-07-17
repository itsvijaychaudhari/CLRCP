﻿
USE [master]
GO

/****** Object:  Database [TEXT]    Script Date: 16-07-2019 11:53:44 ******/
CREATE DATABASE [TEXT] ON  PRIMARY 
( NAME = N'TEXT', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\TEXT\TEXT.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TEXT_log', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\TEXT\TEXT_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [TEXT] SET COMPATIBILITY_LEVEL = 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TEXT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TEXT] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [TEXT] SET ANSI_NULLS OFF
GO

ALTER DATABASE [TEXT] SET ANSI_PADDING OFF
GO

ALTER DATABASE [TEXT] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [TEXT] SET ARITHABORT OFF
GO

ALTER DATABASE [TEXT] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [TEXT] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [TEXT] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [TEXT] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [TEXT] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [TEXT] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [TEXT] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [TEXT] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [TEXT] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [TEXT] SET  DISABLE_BROKER
GO

ALTER DATABASE [TEXT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [TEXT] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [TEXT] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [TEXT] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [TEXT] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [TEXT] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [TEXT] SET RECOVERY SIMPLE
GO

ALTER DATABASE [TEXT] SET  MULTI_USER
GO

ALTER DATABASE [TEXT] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [TEXT] SET DB_CHAINING OFF
GO

USE [TEXT]
GO

/****** Object:  Table [dbo].[Text]    Script Date: 16-07-2019 11:53:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

ALTER DATABASE [TEXT] SET  READ_WRITE
GO
