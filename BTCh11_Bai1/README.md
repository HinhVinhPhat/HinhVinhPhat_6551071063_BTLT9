# BTCh11_Bai1 – Sổ chi tiêu cá nhân

WinForms .NET 10, EF Core Database First, SQL Server, Serilog.

## Chạy thử
1. Mở SSMS, chạy `Database/QL_ChiTieu.sql` (tạo CSDL `QL_ChiTieu` + 12 dòng mẫu).
2. Mở `BTCh11_Bai1.slnx` bằng Visual Studio, chờ NuGet restore, bấm F5.
3. Connection string nằm trong `appsettings.json` và `appsettings.Development.json` (đang là `localhost\SQLEXPRESS`).

## Scaffold lại Model (nếu muốn dùng lệnh thật)
```
dotnet ef dbcontext scaffold "Server=localhost\SQLEXPRESS;Database=QL_ChiTieu;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --no-onconfiguring --force
```
Lưu ý: bản scaffold sinh `DateOnly` cho cột `date`, khớp với code hiện tại.

## Cấu hình
- `appsettings.json`: cấu hình chính. `appsettings.Development.json` ghi đè khi lập trình (build Debug); bản publish Release không chép file này.
- Hai file trên nằm trong `.gitignore`; dùng `appsettings.example.json` làm mẫu.
- Cả hai đặt `Copy to Output Directory = Copy if newer`.

## Ghi log
Serilog ghi `Logs/log-yyyyMMdd.txt` cạnh file .exe: Information khi Thêm/Sửa/Xóa (kèm MaCT), Error khi lỗi CSDL.

## Publish (Framework-Dependent, win-x64)
```
dotnet publish BTCh11_Bai1/BTCh11_Bai1.csproj -c Release -r win-x64 --self-contained false -o publish
```
(hoặc chuột phải project → Publish → chọn profile `FrameworkDependent-win-x64`).
Chép thư mục `publish` sang máy khác đã cài **.NET Desktop Runtime 10**, sửa connection string trong `appsettings.json` cho đúng máy đó (không cần build lại) rồi chạy `BTCh11_Bai1.exe`.
