
CREATE DATABASE QL_DatTour;
GO

USE QL_DatTour;
GO

-- 1. Bảng Tour
CREATE TABLE Tour
(
    MaTour INT IDENTITY(1,1) PRIMARY KEY,
    TenTour NVARCHAR(100) NOT NULL,
    DiemDen NVARCHAR(50) NOT NULL,
    NgoaiTe CHAR(3) NOT NULL,
    GiaNgoaiTe DECIMAL(18,2) NOT NULL,
    NgayKhoiHanh DATE NOT NULL,
    SoChoToiDa INT NOT NULL,

    CONSTRAINT CK_Tour_NgoaiTe
        CHECK (NgoaiTe IN ('THB','KRW','JPY','USD')),
    CONSTRAINT CK_Tour_Gia
        CHECK (GiaNgoaiTe > 0),
    CONSTRAINT CK_Tour_SoCho
        CHECK (SoChoToiDa > 0)
);
GO

-- 2. Bảng KhachHang
CREATE TABLE KhachHang
(
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email NVARCHAR(100)
);
GO

-- 3. Bảng DatTour
CREATE TABLE DatTour
(
    MaDat INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL,
    MaTour INT NOT NULL,
    SoNguoi INT NOT NULL,
    NgayDat DATETIME NOT NULL
        DEFAULT GETDATE(),
    TyGia DECIMAL(18,4) NOT NULL,
    TongTienVND DECIMAL(18,0) NOT NULL,

    CONSTRAINT FK_DatTour_KhachHang
        FOREIGN KEY (MaKH)
        REFERENCES KhachHang(MaKH),

    CONSTRAINT FK_DatTour_Tour
        FOREIGN KEY (MaTour)
        REFERENCES Tour(MaTour),

    CONSTRAINT CK_DatTour_SoNguoi
        CHECK (SoNguoi > 0),
    CONSTRAINT CK_DatTour_TyGia
        CHECK (TyGia > 0),
    CONSTRAINT CK_DatTour_TongTien
        CHECK (TongTienVND >= 0)
);
GO

-- 4. Thêm 5 tour mẫu
INSERT INTO Tour
(TenTour, DiemDen, NgoaiTe,
 GiaNgoaiTe, NgayKhoiHanh, SoChoToiDa)
VALUES
(N'Bangkok - Pattaya', N'Thái Lan',
 'THB', 12000, '2026-11-10', 30),

(N'Seoul - Nami', N'Hàn Quốc',
 'KRW', 850000, '2026-11-15', 25),

(N'Tokyo - Phú Sĩ', N'Nhật Bản',
 'JPY', 150000, '2026-12-01', 20),

(N'Singapore - Sentosa', N'Singapore',
 'USD', 650, '2026-12-10', 35),

(N'New York - Washington', N'Mỹ',
 'USD', 2500, '2026-12-20', 20);
GO

-- 5. Thêm khách hàng mẫu
INSERT INTO KhachHang
(HoTen, SoDienThoai, Email)
VALUES
(N'Nguyễn Văn An',
 '0901234567', 'an@gmail.com'),

(N'Trần Thị Bình',
 '0912345678', 'binh@gmail.com'),

(N'Lê Minh Cường',
 '0987654321', 'cuong@gmail.com'),

(N'Phạm Thị Lan',
 '0934567890', 'lan@gmail.com'),

(N'Hoàng Văn Nam',
 '0976543210', 'nam@gmail.com');
GO

-- 6. Kiểm tra dữ liệu
SELECT * FROM Tour;
SELECT * FROM KhachHang;
SELECT * FROM DatTour;
