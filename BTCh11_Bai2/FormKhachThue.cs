using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using BTCh11_Bai2.Config;
using BTCh11_Bai2.Models;
using BTCh11_Bai2.Services;
using Serilog;

namespace BTCh11_Bai2;

public partial class FormKhachThue : Form
{
    // false: khi thêm mới chỉ liệt kê phòng "Trống". Đặt true nếu cho phép ở ghép.
    private static readonly bool ChoPhepOGhep = false;

    private readonly DbContextOptions<QlNhaTroContext> _opt;
    private readonly ProvinceService _api;
    private readonly Dictionary<int, List<AddressItem>> _wardCache = new();
    private bool _suppress, _apiOffline, _warned;
    private int _wardReq;
    private int? _selectedId;

    public FormKhachThue(AppConfig cfg)
    {
        InitializeComponent();
        dtpNgaySinh.MaxDate = DateTime.Today;
        _opt = new DbContextOptionsBuilder<QlNhaTroContext>()
            .UseSqlServer(cfg.ConnectionStrings.QL_NhaTro).Options;
        _api = new ProvinceService(cfg.ProvinceApi.BaseUrl);
        SetupGrid();
    }

    private QlNhaTroContext NewDb() => new(_opt);

    // ===================== Khởi tạo =====================
    private async void FormKhachThue_Load(object sender, EventArgs e)
    {
        LoadRoomInput(null);
        LoadFilters();
        LoadGrid();
        ClearForm();
        await LoadProvincesAsync();   // không chặn giao diện
        ClearForm();
    }

    private void FormKhachThue_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.Shift && e.KeyCode == Keys.D) btnGayLoi.Visible = !btnGayLoi.Visible;
    }

    private void btnGayLoi_Click(object sender, EventArgs e)
        => throw new InvalidOperationException("Lỗi thử nghiệm từ nút 'Gây lỗi thử'");

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

    // ===================== API Tỉnh / Phường-Xã =====================
    private static void BindCombo(ComboBox c, List<AddressItem> list)
    {
        c.DropDownStyle = ComboBoxStyle.DropDownList;
        c.DisplayMember = "Name";
        c.ValueMember = "Code";
        c.DataSource = list;
        c.SelectedIndex = -1;
    }

    private void SetLoading(ComboBox c)
    {
        bool old = _suppress; _suppress = true;
        BindCombo(c, new List<AddressItem> { new(0, "Đang tải…") });
        c.SelectedIndex = 0;
        c.Enabled = false;
        _suppress = old;
    }

    private void EnableManual(ComboBox c)   // chế độ gõ tay khi API lỗi
    {
        bool old = _suppress; _suppress = true;
        c.DataSource = null;
        c.DropDownStyle = ComboBoxStyle.DropDown;
        c.Enabled = true;
        c.Text = "";
        _suppress = old;
    }

    private void NotifyNetwork()
    {
        if (_warned) return;
        _warned = true;
        MessageBox.Show("Không tải được danh sách địa chỉ, vui lòng kiểm tra mạng.\nBạn vẫn có thể gõ tay Phường/Xã để lưu khách.",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private async Task LoadProvincesAsync()
    {
        SetLoading(cboTinh);
        SetLoading(cboPhuongXa);
        lblStatus.Text = "Đang tải danh sách tỉnh/thành…";
        try
        {
            var list = await _api.GetProvincesAsync();
            _suppress = true;
            BindCombo(cboTinh, list);
            BindCombo(cboPhuongXa, new List<AddressItem>());
            _suppress = false;
            cboTinh.Enabled = true;
            cboPhuongXa.Enabled = false;
            lblStatus.Text = $"Đã tải {list.Count} tỉnh/thành từ provinces.open-api.vn";
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            _apiOffline = true;
            Log.Warning(ex, "Không tải được danh sách tỉnh/thành");
            EnableManual(cboTinh);
            EnableManual(cboPhuongXa);
            lblStatus.Text = "Không tải được danh sách địa chỉ – nhập tay Tỉnh/Phường-Xã";
            NotifyNetwork();
        }
    }

    private async Task LoadWardsAsync(int provinceCode)
    {
        int req = ++_wardReq;
        SetLoading(cboPhuongXa);
        try
        {
            if (!_wardCache.TryGetValue(provinceCode, out var list))
            {
                list = await _api.GetWardsAsync(provinceCode);
                _wardCache[provinceCode] = list;
            }
            if (req != _wardReq) return;   // người dùng đã chọn tỉnh khác
            _suppress = true;
            BindCombo(cboPhuongXa, list);
            _suppress = false;
            cboPhuongXa.Enabled = true;
            lblStatus.Text = $"Đã tải {list.Count} phường/xã";
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or JsonException)
        {
            if (req != _wardReq) return;
            Log.Warning(ex, "Không tải được phường/xã của tỉnh {ProvinceCode}", provinceCode);
            EnableManual(cboPhuongXa);
            lblStatus.Text = "Không tải được phường/xã – nhập tay";
            NotifyNetwork();
        }
    }

    private async void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_suppress || _apiOffline) return;
        if (cboTinh.SelectedItem is AddressItem p && p.Code != 0)
            await LoadWardsAsync(p.Code);
    }

    // ===================== Nạp dữ liệu =====================
    private void LoadRoomInput(int? keepRoomId)
    {
        Run(() =>
        {
            using var db = NewDb();
            var list = db.PhongTros.AsNoTracking()
                .Where(p => ChoPhepOGhep || p.TinhTrang == "Trống" || p.MaPhong == keepRoomId)
                .OrderBy(p => p.TenPhong).ToList()
                .Select(p => new RoomItem(p.MaPhong, $"{p.TenPhong} - {p.GiaThue:N0} đ")).ToList();
            cboPhong.DisplayMember = "Text";
            cboPhong.ValueMember = "MaPhong";
            cboPhong.DataSource = list;
            cboPhong.SelectedIndex = -1;
        }, "tải danh sách phòng");
    }

    private void LoadFilters()
    {
        int oldRoom = cboLocPhong.SelectedValue is int r ? r : 0;
        string oldTinh = cboLocTinh.SelectedIndex > 0 ? cboLocTinh.Text : null;
        Run(() =>
        {
            using var db = NewDb();
            var rooms = db.PhongTros.AsNoTracking().OrderBy(p => p.TenPhong).ToList()
                .Select(p => new RoomItem(p.MaPhong, p.TenPhong)).ToList();
            rooms.Insert(0, new RoomItem(0, "Tất cả"));
            var tinh = db.KhachThues.AsNoTracking().Where(k => k.TenTinh != null)
                .Select(k => k.TenTinh).Distinct().OrderBy(s => s).ToList();
            tinh.Insert(0, "Tất cả");

            cboLocPhong.DisplayMember = "Text";
            cboLocPhong.ValueMember = "MaPhong";
            cboLocPhong.DataSource = rooms;
            cboLocPhong.SelectedValue = oldRoom;
            cboLocTinh.DataSource = tinh;
            cboLocTinh.SelectedItem = oldTinh ?? "Tất cả";
        }, "tải bộ lọc");
    }

    private void SetupGrid()
    {
        dgv.Columns.Clear();
        dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ tên", Width = 130 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "SĐT", Width = 90 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenPhong", HeaderText = "Phòng", Width = 55 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "GiaThue", HeaderText = "Giá thuê (đ)", Width = 90,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "QueQuan", HeaderText = "Quê quán", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        dgv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DaTamTru", HeaderText = "Tạm trú", Width = 60 });
    }

    private void LoadGrid()
    {
        Run(() =>
        {
            using var db = NewDb();
            var q = db.KhachThues.AsNoTracking().AsQueryable();

            var kw = txtTim.Text.Trim();
            if (kw != "")
                q = q.Where(k => k.HoTen.Contains(kw) || (k.SoDienThoai != null && k.SoDienThoai.Contains(kw)));
            if (cboLocPhong.SelectedValue is int mp && mp > 0)
                q = q.Where(k => k.MaPhong == mp);
            if (cboLocTinh.SelectedIndex > 0)
            {
                var t = cboLocTinh.Text;
                q = q.Where(k => k.TenTinh == t);
            }

            // JOIN KhachThue + PhongTro qua navigation property
            var data = q.OrderBy(k => k.MaKhach).Select(k => new
            {
                k.MaKhach, k.HoTen, k.SoDienThoai,
                k.MaPhongNavigation.TenPhong, k.MaPhongNavigation.GiaThue,
                k.TenPhuongXa, k.TenTinh, k.DaDangKyTamTru
            }).ToList();

            var rows = data.Select(d => new KhachRow
            {
                MaKhach = d.MaKhach, HoTen = d.HoTen, SoDienThoai = d.SoDienThoai,
                TenPhong = d.TenPhong, GiaThue = d.GiaThue,
                QueQuan = string.Join(" – ", new[] { d.TenPhuongXa, d.TenTinh }.Where(s => !string.IsNullOrWhiteSpace(s))),
                DaTamTru = d.DaDangKyTamTru
            }).ToList();

            dgv.DataSource = rows;
            dgv.ClearSelection();
        }, "tải danh sách khách thuê");
    }

    // ===================== Đọc & kiểm tra form =====================
    private static (int? code, string name) ReadAddr(ComboBox c)
        => c.SelectedItem is AddressItem a && a.Code != 0 ? (a.Code, a.Name) : (null, c.Text.Trim());

    private KhachThue Fail(string msg, Control focus)
    {
        MessageBox.Show(msg, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        focus.Focus();
        return null;
    }

    private KhachThue ReadForm()
    {
        if (string.IsNullOrWhiteSpace(txtHoTen.Text)) return Fail("Vui lòng nhập họ tên.", txtHoTen);
        if (dtpNgaySinh.Value.Date > DateTime.Today) return Fail("Ngày sinh không được lớn hơn hôm nay.", dtpNgaySinh);
        if (!Regex.IsMatch(txtSdt.Text.Trim(), @"^0\d{9,10}$")) return Fail("Số điện thoại phải gồm 10–11 chữ số, bắt đầu bằng 0.", txtSdt);
        if (cboPhong.SelectedValue is not int maPhong) return Fail("Vui lòng chọn phòng.", cboPhong);
        if (!cboTinh.Enabled) return Fail("Đang tải danh sách địa chỉ, vui lòng đợi giây lát.", cboTinh);

        var (maTinh, tenTinh) = ReadAddr(cboTinh);
        if (string.IsNullOrWhiteSpace(tenTinh)) return Fail("Vui lòng chọn (hoặc nhập) Tỉnh/Thành.", cboTinh);
        if (!cboPhuongXa.Enabled) return Fail("Đang tải danh sách phường/xã, vui lòng đợi giây lát.", cboPhuongXa);

        var (maPx, tenPx) = ReadAddr(cboPhuongXa);
        if (string.IsNullOrWhiteSpace(tenPx)) return Fail("Vui lòng chọn (hoặc nhập tay) Phường/Xã.", cboPhuongXa);

        return new KhachThue
        {
            HoTen = txtHoTen.Text.Trim(),
            GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
            NgaySinh = DateOnly.FromDateTime(dtpNgaySinh.Value),
            SoDienThoai = txtSdt.Text.Trim(),
            MaTinh = maTinh, TenTinh = tenTinh,
            MaPhuongXa = maPx, TenPhuongXa = tenPx,
            DaDangKyTamTru = chkTamTru.Checked,
            MaPhong = maPhong
        };
    }

    private void ClearForm()
    {
        _selectedId = null;
        txtHoTen.Clear();
        rdoNam.Checked = true;
        dtpNgaySinh.Value = new DateTime(2000, 1, 1);
        txtSdt.Clear();
        chkTamTru.Checked = false;
        cboPhong.SelectedIndex = -1;
        _suppress = true;
        if (_apiOffline) { cboTinh.Text = ""; cboPhuongXa.Text = ""; }
        else if (cboTinh.DataSource is List<AddressItem> { Count: > 1 })
        {
            cboTinh.SelectedIndex = -1;
            BindCombo(cboPhuongXa, new List<AddressItem>());
            cboPhuongXa.Enabled = false;
        }
        _suppress = false;
        dgv.ClearSelection();
    }

    // ===================== Chọn dòng → đổ lên control =====================
    private async void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].DataBoundItem is not KhachRow row) return;

        KhachThue k = null;
        if (!Run(() => { using var db = NewDb(); k = db.KhachThues.AsNoTracking().FirstOrDefault(x => x.MaKhach == row.MaKhach); }, "đọc thông tin khách"))
            return;
        if (k == null) return;

        _selectedId = k.MaKhach;
        txtHoTen.Text = k.HoTen;
        rdoNam.Checked = k.GioiTinh == "Nam";
        rdoNu.Checked = k.GioiTinh != "Nam";
        dtpNgaySinh.Value = k.NgaySinh.ToDateTime(TimeOnly.MinValue);
        txtSdt.Text = k.SoDienThoai;
        chkTamTru.Checked = k.DaDangKyTamTru;
        LoadRoomInput(k.MaPhong);
        cboPhong.SelectedValue = k.MaPhong;
        await ShowAddressAsync(k);
    }

    private async Task ShowAddressAsync(KhachThue k)
    {
        _suppress = true;
        if (cboTinh.DropDownStyle == ComboBoxStyle.DropDownList && cboTinh.Enabled)
        {
            if (k.MaTinh is int mt) cboTinh.SelectedValue = mt;
            if (cboTinh.SelectedIndex < 0 && !string.IsNullOrEmpty(k.TenTinh)) cboTinh.Text = k.TenTinh;
        }
        else cboTinh.Text = k.TenTinh ?? "";
        _suppress = false;

        if (!_apiOffline && cboTinh.SelectedItem is AddressItem p && p.Code != 0)
            await LoadWardsAsync(p.Code);

        if (cboPhuongXa.DropDownStyle == ComboBoxStyle.DropDownList && cboPhuongXa.Enabled)
        {
            if (k.MaPhuongXa is int mw) cboPhuongXa.SelectedValue = mw;
            if (cboPhuongXa.SelectedIndex < 0 && !string.IsNullOrEmpty(k.TenPhuongXa)) cboPhuongXa.Text = k.TenPhuongXa;
        }
        else cboPhuongXa.Text = k.TenPhuongXa ?? "";
    }

    // ===================== CRUD (nhiều bảng) =====================
    /// <summary>Đồng bộ trạng thái phòng theo số khách: còn khách → "Đang thuê", hết → "Trống"</summary>
    private static void SyncRoom(QlNhaTroContext db, int maPhong)
    {
        var phong = db.PhongTros.Find(maPhong);
        if (phong == null) return;
        phong.TinhTrang = db.KhachThues.Any(k => k.MaPhong == maPhong) ? "Đang thuê" : "Trống";
    }

    private void AfterChange()
    {
        ClearForm();
        LoadRoomInput(null);
        LoadFilters();
        LoadGrid();
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        var k = ReadForm();
        if (k == null) return;
        if (Run(() =>
        {
            using var db = NewDb();
            using var tx = db.Database.BeginTransaction();
            db.KhachThues.Add(k);
            db.SaveChanges();
            SyncRoom(db, k.MaPhong);
            db.SaveChanges();
            tx.Commit();
        }, "thêm khách thuê"))
        {
            Log.Information("Thêm khách thuê MaKhach={MaKhach} vào phòng {MaPhong}", k.MaKhach, k.MaPhong);
            AfterChange();
        }
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (_selectedId == null)
        {
            MessageBox.Show("Vui lòng chọn một khách trong danh sách để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var n = ReadForm();
        if (n == null) return;
        int id = _selectedId.Value;
        if (Run(() =>
        {
            using var db = NewDb();
            using var tx = db.Database.BeginTransaction();
            var k = db.KhachThues.Find(id) ?? throw new InvalidOperationException("Khách không còn tồn tại");
            int oldRoom = k.MaPhong;
            k.HoTen = n.HoTen; k.GioiTinh = n.GioiTinh; k.NgaySinh = n.NgaySinh; k.SoDienThoai = n.SoDienThoai;
            k.MaTinh = n.MaTinh; k.TenTinh = n.TenTinh; k.MaPhuongXa = n.MaPhuongXa; k.TenPhuongXa = n.TenPhuongXa;
            k.DaDangKyTamTru = n.DaDangKyTamTru; k.MaPhong = n.MaPhong;
            db.SaveChanges();
            SyncRoom(db, oldRoom);
            if (n.MaPhong != oldRoom) SyncRoom(db, n.MaPhong);
            db.SaveChanges();
            tx.Commit();
        }, "sửa thông tin khách thuê"))
        {
            Log.Information("Sửa khách thuê MaKhach={MaKhach}", id);
            AfterChange();
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_selectedId == null)
        {
            MessageBox.Show("Vui lòng chọn một khách trong danh sách để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        if (MessageBox.Show($"Xóa khách \"{txtHoTen.Text}\"?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        int id = _selectedId.Value;
        if (Run(() =>
        {
            using var db = NewDb();
            using var tx = db.Database.BeginTransaction();
            var k = db.KhachThues.Find(id);
            if (k == null) return;
            int maPhong = k.MaPhong;
            db.KhachThues.Remove(k);
            db.SaveChanges();
            SyncRoom(db, maPhong);   // người cuối cùng bị xóa → phòng về "Trống"
            db.SaveChanges();
            tx.Commit();
        }, "xóa khách thuê"))
        {
            Log.Information("Xóa khách thuê MaKhach={MaKhach}", id);
            AfterChange();
        }
    }

    private void btnLamMoi_Click(object sender, EventArgs e) => AfterChange();

    // ===================== Tìm kiếm =====================
    private void btnTim_Click(object sender, EventArgs e) => LoadGrid();

    private void btnBoLoc_Click(object sender, EventArgs e)
    {
        txtTim.Clear();
        cboLocPhong.SelectedIndex = 0;
        cboLocTinh.SelectedIndex = 0;
        LoadGrid();
    }
}
