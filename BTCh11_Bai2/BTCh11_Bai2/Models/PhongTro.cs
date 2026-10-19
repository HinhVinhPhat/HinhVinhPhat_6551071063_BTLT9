namespace BTCh11_Bai2.Models;

public partial class PhongTro
{
    public int MaPhong { get; set; }
    public string TenPhong { get; set; } = null!;
    public decimal GiaThue { get; set; }
    public string TinhTrang { get; set; } = null!;

    // Navigation property: 1 phòng - nhiều khách
    public virtual ICollection<KhachThue> KhachThues { get; set; } = new List<KhachThue>();
}
