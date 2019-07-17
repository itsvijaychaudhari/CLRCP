﻿
USE [master]
GO

/****** Object:  Database [IMAGE]    Script Date: 16-07-2019 11:52:55 ******/
CREATE DATABASE [IMAGE] ON  PRIMARY 
( NAME = N'IMAGE', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\IMAGE\IMAGE.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IMAGE_log', FILENAME = N'E:\CLRCP\Database\MSSQL_DATA\IMAGE\IMAGE_log.ldf' , SIZE = 1792KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [IMAGE] SET COMPATIBILITY_LEVEL = 90
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IMAGE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [IMAGE] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [IMAGE] SET ANSI_NULLS OFF
GO

ALTER DATABASE [IMAGE] SET ANSI_PADDING OFF
GO

ALTER DATABASE [IMAGE] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [IMAGE] SET ARITHABORT OFF
GO

ALTER DATABASE [IMAGE] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [IMAGE] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [IMAGE] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [IMAGE] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [IMAGE] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [IMAGE] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [IMAGE] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [IMAGE] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [IMAGE] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [IMAGE] SET  DISABLE_BROKER
GO

ALTER DATABASE [IMAGE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [IMAGE] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [IMAGE] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [IMAGE] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [IMAGE] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [IMAGE] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [IMAGE] SET RECOVERY SIMPLE
GO

ALTER DATABASE [IMAGE] SET  MULTI_USER
GO

ALTER DATABASE [IMAGE] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [IMAGE] SET DB_CHAINING OFF
GO

USE [IMAGE]
GO

/****** Object:  Table [dbo].[Images]    Script Date: 16-07-2019 11:52:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

ALTER DATABASE [IMAGE] SET  READ_WRITE
GO
