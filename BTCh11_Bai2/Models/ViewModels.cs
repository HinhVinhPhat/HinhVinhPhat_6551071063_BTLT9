namespace BTCh11_Bai2.Models;

/// <summary>Một dòng của lưới (kết quả JOIN KhachThue + PhongTro)</summary>
public class KhachRow
{
    public int MaKhach { get; set; }
    public string HoTen { get; set; }
    public string SoDienThoai { get; set; }
    public string TenPhong { get; set; }
    public decimal GiaThue { get; set; }
    public string QueQuan { get; set; }
    public bool DaTamTru { get; set; }
}

/// <summary>Item cho ComboBox phòng</summary>
public record RoomItem(int MaPhong, string Text);
