using System.Globalization;
using BTCh11_Bai1.Config;
using BTCh11_Bai1.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BTCh11_Bai1.Forms;

public partial class FormChiTieu : Form
{
    private static readonly string[] DanhSachLoai = { "Ăn uống", "Đi lại", "Học tập", "Giải trí", "Khác" };

    private readonly DbContextOptions<QlChiTieuContext> _opt;
    private int? _selectedId;

    public FormChiTieu(AppSettings settings)
    {
        InitializeComponent();
        _opt = new DbContextOptionsBuilder<QlChiTieuContext>()
            .UseSqlServer(settings.ConnectionStrings.QL_ChiTieu).Options;

        cboLoai.Items.AddRange(DanhSachLoai);
        cboLoc.Items.Add("Tất cả");
        cboLoc.Items.AddRange(DanhSachLoai);
        dtpNgayChi.MaxDate = DateTime.Today;
        dtpDen.MaxDate = DateTime.Today;
        SetupGrid();
        ResetFilters();
        ClearForm();
    }

    private QlChiTieuContext NewDb() => new(_opt);

    private void FormChiTieu_Load(object sender, EventArgs e) => LoadGrid();

    /// <summary>Bọc truy cập CSDL: lỗi thì ghi log Error + báo người dùng thông báo thân thiện.</summary>
    private bool Run(Action act, string what)
    {
        try { act(); return true; }
        catch (Exception ex)
        {
            Log.Error(ex, "Lỗi CSDL khi {What}", what);
            MessageBox.Show($"Không thể {what}. Vui lòng kiểm tra kết nối cơ sở dữ liệu rồi thử lại.",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    // ===================== Lưới =====================
    private void SetupGrid()
    {
        dgv.Columns.Clear();
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "MaCT", HeaderText = "Mã", Width = 60, DefaultCellStyle = new DataGridViewCellStyle { Format = "000" } });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "NgayChi", HeaderText = "Ngày chi", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "NoiDung", HeaderText = "Nội dung", Width = 220 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "LoaiChi", HeaderText = "Loại chi", Width = 100 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "SoTien", HeaderText = "Số tiền", Width = 110,
          DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        { DataPropertyName = "GhiChu", HeaderText = "Ghi chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
    }

    private void ResetFilters()
    {
        txtTim.Clear();
        cboLoc.SelectedIndex = 0;
        var today = DateTime.Today;
        dtpTu.Value = new DateTime(today.Year, today.Month, 1);   // mặc định: tháng này
        dtpDen.Value = today;
    }

    private void LoadGrid()
    {
        if (dtpTu.Value.Date > dtpDen.Value.Date)
        {
            MessageBox.Show("\"Từ ngày\" không được lớn hơn \"Đến ngày\".", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var tu = DateOnly.FromDateTime(dtpTu.Value);
        var den = DateOnly.FromDateTime(dtpDen.Value);
        var kw = txtTim.Text.Trim();
        var loai = cboLoc.SelectedIndex > 0 ? cboLoc.Text : null;

        Run(() =>
        {
            using var db = NewDb();
            var q = db.ChiTieus.AsNoTracking().Where(c => c.NgayChi >= tu && c.NgayChi <= den);
            if (kw != "") q = q.Where(c => c.NoiDung.Contains(kw));
            if (loai != null) q = q.Where(c => c.LoaiChi == loai);

            var rows = q.OrderBy(c => c.NgayChi).ThenBy(c => c.MaCT).ToList()
                .Select(c => new ChiTieuRow
                {
                    MaCT = c.MaCT, NgayChi = c.NgayChi.ToDateTime(TimeOnly.MinValue),
                    NoiDung = c.NoiDung, LoaiChi = c.LoaiChi, SoTien = c.SoTien, GhiChu = c.GhiChu
                }).ToList();

            dgv.DataSource = rows;
            dgv.ClearSelection();
            // Tổng chi tự cập nhật theo kết quả đang hiển thị
            lblTong.Text = $"Tổng chi: {rows.Sum(r => r.SoTien).ToString("N0", CultureInfo.CurrentCulture)} đ";
        }, "tải danh sách chi tiêu");
    }

    // ===================== Form <-> dữ liệu =====================
    private void ClearForm()
    {
        _selectedId = null;
        dtpNgayChi.Value = DateTime.Today;
        txtNoiDung.Clear();
        cboLoai.SelectedIndex = 0;
        nudSoTien.Value = 0;
        txtGhiChu.Clear();
        dgv.ClearSelection();
    }

    private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].DataBoundItem is not ChiTieuRow r) return;
        _selectedId = r.MaCT;
        dtpNgayChi.Value = r.NgayChi < dtpNgayChi.MinDate ? dtpNgayChi.MinDate : r.NgayChi;
        txtNoiDung.Text = r.NoiDung;
        cboLoai.SelectedItem = r.LoaiChi;
        if (cboLoai.SelectedIndex < 0) cboLoai.SelectedIndex = DanhSachLoai.Length - 1;
        nudSoTien.Value = Math.Min(r.SoTien, nudSoTien.Maximum);
        txtGhiChu.Text = r.GhiChu ?? "";
    }

    private bool Fail(string msg, Control focus)
    {
        MessageBox.Show(msg, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        focus.Focus();
        return false;
    }

    /// <summary>Kiểm tra hợp lệ rồi đọc form thành entity.</summary>
    private bool TryReadForm(out ChiTieu c)
    {
        c = null;
        if (string.IsNullOrWhiteSpace(txtNoiDung.Text)) return Fail("Nội dung không được để trống.", txtNoiDung);
        if (nudSoTien.Value <= 0) return Fail("Số tiền phải lớn hơn 0.", nudSoTien);
        if (dtpNgayChi.Value.Date > DateTime.Today) return Fail("Ngày chi không được lớn hơn hôm nay.", dtpNgayChi);
        if (cboLoai.SelectedIndex < 0) return Fail("Vui lòng chọn loại chi.", cboLoai);

        c = new ChiTieu
        {
            NgayChi = DateOnly.FromDateTime(dtpNgayChi.Value),
            NoiDung = txtNoiDung.Text.Trim(),
            LoaiChi = cboLoai.Text,
            SoTien = nudSoTien.Value,
            GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim()
        };
        return true;
    }

    /// <summary>Sau khi thêm/sửa: mở rộng khoảng ngày nếu khoản chi nằm ngoài, rồi làm mới lưới.</summary>
    private void AfterChange(DateTime? ngay = null)
    {
        if (ngay is DateTime d)
        {
            if (d.Date < dtpTu.Value.Date) dtpTu.Value = d.Date;
            if (d.Date > dtpDen.Value.Date) dtpDen.Value = d.Date;
        }
        ClearForm();
        LoadGrid();
    }

    // ===================== CRUD =====================
    private void btnThem_Click(object sender, EventArgs e)
    {
        if (!TryReadForm(out var c)) return;
        if (Run(() =>
        {
            using var db = NewDb();
            db.ChiTieus.Add(c);
            db.SaveChanges();
        }, "thêm khoản chi"))
        {
            Log.Information("Thêm khoản chi MaCT={MaCT}", c.MaCT);
            AfterChange(c.NgayChi.ToDateTime(TimeOnly.MinValue));
        }
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (_selectedId == null)
        {
            MessageBox.Show("Vui lòng chọn một dòng trong danh sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (!TryReadForm(out var n)) return;
        int id = _selectedId.Value;
        if (Run(() =>
        {
            using var db = NewDb();
            var c = db.ChiTieus.Find(id) ?? throw new InvalidOperationException($"Khoản chi {id} không còn tồn tại");
            c.NgayChi = n.NgayChi; c.NoiDung = n.NoiDung; c.LoaiChi = n.LoaiChi; c.SoTien = n.SoTien; c.GhiChu = n.GhiChu;
            db.SaveChanges();
        }, "sửa khoản chi"))
        {
            Log.Information("Sửa khoản chi MaCT={MaCT}", id);
            AfterChange(n.NgayChi.ToDateTime(TimeOnly.MinValue));
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_selectedId == null)
        {
            MessageBox.Show("Vui lòng chọn một dòng trong danh sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show($"Bạn có chắc muốn xóa khoản chi \"{txtNoiDung.Text}\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        int id = _selectedId.Value;
        if (Run(() =>
        {
            using var db = NewDb();
            var c = db.ChiTieus.Find(id);
            if (c == null) return;
            db.ChiTieus.Remove(c);
            db.SaveChanges();
        }, "xóa khoản chi"))
        {
            Log.Information("Xóa khoản chi MaCT={MaCT}", id);
            AfterChange();
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        ClearForm();
        ResetFilters();
        LoadGrid();
    }

    // ===================== Tìm kiếm =====================
    private void btnTim_Click(object sender, EventArgs e) => LoadGrid();
}
