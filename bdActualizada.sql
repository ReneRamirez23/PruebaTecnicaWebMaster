USE [master]
GO
/****** Object:  Database [BD_ControlVentas]    Script Date: 23/06/2024 16:33:52 ******/
CREATE DATABASE [BD_ControlVentas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_ControlVentas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_ControlVentas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BD_ControlVentas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_ControlVentas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BD_ControlVentas] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_ControlVentas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_ControlVentas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_ControlVentas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_ControlVentas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BD_ControlVentas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_ControlVentas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET RECOVERY FULL 
GO
ALTER DATABASE [BD_ControlVentas] SET  MULTI_USER 
GO
ALTER DATABASE [BD_ControlVentas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_ControlVentas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_ControlVentas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_ControlVentas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BD_ControlVentas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BD_ControlVentas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BD_ControlVentas', N'ON'
GO
ALTER DATABASE [BD_ControlVentas] SET QUERY_STORE = ON
GO
ALTER DATABASE [BD_ControlVentas] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BD_ControlVentas]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 23/06/2024 16:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[IdProducts] [int] IDENTITY(1,1) NOT NULL,
	[NameProducts] [nvarchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducts] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 23/06/2024 16:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[IdSale] [int] IDENTITY(1,1) NOT NULL,
	[Client] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](200) NOT NULL,
	[MailClient] [nvarchar](50) NOT NULL,
	[TotalPrice] [money] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[PaidDate] [datetime] NOT NULL,
	[IsPaid] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesProducts]    Script Date: 23/06/2024 16:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesProducts](
	[IdSP] [int] IDENTITY(1,1) NOT NULL,
	[SalesId] [int] NOT NULL,
	[ProductsId] [int] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/06/2024 16:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[TypeUser] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Mail] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (1, N'Coca', 20.0000, 2, 1)
INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (2, N'arina', 2.0000, 23, 1)
INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (8, N'prueva', 2.0000, 7, 0)
INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (12, N'Apple', 1.5000, 15, 1)
INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (16, N'Galleta', 0.7500, 20, 1)
INSERT [dbo].[Products] ([IdProducts], [NameProducts], [UnitPrice], [Quantity], [Active]) VALUES (17, N'Jugo', 1.2500, 12, 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (1, N'rene ramirez', N'afcaefvfvawr', N'rene.ramirez@prueva.com', 110.0000, CAST(N'2024-06-16T22:45:25.267' AS DateTime), CAST(N'2024-06-17T01:54:56.357' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (2, N'Antonio Amaya', N'asderfgtr', N'antonio.amaya@prueva.com', 28.0000, CAST(N'2024-06-17T00:35:53.580' AS DateTime), CAST(N'2024-06-17T01:59:31.983' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (3, N'roberto', N'fojnfwojrnf jnwrjfg', N'roberto@prueva.com', 6.0000, CAST(N'2024-06-17T00:48:01.277' AS DateTime), CAST(N'2024-06-22T00:25:56.263' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (4, N'jorge', N'asdf fgtreds', N'jorge@prueva.com', 23.5000, CAST(N'2024-06-21T23:08:39.163' AS DateTime), CAST(N'2024-06-21T23:08:39.163' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (5, N'antonio', N'asdf fgtreds', N'antonio@prueva.com', 4.5000, CAST(N'2024-06-21T23:32:59.643' AS DateTime), CAST(N'2024-06-23T14:20:14.713' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (6, N'antonio2', N'asdf fgtreds', N'antonio@prueva.com', 23.5000, CAST(N'2024-06-22T14:16:58.237' AS DateTime), CAST(N'2024-06-22T14:16:58.237' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (7, N'chepe', N'asderf gdsbhsb', N'chepe@prueva.com', 8.0000, CAST(N'2024-06-22T15:13:56.547' AS DateTime), CAST(N'2024-06-22T15:13:56.550' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (8, N'', N'', N'', 0.0000, CAST(N'2024-06-22T16:45:32.800' AS DateTime), CAST(N'2024-06-22T16:45:51.533' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (9, N'', N'', N'', 0.0000, CAST(N'2024-06-22T23:39:36.903' AS DateTime), CAST(N'2024-06-23T16:11:50.717' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (10, N'Diego', N'asdersbgvt thbthnjyrfj', N'diego@prueva.com', 226.5000, CAST(N'2024-06-23T10:50:55.690' AS DateTime), CAST(N'2024-06-23T10:50:55.690' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (11, N'antonio', N'asderf gdsbhsb', N'antonio@prueva.com', 250.0000, CAST(N'2024-06-23T10:58:49.443' AS DateTime), CAST(N'2024-06-23T10:58:49.443' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [Client], [Descripcion], [MailClient], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (12, N'neto', N'asdersbgvt thbthnjyrfj', N'neto@prueva.com', 12.7500, CAST(N'2024-06-23T16:19:08.710' AS DateTime), CAST(N'2024-06-23T16:19:08.710' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesProducts] ON 

INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (1, 1, 1, 5)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (2, 1, 2, 5)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (3, 2, 1, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (4, 2, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (5, 2, 8, 2)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (6, 3, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (7, 4, 1, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (8, 4, 2, 0)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (9, 4, 8, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (10, 4, 12, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (11, 5, 12, 3)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (12, 6, 12, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (13, 6, 2, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (14, 6, 1, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (15, 7, 2, 4)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (16, 8, 1, 0)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (17, 8, 12, 0)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (18, 10, 12, 1)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (19, 10, 16, 3)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (20, 11, 17, 2)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (21, 12, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (22, 12, 12, 4)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (23, 12, 16, 2)
INSERT [dbo].[SalesProducts] ([IdSP], [SalesId], [ProductsId], [Quantity]) VALUES (24, 12, 17, 1)
SET IDENTITY_INSERT [dbo].[SalesProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [TypeUser], [FirstName], [LastName], [Mail], [Password]) VALUES (2, N'accountant', N'Antonio', N'Amaya', N'Antonio.Amaya@prueba.com', N'1234')
INSERT [dbo].[User] ([IdUser], [TypeUser], [FirstName], [LastName], [Mail], [Password]) VALUES (3, N'seller', N'Josue', N'Hernandez', N'Josue.Hernandez@prueba.com', N'1234')
INSERT [dbo].[User] ([IdUser], [TypeUser], [FirstName], [LastName], [Mail], [Password]) VALUES (4, N'admin', N'Rene', N'Ramirez', N'Rene.Ramirez@prueba.com', N'1234')
INSERT [dbo].[User] ([IdUser], [TypeUser], [FirstName], [LastName], [Mail], [Password]) VALUES (7, N'admin', N'Julio', N'Rodriguez', N'julio.rodriguez@prueba.com', N'123458')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Products] FOREIGN KEY([ProductsId])
REFERENCES [dbo].[Products] ([IdProducts])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Products]
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Sales] FOREIGN KEY([SalesId])
REFERENCES [dbo].[Sales] ([IdSale])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Sales]
GO
USE [master]
GO
ALTER DATABASE [BD_ControlVentas] SET  READ_WRITE 
GO
