namespace BTCh11_Bai1.Models;

/// <summary>Một dòng hiển thị trên DataGridView (NgayChi dùng DateTime để định dạng dễ).</summary>
public class ChiTieuRow
{
    public int MaCT { get; set; }
    public DateTime NgayChi { get; set; }
    public string NoiDung { get; set; }
    public string LoaiChi { get; set; }
    public decimal SoTien { get; set; }
    public string GhiChu { get; set; }
}
