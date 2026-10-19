-- Script tạo CSDL QL_NhaTro + dữ liệu mẫu (SQL Server)
IF DB_ID(N'QL_NhaTro') IS NULL CREATE DATABASE QL_NhaTro;
GO
USE QL_NhaTro;
GO
IF OBJECT_ID('dbo.KhachThue') IS NOT NULL DROP TABLE dbo.KhachThue;
IF OBJECT_ID('dbo.PhongTro') IS NOT NULL DROP TABLE dbo.PhongTro;
GO
CREATE TABLE dbo.PhongTro (
    MaPhong   INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong  NVARCHAR(20)  NOT NULL,
    GiaThue   DECIMAL(18,0) NOT NULL,
    TinhTrang NVARCHAR(20)  NOT NULL
        CONSTRAINT DF_PhongTro_TinhTrang DEFAULT N'Trống'
        CONSTRAINT CK_PhongTro_TinhTrang CHECK (TinhTrang IN (N'Trống', N'Đang thuê'))
);
CREATE TABLE dbo.KhachThue (
    MaKhach        INT IDENTITY(1,1) PRIMARY KEY,
    HoTen          NVARCHAR(100) NOT NULL,
    GioiTinh       NVARCHAR(5)   NOT NULL,
    NgaySinh       DATE          NOT NULL,
    SoDienThoai    VARCHAR(15)   NULL,
    MaTinh         INT           NULL,
    TenTinh        NVARCHAR(100) NULL,
    MaPhuongXa     INT           NULL,
    TenPhuongXa    NVARCHAR(100) NULL,
    DaDangKyTamTru BIT           NOT NULL CONSTRAINT DF_KhachThue_TamTru DEFAULT 0,
    MaPhong        INT           NOT NULL,
    CONSTRAINT FK_KhachThue_PhongTro FOREIGN KEY (MaPhong) REFERENCES dbo.PhongTro(MaPhong)
);
GO
INSERT dbo.PhongTro (TenPhong, GiaThue, TinhTrang) VALUES
(N'P01', 1500000, N'Đang thuê'),
(N'P02', 1600000, N'Đang thuê'),
(N'P03', 1800000, N'Đang thuê'),
(N'P04', 2000000, N'Đang thuê'),
(N'P05', 1700000, N'Đang thuê'),
(N'P06', 1900000, N'Trống');

-- Mã tỉnh/phường để NULL cho dữ liệu mẫu: form sẽ tự khớp theo tên khi API khả dụng
INSERT dbo.KhachThue (HoTen, GioiTinh, NgaySinh, SoDienThoai, MaTinh, TenTinh, MaPhuongXa, TenPhuongXa, DaDangKyTamTru, MaPhong) VALUES
(N'Nguyễn Văn Nam', N'Nam', '2003-03-15', '0912345678', NULL, N'Thành phố Hà Nội', NULL, N'Phường Ba Đình', 1, 3),
(N'Nguyễn Văn An',  N'Nam', '2004-05-12', '0912345679', NULL, N'Thành phố Hà Nội', NULL, N'Phường Ngọc Hà', 1, 3),
(N'Trần Thị Bình',  N'Nữ',  '2002-08-20', '0876543210', NULL, N'Tỉnh Ninh Bình',   NULL, N'Phường Nam Định', 0, 1),
(N'Lê Văn Cường',   N'Nam', '2001-11-02', '0901234567', NULL, N'Tỉnh Thanh Hóa',   NULL, NULL,               0, 2),
(N'Phạm Minh Đức',  N'Nam', '2000-01-30', '0987654321', NULL, N'Tỉnh Thanh Hóa',   NULL, NULL,               1, 4),
(N'Hoàng Thị Dung', N'Nữ',  '2003-09-09', '0765432109', NULL, N'Tỉnh Bắc Ninh',    NULL, NULL,               0, 5);
GO