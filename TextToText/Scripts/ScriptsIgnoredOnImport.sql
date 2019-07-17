﻿
USE [master]
GO

/****** Object:  Database [TextToText]    Script Date: 16-07-2019 11:54:33 ******/
CREATE DATABASE [TextToText] ON  PRIMARY 
( NAME = N'TextToText', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\TextToText\TextToText.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TextToText_log', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\TextToText\TextToText_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [TextToText] SET COMPATIBILITY_LEVEL = 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TextToText].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TextToText] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [TextToText] SET ANSI_NULLS OFF
GO

ALTER DATABASE [TextToText] SET ANSI_PADDING OFF
GO

ALTER DATABASE [TextToText] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [TextToText] SET ARITHABORT OFF
GO

ALTER DATABASE [TextToText] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [TextToText] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [TextToText] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [TextToText] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [TextToText] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [TextToText] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [TextToText] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [TextToText] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [TextToText] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [TextToText] SET  DISABLE_BROKER
GO

ALTER DATABASE [TextToText] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [TextToText] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [TextToText] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [TextToText] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [TextToText] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [TextToText] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [TextToText] SET RECOVERY SIMPLE
GO

ALTER DATABASE [TextToText] SET  MULTI_USER
GO

ALTER DATABASE [TextToText] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [TextToText] SET DB_CHAINING OFF
GO

USE [TextToText]
GO

/****** Object:  Table [dbo].[TextText]    Script Date: 16-07-2019 11:54:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

SET ANSI_PADDING OFF
GO

USE [master]
GO

ALTER DATABASE [TextToText] SET  READ_WRITE
GO