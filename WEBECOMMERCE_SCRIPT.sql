USE [master]
GO
/****** Object:  Database [QLWebBanHang]    Script Date: 24/08/2023 4:30:27 PM ******/
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
/****** Object:  Table [dbo].[Customer]    Script Date: 24/08/2023 4:30:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 24/08/2023 4:30:28 PM ******/
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
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](11) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 24/08/2023 4:30:28 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 24/08/2023 4:30:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductDescription] [nvarchar](255) NULL,
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
/****** Object:  Table [dbo].[ShoppingCart]    Script Date: 24/08/2023 4:30:28 PM ******/
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
/****** Object:  Table [dbo].[Supplier]    Script Date: 24/08/2023 4:30:28 PM ******/
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
/****** Object:  Table [dbo].[UserLogin]    Script Date: 24/08/2023 4:30:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](255) NOT NULL,
	[PassWord] [nchar](255) NOT NULL,
	[VaiTro] [nvarchar](50) NULL,
	[CustomerID] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'1f1d03b1-a00f-407b-96ed-f728a9ed40cb', N'mới', N'tài khoản', N'231 dô', N'093210313')
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'31e1e9d4-84e3-4ae6-83c1-1b34abc37b72', NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'38072962-b006-4b1e-9ac7-a27837f26662', N'hàng', N'Khách ', N'123 đâu đó', N'0112333401')
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'47ce4e1b-1271-4989-9063-204fdf3aaadb', NULL, NULL, NULL, NULL)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'96af0cc7-d83e-4b2c-bd5e-c44876ed6858', N'Liêu', N'Liêu', N'3232 đâu đó', NULL)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'e7d96a43-45a3-434d-91b7-925bc58dced1', N'Lê', N'Văn', N'222 Nguyễn Thượng Hiền', NULL)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'f37da477-2a64-41d7-91ec-9a1b4c519f0e', N'Và điệp', N'Lan', N'100 nguyễn gì đó', N'9132312313')
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'f6b88f9f-8de8-44a2-9632-839b516d3200', N'nam', N'nam', N'122', N'019231312')
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [PhoneNumber]) VALUES (N'ff0e3ce7-8013-4c72-b629-6c0b9909085d', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Order] ([OrderID], [PaymentMethod], [OrderDate], [AmountPaid], [CustomerID], [Address], [Phone]) VALUES (N'5b56affc-3040-4fbd-9395-e2586cc36e34', NULL, CAST(N'2023-08-24' AS Date), 1560000, N'38072962-b006-4b1e-9ac7-a27837f26662', NULL, NULL)
INSERT [dbo].[Order] ([OrderID], [PaymentMethod], [OrderDate], [AmountPaid], [CustomerID], [Address], [Phone]) VALUES (N'673f8d81-ae59-4cc0-923e-73721acdea75', NULL, CAST(N'2023-08-24' AS Date), 160000, N'38072962-b006-4b1e-9ac7-a27837f26662', NULL, NULL)
INSERT [dbo].[Order] ([OrderID], [PaymentMethod], [OrderDate], [AmountPaid], [CustomerID], [Address], [Phone]) VALUES (N'8169ab62-b459-4d86-84ea-262d56f043b7', NULL, CAST(N'2023-08-17' AS Date), 170000, N'96af0cc7-d83e-4b2c-bd5e-c44876ed6858', NULL, NULL)
GO
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'5b56affc-3040-4fbd-9395-e2586cc36e34', N'93e13c14-f914-408a-b9d1-a0f64e70a104', 12, 130000, 1560000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'673f8d81-ae59-4cc0-923e-73721acdea75', N'48ad425e-f7e9-4215-bff5-9a59212431d3', 4, 30000, 120000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'673f8d81-ae59-4cc0-923e-73721acdea75', N'7b8dbd7f-7b1d-404e-9dfb-1832cb48e380', 1, 40000, 40000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'07477ad3-49ff-476a-9a08-56b8ed6e728d', 2, 20000, 40000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'26a7fb84-b585-47f6-a15a-ddfaad799b0b', 1, 70000, 70000)
INSERT [dbo].[OrderDetail] ([OrderDetailID], [OrderID], [ProductID], [Quantity], [Unit], [Total]) VALUES (NULL, N'8169ab62-b459-4d86-84ea-262d56f043b7', N'48ad425e-f7e9-4215-bff5-9a59212431d3', 2, 30000, 60000)
GO
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'07477ad3-49ff-476a-9a08-56b8ed6e728d', N'Hồng sim', N'Trái hồng sim/ Sapoche ngon', 10, 20000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'sapoche.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'24312240-c771-4b44-a418-2ac75d9da6dc', N'Sơ ri', N'một loại trái cây giàu dinh dưỡng của thực vật có tên khoa học là Malpighia Emarginata. Malpighia Emarginata là một loại cây bụi nhiệt đới hoặc cây gỗ nhỏ thuộc họ Malpighiaceae. Loại cây ăn quả này có nguồn gốc từ Nam Mỹ và miền nam Mexico. ', 100, 30000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'so-ri.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'26a7fb84-b585-47f6-a15a-ddfaad799b0b', N'Mảng cầu gai', N'Mảng cầu gai siêu nhiều hạt, ngon ngọt', 10, 70000, N'ee536776-87c7-486e-8c5b-8f50b65a706a', N'mang-cau-gai.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'48628886-9ee7-4b50-b479-933845bc7ae5', N'Mít tố nữ', N'Mít tố nữ/ mít ước ngon ngọt ', 10, 40000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'mit-to-nu.webp')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'48ad425e-f7e9-4215-bff5-9a59212431d3', N'Táo xanh', N'Táo xanh siêu nhỏ không hạt', 10, 30000, N'fd9502a9-6a34-4f4b-960e-a9ffe40d60c3', N'tao-xanh.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'7b8dbd7f-7b1d-404e-9dfb-1832cb48e380', N'Nho 2 màu', N'2 chùm nho màu xanh và tím', 1, 40000, N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'chum-nho.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'82a907e6-2e97-4e5a-9110-07587723364f', N'Dâu Đà lạt', N'Dâu đà lạc to ngon, mọng nước. Đến từ nhà phân phối lớn nhất nhì châu Á Nguyễn Văn Linh.', 12, 230000, N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'dau-da-lat.gif')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'93e13c14-f914-408a-b9d1-a0f64e70a104', N'Sầu riêng', N'Sầu riêng cơm vàng bao ngon', 8, 130000, N'5f46580b-a570-4988-874c-c6bd803ef33a', N'sau-rieng.jpg')
INSERT [dbo].[Product] ([ProductID], [ProductName], [ProductDescription], [Quantity], [Prices], [SupplierID], [Image]) VALUES (N'a50641b3-f354-4798-832d-71ab22b1add0', N'Táo', N'Táo tào siêu nhăn', 2, 50000, N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'tao-tau.jpg')
GO
INSERT [dbo].[ShoppingCart] ([CustomerID], [ProductID], [Quantity], [Unit], [Total]) VALUES (N'1f1d03b1-a00f-407b-96ed-f728a9ed40cb', N'26a7fb84-b585-47f6-a15a-ddfaad799b0b', 1, 70000, 70000)
GO
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'5f46580b-a570-4988-874c-c6bd803ef33a', N'Nguyễn Hoàng Phúc', N'Sĩ/lẽ trái cây nhiệt đới')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'b145e702-7ffb-46ce-99a0-3127d87ae756', N'Nguyễn Văn Linh', N'Nhà phân phối số 1')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'ee536776-87c7-486e-8c5b-8f50b65a706a', N'Đỗ Minh Khương', N'Cung cấp các loại trái cây tươi mới với giá cả hợp lý')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [Description]) VALUES (N'fd9502a9-6a34-4f4b-960e-a9ffe40d60c3', N'Trần Tuấn Huynh', N'Chuyên cung cấp trái cây sĩ lẽ trên toàn quốc')
GO
SET IDENTITY_INSERT [dbo].[UserLogin] ON 

INSERT [dbo].[UserLogin] ([UID], [UserName], [PassWord], [VaiTro], [CustomerID]) VALUES (2, N'hehe                                                                                                                                                                                                                                                           ', N'123456                                                                                                                                                                                                                                                         ', N'customer', N'1f1d03b1-a00f-407b-96ed-f728a9ed40cb')
INSERT [dbo].[UserLogin] ([UID], [UserName], [PassWord], [VaiTro], [CustomerID]) VALUES (7, N'khachhang1                                                                                                                                                                                                                                                     ', N'khachhang1                                                                                                                                                                                                                                                     ', N'customer', N'38072962-b006-4b1e-9ac7-a27837f26662')
SET IDENTITY_INSERT [dbo].[UserLogin] OFF
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
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_UserLogin_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_UserLogin_Customer]
GO
USE [master]
GO
ALTER DATABASE [QLWebBanHang] SET  READ_WRITE 
GO
