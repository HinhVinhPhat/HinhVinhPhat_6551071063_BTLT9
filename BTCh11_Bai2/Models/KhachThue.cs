namespace BTCh11_Bai2.Models;

public partial class KhachThue
{
    public int MaKhach { get; set; }
    public string HoTen { get; set; } = null!;
    public string GioiTinh { get; set; } = null!;
    public DateOnly NgaySinh { get; set; }
    public string SoDienThoai { get; set; }
    public int? MaTinh { get; set; }
    public string TenTinh { get; set; }
    public int? MaPhuongXa { get; set; }
    public string TenPhuongXa { get; set; }
    public bool DaDangKyTamTru { get; set; }
    public int MaPhong { get; set; }

    // Navigation property: khách -> phòng
    public virtual PhongTro MaPhongNavigation { get; set; } = null!;
}
