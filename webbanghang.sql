USE [master]
GO
/****** Object:  Database [QLWebBanHang]    Script Date: 17/08/2023 10:29:16 PM ******/
CREATE DATABASE [QLWebBanHang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLWebBanHang', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER1\MSSQL\DATA\QLWebBanHang.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLWebBanHang_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER1\MSSQL\DATA\QLWebBanHang_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLWebBanHang] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLWebBanHang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLWebBanHang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLWebBanHang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLWebBanHang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLWebBanHang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLWebBanHang] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLWebBanHang] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLWebBanHang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLWebBanHang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLWebBanHang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLWebBanHang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLWebBanHang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLWebBanHang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLWebBanHang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLWebBanHang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLWebBanHang] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLWebBanHang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLWebBanHang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLWebBanHang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLWebBanHang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLWebBanHang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLWebBanHang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLWebBanHang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLWebBanHang] SET RECOVERY FULL 
GO
ALTER DATABASE [QLWebBanHang] SET  MULTI_USER 
GO
ALTER DATABASE [QLWebBanHang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLWebBanHang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLWebBanHang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLWebBanHang] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLWebBanHang] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLWebBanHang] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLWebBanHang', N'ON'
GO
ALTER DATABASE [QLWebBanHang] SET QUERY_STORE = OFF
GO
USE [QLWebBanHang]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [nvarchar](50) NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[OrderDate] [date] NULL,
	[AmountPaid] [int] NULL,
	[CustomerID] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailID] [nvarchar](50) NULL,
	[OrderID] [nvarchar](50) NOT NULL,
	[ProductID] [nvarchar](50) NOT NULL,
	[Quantity] [int] NULL,
	[Unit] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_OrderDetail_1] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductDescription] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[Prices] [int] NULL,
	[SupplierID] [nvarchar](50) NULL,
	[Image] [nvarchar](255) NULL,
 CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingCart]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCart](
	[CustomerID] [nvarchar](50) NOT NULL,
	[ProductID] [nvarchar](50) NOT NULL,
	[Quantity] [int] NULL,
	[Unit] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_ShoppingCart_1] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 17/08/2023 10:29:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [nvarchar](50) NOT NULL,
	[SupplierName] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Distributor] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'96af0cc7-d83e-4b2c-bd5e-c44876ed6858', N'Liêu', N'Liêu', N'3232 đâu đó', NULL)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'e7d96a43-45a3-434d-91b7-925bc58dced1', N'Lê', N'Văn', N'222 Nguyễn Thượng Hiền', NULL)
GO
INSERT [dbo].[Order] ([OrderID], [PaymentMethod], [OrderDate], [AmountPaid], [CustomerID]) VALUES (N'8169ab62-b459-4d86-84ea-262d56f043b7', NULL, CAST(N'2023-08-17' AS Date), 170000, N'96af0cc7-d83e-4b2c-bd5e-c44876ed6858')
GO
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'07477ad3-49ff-476a-9a08-56b8ed6e728d', 2, 20000, 40000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'26a7fb84-b585-47f6-a15a-ddfaad799b0b', 1, 70000, 70000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'48ad425e-f7e9-4215-bff5-9a59212431d3', 2, 30000, 60000)
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'07477ad3-49ff-476a-9a08-56b8ed6e728d', N'Hồng sim', N'Trái hồng sim/ Sapoche ngon', 10, 20000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'sapoche.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'26a7fb84-b585-47f6-a15a-ddfaad799b0b', N'Mảng cầu gai', N'Mảng cầu gai siêu nhiều hạt, ngon ngọt', 10, 70000, N'ee536776-87c7-486e-8c5b-8f50b65a706a', N'mang-cau-gai.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'48ad425e-f7e9-4215-bff5-9a59212431d3', N'Táo xanh', N'Táo xanh siêu nhỏ không hạt', 10, 30000, N'fd9502a9-6a34-4f4b-960e-a9ffe40d60c3', N'tao-xanh.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'7b8dbd7f-7b1d-404e-9dfb-1832cb48e380', N'Nho 2 màu', N'2 chùm nho màu xanh và tím', 1, 40000, N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'chum-nho.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'93e13c14-f914-408a-b9d1-a0f64e70a104', N'Sầu riêng', N'Sầu riêng cơm vàng bao ngon', 20, 130000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'sau-rieng.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'a50641b3-f354-4798-832d-71ab22b1add0', N'Táo', N'Táo tào siêu nhăn', 2, 50000, N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'tao-tau.jpg')
GO
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'5f46580b-a570-4988-874c-c6bd803ef33a', N'Nguyễn Hoàng Phúc', N'Sĩ/lẽ trái cây nhiệt đới')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'Nguyễn Văn Linh', N'Nhà phân phối số 1')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'ee536776-87c7-486e-8c5b-8f50b65a706a', N'Đỗ Minh Khương', N'Cung cấp các loại trái cây tươi mới với giá cả hợp lý')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'fd9502a9-6a34-4f4b-960e-a9ffe40d60c3', N'Trần Tuấn Huynh', N'Chuyên cung cấp trái cây sĩ lẽ trên toàn quốc')
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Distributor] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Distributor]
GO
ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[ShoppingCart] CHECK CONSTRAINT [FK_ShoppingCart_Customer]
GO
ALTER TABLE [dbo].[ShoppingCart]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCart_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[ShoppingCart] CHECK CONSTRAINT [FK_ShoppingCart_Product]
GO
USE [master]
GO
ALTER DATABASE [QLWebBanHang] SET  READ_WRITE 
GO
