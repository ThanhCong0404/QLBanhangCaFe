USE [master]
GO
/****** Object:  Database [qlbanhang]    Script Date: 4/19/2022 11:11:40 PM ******/
CREATE DATABASE [qlbanhang]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'qlbanhang', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\qlbanhang.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'qlbanhang_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\qlbanhang_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [qlbanhang] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [qlbanhang].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [qlbanhang] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [qlbanhang] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [qlbanhang] SET ARITHABORT OFF 
GO
ALTER DATABASE [qlbanhang] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [qlbanhang] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [qlbanhang] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [qlbanhang] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [qlbanhang] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [qlbanhang] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [qlbanhang] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [qlbanhang] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [qlbanhang] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [qlbanhang] SET  ENABLE_BROKER 
GO
ALTER DATABASE [qlbanhang] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [qlbanhang] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [qlbanhang] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [qlbanhang] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [qlbanhang] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [qlbanhang] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [qlbanhang] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [qlbanhang] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [qlbanhang] SET  MULTI_USER 
GO
ALTER DATABASE [qlbanhang] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [qlbanhang] SET DB_CHAINING OFF 
GO
ALTER DATABASE [qlbanhang] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [qlbanhang] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [qlbanhang] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [qlbanhang] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [qlbanhang] SET QUERY_STORE = OFF
GO
USE [qlbanhang]
GO
/****** Object:  Table [dbo].[CTHD]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTHD](
	[MaHD] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[Soluong] [int] NULL,
	[DongiaBan] [real] NULL,
	[Giamgia] [real] NULL,
 CONSTRAINT [PK_CTHD] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHD] [int] IDENTITY(1,1) NOT NULL,
	[MaKH] [int] NULL,
	[MaNV] [int] NULL,
	[NgayLapHD] [datetime] NOT NULL,
	[NgayGiaoHang] [datetime] NOT NULL,
	[TongTien] [real] NULL,
	[TinhTrangDonHang] [nchar](20) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](30) NULL,
	[DiaChi] [nvarchar](30) NULL,
	[DienThoai] [nvarchar](7) NULL,
	[Fax] [nvarchar](12) NULL,
	[Email] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](255) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiSP]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSP](
	[MaLoaiSP] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiSP] [nvarchar](255) NULL,
 CONSTRAINT [PK_LoaiSP] PRIMARY KEY CLUSTERED 
(
	[MaLoaiSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nhanvien]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nhanvien](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[HoNV] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[Diachi] [nvarchar](50) NULL,
	[Dienthoai] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[MatKhau] [nvarchar](255) NULL,
	[isAdmin] [bit] NULL,
 CONSTRAINT [PK_Nhanvien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 4/19/2022 11:11:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] IDENTITY(1,1) NOT NULL,
	[TenSP] [nvarchar](20) NULL,
	[Dongia] [float] NULL,
	[MaLoaiSP] [int] NULL,
	[HinhSP] [nvarchar](max) NULL,
	[SoLuong] [int] NOT NULL,
	[SoLuongDaBan] [int] NOT NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (18, 4, 5, 60000, NULL)
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (19, 4, 1, 12000, NULL)
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (20, 4, 2, 24000, NULL)
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (21, 5, 1, 30000, NULL)
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (22, 6, 1, 15000, NULL)
INSERT [dbo].[CTHD] ([MaHD], [MaSP], [Soluong], [DongiaBan], [Giamgia]) VALUES (23, 6, 1, 15000, NULL)
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (11, 17, 18, CAST(N'2022-04-11T11:30:00.537' AS DateTime), CAST(N'2022-04-11T11:30:00.537' AS DateTime), 100000, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (18, 17, 19, CAST(N'2022-04-13T09:52:02.147' AS DateTime), CAST(N'2022-04-13T09:52:02.147' AS DateTime), 60000, N'Đã xác nhận!        ')
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (19, 17, 19, CAST(N'2022-04-13T10:49:11.203' AS DateTime), CAST(N'2022-04-13T10:49:11.203' AS DateTime), 12000, N'Đã xác nhận!        ')
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (20, 17, 18, CAST(N'2022-04-13T10:53:12.027' AS DateTime), CAST(N'2022-04-13T10:53:12.027' AS DateTime), 24000, N'Đã xác nhận!        ')
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (21, 17, NULL, CAST(N'2022-04-13T10:53:56.563' AS DateTime), CAST(N'2022-04-13T10:53:56.563' AS DateTime), 54000, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (22, 17, NULL, CAST(N'2022-04-13T11:14:52.457' AS DateTime), CAST(N'2022-04-13T11:14:52.457' AS DateTime), 15000, NULL)
INSERT [dbo].[HoaDon] ([MaHD], [MaKH], [MaNV], [NgayLapHD], [NgayGiaoHang], [TongTien], [TinhTrangDonHang]) VALUES (23, 25, NULL, CAST(N'2022-04-13T15:26:42.617' AS DateTime), CAST(N'2022-04-13T15:26:42.617' AS DateTime), 57000, NULL)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [MatKhau]) VALUES (17, N'123456', N'123', N'123', N'123', N'123@gmail.com', N'202cb962ac59075b964b07152d234b70')
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [DiaChi], [DienThoai], [Fax], [Email], [MatKhau]) VALUES (25, N'khach hang1', N'123', N'123', N'', N'thanhcong04042001@gmail.com', N'e10adc3949ba59abbe56e057f20f883e')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiSP] ON 

INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (6, N'Cafe')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (7, N'Cafe Gói')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (8, N'Rượu')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (10, N'Nước Ngọt')
INSERT [dbo].[LoaiSP] ([MaLoaiSP], [TenLoaiSP]) VALUES (11, N'Bánh')
SET IDENTITY_INSERT [dbo].[LoaiSP] OFF
GO
SET IDENTITY_INSERT [dbo].[Nhanvien] ON 

INSERT [dbo].[Nhanvien] ([MaNV], [HoNV], [Ten], [Diachi], [Dienthoai], [Email], [MatKhau], [isAdmin]) VALUES (18, NULL, N'NhanVien02', NULL, NULL, N'nhanvien@gmail.com', N'123456', NULL)
INSERT [dbo].[Nhanvien] ([MaNV], [HoNV], [Ten], [Diachi], [Dienthoai], [Email], [MatKhau], [isAdmin]) VALUES (19, NULL, N'ADMIN', NULL, NULL, N'admin@gmail.com', N'123456', 1)
SET IDENTITY_INSERT [dbo].[Nhanvien] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (4, N'Cafe đá', 12000, 6, N'cafe đá.jpg', 95, 6)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (5, N'Cafe espresso', 30000, 6, N'cafe espresso.jpg', 98, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (6, N'7 up', 15000, 10, N'7Up.jpg', 98, 2)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (7, N'Blue Mountain', 150000, 7, N'Blue Mountain Coffee.jpg', 100, 0)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (8, N'capuchino', 200000, 7, N'cafe cappucino.jpg', 100, 0)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (9, N'Ruou Vang Do', 1200000, 8, N'ruouchatdo.jpg', 100, 0)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (10, N'Cafe Moca', 300000, 7, N'cafe Moka.jpg', 100, 0)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (11, N'Nestcafe', 140000, 7, N'caffe late.jpg', 100, 0)
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Dongia], [MaLoaiSP], [HinhSP], [SoLuong], [SoLuongDaBan]) VALUES (12, N'Cafe Sữa', 20000, 6, N'caffesua.jpg', 100, 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
ALTER TABLE [dbo].[Nhanvien] ADD  CONSTRAINT [DF_Nhanvien_isAdmin]  DEFAULT ((0)) FOR [isAdmin]
GO
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuongDaBan]  DEFAULT ((0)) FOR [SoLuongDaBan]
GO
ALTER TABLE [dbo].[CTHD]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_HoaDon] FOREIGN KEY([MaHD])
REFERENCES [dbo].[HoaDon] ([MaHD])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CTHD] CHECK CONSTRAINT [FK_CTHD_HoaDon]
GO
ALTER TABLE [dbo].[CTHD]  WITH CHECK ADD  CONSTRAINT [FK_CTHD_SanPham] FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CTHD] CHECK CONSTRAINT [FK_CTHD_SanPham]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_Nhanvien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[Nhanvien] ([MaNV])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_Nhanvien]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_LoaiSP] FOREIGN KEY([MaLoaiSP])
REFERENCES [dbo].[LoaiSP] ([MaLoaiSP])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_LoaiSP]
GO
USE [master]
GO
ALTER DATABASE [qlbanhang] SET  READ_WRITE 
GO
