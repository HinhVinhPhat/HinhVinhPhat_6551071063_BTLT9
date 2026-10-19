using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTCh11_Bai5.Model.EF;
using BTCh11_Bai5.Model.Entities;
using Microsoft.EntityFrameworkCore;

using BTCh11_Bai5.Services;


namespace BTCh11_Bai5
{
    public partial class Form1 : Form
    {
        // ===== TÊN CỘT/THUỘC TÍNH CHƯA BIẾT TRONG ENTITY - sửa lại nếu khác với Tour.cs / DatTour.cs =====
        const string COL_NGAY_KH = "NgayKhoiHanh";   // Tour: ngày khởi hành
        const string COL_SO_CHO = "SoChoToiDa";      // Tour: số chỗ tối đa
        const string COL_SO_NGUOI = "SoNguoi";       // DatTour: số người đặt
        // ======================================================================================

        static readonly CultureInfo VN = new CultureInfo("vi-VN");

        // Bảng trong bộ nhớ dùng để hiển thị; dữ liệu thật nằm trong database
        DataTable tbTour = new DataTable("Tour");
        DataTable tbKhach = new DataTable("KhachHang");
        DataTable tbDat = new DataTable("DatTour");
        DataTable tbView = new DataTable("DSDatTour");

        public Form1()
        {
            InitializeComponent();   // giao diện nằm trong Form1.Designer.cs
            InitTables();
            BindData();
            cboNgoaiTe.SelectedIndex = 0;
            cboThang.SelectedIndex = 0;
            LoadFromDb();
        }

        // =====================================================
        //  TIỆN ÍCH ĐỌC/GHI THUỘC TÍNH THEO TÊN
        // =====================================================
        static object GetVal(DbContext db, object entity, string prop)
        {
            return db.Entry(entity).Property(prop).CurrentValue;
        }

        static void SetVal(DbContext db, object entity, string prop, object value)
        {
            var p = db.Entry(entity).Property(prop);
            Type clr = Nullable.GetUnderlyingType(p.Metadata.ClrType) ?? p.Metadata.ClrType;
            if (clr == typeof(DateOnly) && value is DateTime dt) value = DateOnly.FromDateTime(dt);
            else if (clr == typeof(int)) value = Convert.ToInt32(value);
            else if (clr == typeof(DateTime)) value = Convert.ToDateTime(value);
            p.CurrentValue = value;
        }

        static DateTime ToDateTime(object o)
        {
            if (o == null) return DateTime.Today;
            if (o is DateOnly d) return d.ToDateTime(TimeOnly.MinValue);
            return Convert.ToDateTime(o);
        }

        bool Run(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi database:\n" + (ex.InnerException != null ? ex.InnerException.Message : ex.Message));
                return false;
            }
        }

        // =====================================================
        //  CẤU TRÚC BẢNG HIỂN THỊ
        // =====================================================
        void InitTables()
        {
            tbTour.Columns.Add("MaTour", typeof(int));
            tbTour.Columns.Add("TenTour", typeof(string));
            tbTour.Columns.Add("DiemDen", typeof(string));
            tbTour.Columns.Add("NgoaiTe", typeof(string));
            tbTour.Columns.Add("NgayKhoiHanh", typeof(DateTime));
            tbTour.Columns.Add("GiaNgoaiTe", typeof(decimal));
            tbTour.Columns.Add("SoChoToiDa", typeof(int));
            tbTour.PrimaryKey = new[] { tbTour.Columns["MaTour"] };

            tbKhach.Columns.Add("MaKH", typeof(int));
            tbKhach.Columns.Add("HoTen", typeof(string));
            tbKhach.Columns.Add("SDT", typeof(string));
            tbKhach.Columns.Add("Email", typeof(string));
            tbKhach.Columns.Add("HienThi", typeof(string), "HoTen + ' - ' + SDT");
            tbKhach.PrimaryKey = new[] { tbKhach.Columns["MaKH"] };

            tbDat.Columns.Add("MaDat", typeof(int));
            tbDat.Columns.Add("MaKH", typeof(int));
            tbDat.Columns.Add("MaTour", typeof(int));
            tbDat.Columns.Add("SoNguoi", typeof(int));
            tbDat.Columns.Add("TyGia", typeof(decimal));          // tỷ giá tại thời điểm đặt
            tbDat.Columns.Add("TongTienVND", typeof(decimal));
            tbDat.Columns.Add("NgayDat", typeof(DateTime));
            tbDat.PrimaryKey = new[] { tbDat.Columns["MaDat"] };
        }

        void BindData()
        {
            dgvTour.DataSource = tbTour;
            dgvTour.Columns["MaTour"].HeaderText = "Mã";
            dgvTour.Columns["MaTour"].FillWeight = 40;
            dgvTour.Columns["TenTour"].HeaderText = "Tên tour";
            dgvTour.Columns["TenTour"].FillWeight = 180;
            dgvTour.Columns["DiemDen"].HeaderText = "Điểm đến";
            dgvTour.Columns["NgoaiTe"].HeaderText = "Ngoại tệ";
            dgvTour.Columns["NgoaiTe"].FillWeight = 60;
            dgvTour.Columns["NgayKhoiHanh"].HeaderText = "Khởi hành";
            dgvTour.Columns["NgayKhoiHanh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvTour.Columns["GiaNgoaiTe"].HeaderText = "Giá/người";
            dgvTour.Columns["GiaNgoaiTe"].DefaultCellStyle.Format = "N0";
            dgvTour.Columns["GiaNgoaiTe"].DefaultCellStyle.FormatProvider = VN;
            dgvTour.Columns["SoChoToiDa"].HeaderText = "Số chỗ";
            dgvTour.Columns["SoChoToiDa"].FillWeight = 60;

            dgvKhach.DataSource = tbKhach;
            dgvKhach.Columns["MaKH"].HeaderText = "Mã";
            dgvKhach.Columns["MaKH"].FillWeight = 40;
            dgvKhach.Columns["HoTen"].HeaderText = "Họ tên";
            dgvKhach.Columns["SDT"].HeaderText = "SĐT";
            dgvKhach.Columns["Email"].HeaderText = "Email";
            dgvKhach.Columns["HienThi"].Visible = false;

            cboKhach.DisplayMember = "HienThi";
            cboKhach.ValueMember = "MaKH";
            cboKhach.DataSource = tbKhach;

            cboTour.DisplayMember = "TenTour";
            cboTour.ValueMember = "MaTour";
            cboTour.DataSource = tbTour;

            tbView.Columns.Add("Ma", typeof(string));
            tbView.Columns.Add("KhachHang", typeof(string));
            tbView.Columns.Add("Tour", typeof(string));
            tbView.Columns.Add("SoNguoi", typeof(int));
            tbView.Columns.Add("TyGia", typeof(string));
            tbView.Columns.Add("TongTien", typeof(decimal));
            tbView.Columns.Add("NgayDat", typeof(DateTime));
            dgvDat.DataSource = tbView;
            dgvDat.Columns["Ma"].HeaderText = "Mã";
            dgvDat.Columns["Ma"].FillWeight = 45;
            dgvDat.Columns["KhachHang"].HeaderText = "Khách hàng";
            dgvDat.Columns["Tour"].HeaderText = "Tour";
            dgvDat.Columns["Tour"].FillWeight = 160;
            dgvDat.Columns["SoNguoi"].HeaderText = "Số người";
            dgvDat.Columns["SoNguoi"].FillWeight = 55;
            dgvDat.Columns["TyGia"].HeaderText = "Tỷ giá";
            dgvDat.Columns["TyGia"].FillWeight = 70;
            dgvDat.Columns["TongTien"].HeaderText = "Tổng tiền (VND)";
            dgvDat.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            dgvDat.Columns["TongTien"].DefaultCellStyle.FormatProvider = VN;
            dgvDat.Columns["NgayDat"].HeaderText = "Ngày đặt";
            dgvDat.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        // =====================================================
        //  ĐỌC DỮ LIỆU TỪ DATABASE
        // =====================================================
        void LoadFromDb()
        {
            object keepTour = cboTour.SelectedValue;
            object keepKhach = cboKhach.SelectedValue;

            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                var dats = db.DatTours.OrderBy(d => d.MaDat).ToList();
                var khs = db.KhachHangs.OrderBy(k => k.MaKh).ToList();
                var tours = db.Tours.OrderBy(t => t.MaTour).ToList();

                tbDat.BeginLoadData();
                tbDat.Rows.Clear();
                foreach (var d in dats)
                {
                    tbDat.Rows.Add(
                        d.MaDat,
                        Convert.ToInt32((object)d.MaKh),
                        Convert.ToInt32((object)d.MaTour),
                        Convert.ToInt32(GetVal(db, d, COL_SO_NGUOI)),
                        Convert.ToDecimal((object)d.TyGia),
                        Convert.ToDecimal((object)d.TongTienVnd),
                        ToDateTime((object)d.NgayDat));
                }
                tbDat.EndLoadData();

                tbKhach.BeginLoadData();
                tbKhach.Rows.Clear();
                foreach (var k in khs)
                    tbKhach.Rows.Add(k.MaKh, k.HoTen, k.SoDienThoai ?? "", k.Email ?? "");
                tbKhach.EndLoadData();

                tbTour.BeginLoadData();
                tbTour.Rows.Clear();
                foreach (var t in tours)
                {
                    tbTour.Rows.Add(
                        t.MaTour,
                        t.TenTour,
                        t.DiemDen,
                        (t.NgoaiTe ?? "VND").Trim(),
                        ToDateTime(GetVal(db, t, COL_NGAY_KH)),
                        Convert.ToDecimal((object)t.GiaNgoaiTe),
                        Convert.ToInt32(GetVal(db, t, COL_SO_CHO)));
                }
                tbTour.EndLoadData();
            });

            if (!ok) return;

            if (keepTour != null) cboTour.SelectedValue = keepTour;
            if (keepKhach != null) cboKhach.SelectedValue = keepKhach;

            RefreshDat();
            _ = UpdateBookingAsync();
        }

        static string Esc(string s)
        {
            return s.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]");
        }

        DataRow CurrentRow(DataGridView g)
        {
            var drv = g.CurrentRow == null ? null : g.CurrentRow.DataBoundItem as DataRowView;
            return drv == null ? null : drv.Row;
        }

        // =====================================================
        //  TAB TOUR
        // =====================================================
        private void dgvTour_SelectionChanged(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvTour);
            if (r == null) return;
            txtTenTour.Text = (string)r["TenTour"];
            txtDiemDen.Text = (string)r["DiemDen"];
            cboNgoaiTe.SelectedItem = (string)r["NgoaiTe"];
            dtpKhoiHanh.Value = (DateTime)r["NgayKhoiHanh"];
            nudGia.Value = Math.Min(nudGia.Maximum, (decimal)r["GiaNgoaiTe"]);
            nudSoCho.Value = Math.Min(nudSoCho.Maximum, (int)r["SoChoToiDa"]);
        }

        bool TourHopLe()
        {
            if (txtTenTour.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập tên tour."); return false; }
            if (txtDiemDen.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập điểm đến."); return false; }
            if (nudGia.Value <= 0) { MessageBox.Show("Giá/người phải lớn hơn 0."); return false; }
            if (nudSoCho.Value <= 0) { MessageBox.Show("Số chỗ phải lớn hơn 0."); return false; }
            return true;
        }

        void FillTour(AppDbContext db, Tour t)
        {
            t.TenTour = txtTenTour.Text.Trim();
            t.DiemDen = txtDiemDen.Text.Trim();
            t.NgoaiTe = cboNgoaiTe.SelectedItem.ToString();
            t.GiaNgoaiTe = nudGia.Value;
            SetVal(db, t, COL_NGAY_KH, dtpKhoiHanh.Value.Date);
            SetVal(db, t, COL_SO_CHO, (int)nudSoCho.Value);
        }

        private void btnTourThem_Click(object sender, EventArgs e)
        {
            if (!TourHopLe()) return;
            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                var t = new Tour();
                db.Tours.Add(t);
                FillTour(db, t);
                db.SaveChanges();
            });
            if (ok) LoadFromDb();
        }

        private void btnTourSua_Click(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvTour);
            if (r == null) { MessageBox.Show("Hãy chọn một tour."); return; }
            if (!TourHopLe()) return;

            int id = (int)r["MaTour"];
            int daDat = (int)r["SoChoToiDa"] - SeatsLeft(r);
            if (nudSoCho.Value < daDat)
            {
                MessageBox.Show("Số chỗ không được nhỏ hơn số người đã đặt (" + daDat + ").");
                return;
            }

            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                var t = db.Tours.Find(id);
                if (t == null) throw new Exception("Không tìm thấy tour.");
                FillTour(db, t);
                db.SaveChanges();
            });
            if (ok) LoadFromDb();
        }

        private void btnTourXoa_Click(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvTour);
            if (r == null) { MessageBox.Show("Hãy chọn một tour."); return; }
            int id = (int)r["MaTour"];
            if (tbDat.Select("MaTour = " + id).Length > 0)
            {
                MessageBox.Show("Tour này đã có đơn đặt, không thể xóa.");
                return;
            }
            if (MessageBox.Show("Xóa tour đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                var t = db.Tours.Find(id);
                if (t != null)
                {
                    db.Tours.Remove(t);
                    db.SaveChanges();
                }
            });
            if (ok) LoadFromDb();
        }

        private void btnTourLamMoi_Click(object sender, EventArgs e)
        {
            txtTenTour.Clear();
            txtDiemDen.Clear();
            cboNgoaiTe.SelectedIndex = 0;
            dtpKhoiHanh.Value = DateTime.Today;
            nudGia.Value = 0;
            nudSoCho.Value = 20;
            txtTimTour.Clear();
        }

        private void txtTimTour_TextChanged(object sender, EventArgs e)
        {
            string k = Esc(txtTimTour.Text.Trim());
            tbTour.DefaultView.RowFilter = k == "" ? "" : "TenTour LIKE '%" + k + "%' OR DiemDen LIKE '%" + k + "%'";
        }

        // =====================================================
        //  TAB KHÁCH HÀNG
        // =====================================================
        private void dgvKhach_SelectionChanged(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvKhach);
            if (r == null) return;
            txtTenKH.Text = (string)r["HoTen"];
            txtSDT.Text = (string)r["SDT"];
            txtEmail.Text = r["Email"] as string ?? "";
        }

        bool KhachHopLe()
        {
            if (txtTenKH.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập họ tên."); return false; }
            if (!Regex.IsMatch(txtSDT.Text.Trim(), @"^0\d{9}$")) { MessageBox.Show("SĐT phải gồm 10 chữ số, bắt đầu bằng 0."); return false; }
            return true;
        }

        private void btnKhachThem_Click(object sender, EventArgs e)
        {
            if (!KhachHopLe()) return;
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();

            bool trung = false;
            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                if (db.KhachHangs.Any(k => k.SoDienThoai == sdt))
                {
                    trung = true;
                    return;
                }
                db.KhachHangs.Add(new KhachHang
                {
                    HoTen = txtTenKH.Text.Trim(),
                    SoDienThoai = sdt,
                    Email = email == "" ? null : email
                });
                db.SaveChanges();
            });

            if (trung) { MessageBox.Show("SĐT này đã tồn tại."); return; }
            if (ok) LoadFromDb();
        }

        private void btnKhachSua_Click(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvKhach);
            if (r == null) { MessageBox.Show("Hãy chọn một khách hàng."); return; }
            if (!KhachHopLe()) return;

            int id = (int)r["MaKH"];
            string sdt = txtSDT.Text.Trim();
            string email = txtEmail.Text.Trim();

            bool trung = false;
            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                if (db.KhachHangs.Any(k => k.SoDienThoai == sdt && k.MaKh != id))
                {
                    trung = true;
                    return;
                }
                var kh = db.KhachHangs.Find(id);
                if (kh == null) throw new Exception("Không tìm thấy khách hàng.");
                kh.HoTen = txtTenKH.Text.Trim();
                kh.SoDienThoai = sdt;
                kh.Email = email == "" ? null : email;
                db.SaveChanges();
            });

            if (trung) { MessageBox.Show("SĐT này đã được khách hàng khác sử dụng."); return; }
            if (ok) LoadFromDb();
        }

        private void btnKhachXoa_Click(object sender, EventArgs e)
        {
            var r = CurrentRow(dgvKhach);
            if (r == null) { MessageBox.Show("Hãy chọn một khách hàng."); return; }
            int id = (int)r["MaKH"];
            if (tbDat.Select("MaKH = " + id).Length > 0)
            {
                MessageBox.Show("Khách hàng này đã có đơn đặt, không thể xóa.");
                return;
            }
            if (MessageBox.Show("Xóa khách hàng đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            bool ok = Run(() =>
            {
                using var db = new AppDbContext();
                var kh = db.KhachHangs.Find(id);
                if (kh != null)
                {
                    db.KhachHangs.Remove(kh);
                    db.SaveChanges();
                }
            });
            if (ok) LoadFromDb();
        }

        private void btnKhachLamMoi_Click(object sender, EventArgs e)
        {
            txtTenKH.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtTimKH.Clear();
        }

        private void txtTimKH_TextChanged(object sender, EventArgs e)
        {
            string k = Esc(txtTimKH.Text.Trim());
            tbKhach.DefaultView.RowFilter = k == "" ? "" : "HoTen LIKE '%" + k + "%' OR SDT LIKE '%" + k + "%'";
        }

        // =====================================================
        //  TAB ĐẶT TOUR
        // =====================================================
        int SeatsLeft(DataRow tour)
        {
            object s = tbDat.Compute("SUM(SoNguoi)", "MaTour = " + tour["MaTour"]);
            int used = s == DBNull.Value ? 0 : Convert.ToInt32(s);
            return (int)tour["SoChoToiDa"] - used;
        }

        DataRow SelectedTour()
        {
            var drv = cboTour.SelectedItem as DataRowView;
            return drv == null ? null : drv.Row;
        }


        private readonly TyGiaService tyGiaService = new();
        private int bookingRequestVersion = 0;

        private async Task<decimal> GetRateAsync(string code)
        {
            return await tyGiaService.LayTyGiaAsync(code);
        }


        async Task UpdateBookingAsync()
        {
            int requestVersion = ++bookingRequestVersion;
            var tour = SelectedTour();
            btnDat.Enabled = false;

            if (tour == null)
            {
                lblGia.Text = "";
                lblTyGia.Text = "";
                lblNguon.Text = "";
                lblTong.Text = "0 đ";
                lblConCho.Text = "";
                return;
            }

            int maTour = (int)tour["MaTour"];
            string code = ((string)tour["NgoaiTe"]).Trim().ToUpperInvariant();
            decimal gia = (decimal)tour["GiaNgoaiTe"];
            int conCho = SeatsLeft(tour);

            nudSoNguoi.Minimum = 1;
            nudSoNguoi.Maximum = Math.Max(1, conCho);
            lblConCho.Text = conCho > 0 ? "(còn " + conCho + " chỗ)" : "(HẾT CHỖ)";
            lblGia.Text = gia.ToString("N2", VN) + " " + code;
            lblTyGia.Text = "Đang tải tỷ giá...";
            lblNguon.Text = "";
            lblTong.Text = "0 đ";

            try
            {
                decimal rate = Math.Round(await GetRateAsync(code), 4);
                if (requestVersion != bookingRequestVersion) return;
                var now = SelectedTour();
                if (now == null || (int)now["MaTour"] != maTour) return;

                lblTyGia.Text = "1 " + code + " = " + rate.ToString("N4", VN) + " VND";
                lblNguon.Text = "API tỷ giá (dự phòng + cache 10 phút)";
                lblTong.Text = Math.Round(gia * nudSoNguoi.Value * rate, 0,
                    MidpointRounding.AwayFromZero).ToString("N0", VN) + " đ";
                btnDat.Enabled = conCho > 0;
            }
            catch (Exception ex)
            {
                if (requestVersion != bookingRequestVersion) return;
                lblTyGia.Text = "Không lấy được tỷ giá";
                lblNguon.Text = "";
                lblTong.Text = "0 đ";
                btnDat.Enabled = false;
                MessageBox.Show(ex.Message, "Lỗi tỷ giá",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void cboTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            await UpdateBookingAsync();
        }

        private void nudSoNguoi_ValueChanged(object sender, EventArgs e)
        {
            // Không gọi API lại khi chỉ thay đổi số người; tỷ giá đã có cache.
            _ = UpdateBookingAsync();
        }

        private async void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabDat)
            {
                RefreshDat();
                await UpdateBookingAsync();
            }
        }

        private async void btnDat_Click(object sender, EventArgs e)
        {
            var tour = SelectedTour();
            if (tour == null || cboKhach.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng và tour.");
                return;
            }

            int maKh = Convert.ToInt32(cboKhach.SelectedValue);
            int maTour = (int)tour["MaTour"];
            int n = (int)nudSoNguoi.Value;
            btnDat.Enabled = false;

            try
            {
                decimal rate = Math.Round(
                    await GetRateAsync(((string)tour["NgoaiTe"]).Trim()), 4);

                // Đọc lại giá và sức chứa trong giao dịch Serializable.
                // Tránh hai phiên đặt tour đồng thời vượt số chỗ.
                using var db = new AppDbContext();
                using var transaction = db.Database.BeginTransaction(
                    System.Data.IsolationLevel.Serializable);

                var tourDb = db.Tours.SingleOrDefault(t => t.MaTour == maTour);
                if (tourDb == null) throw new Exception("Tour không còn tồn tại.");

                int soCho = Convert.ToInt32(GetVal(db, tourDb, COL_SO_CHO));
                int daDat = db.DatTours.Where(d => d.MaTour == maTour)
                    .Sum(d => (int?)EF.Property<int>(d, COL_SO_NGUOI)) ?? 0;

                if (n > soCho - daDat)
                    throw new Exception("Tour chỉ còn " + (soCho - daDat) + " chỗ.");

                decimal tong = Math.Round(tourDb.GiaNgoaiTe * n * rate, 0,
                    MidpointRounding.AwayFromZero);

                var dat = new DatTour
                {
                    MaKh = maKh,
                    MaTour = maTour,
                    TyGia = rate,
                    TongTienVnd = tong,
                    NgayDat = DateTime.Now
                };
                db.DatTours.Add(dat);
                SetVal(db, dat, COL_SO_NGUOI, n);
                db.SaveChanges();
                transaction.Commit();

                LoadFromDb();
                MessageBox.Show("Đặt tour thành công!\\nTổng tiền: " +
                    tong.ToString("N0", VN) + " đ", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message,
                    "Không thể đặt tour", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                _ = UpdateBookingAsync();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            RefreshDat();
        }

        private void txtTimDat_TextChanged(object sender, EventArgs e)
        {
            RefreshDat();
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDat();
        }

        // Danh sách đặt tour (JOIN Khách hàng + Tour + DatTour)
        void RefreshDat()
        {
            if (tbView.Columns.Count == 0) return;

            string kw = txtTimDat.Text.Trim();
            int thang = cboThang.SelectedIndex;   // 0 = tất cả


            decimal tongDoanhThu = 0;
            tbView.Rows.Clear();
            foreach (DataRow d in tbDat.Rows)
            {
                DataRow kh = tbKhach.Rows.Find(d["MaKH"]);
                DataRow tour = tbTour.Rows.Find(d["MaTour"]);
                if (kh == null || tour == null) continue;

                string tenKH = (string)kh["HoTen"];
                string tenTour = (string)tour["TenTour"];

                if (kw != "" &&
                    tenKH.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) < 0 &&
                    tenTour.IndexOf(kw, StringComparison.CurrentCultureIgnoreCase) < 0) continue;

                if (thang > 0 && ((DateTime)tour["NgayKhoiHanh"]).Month != thang) continue;

                decimal rate = (decimal)d["TyGia"];
                string code = (string)tour["NgoaiTe"];
                string tyGiaText = rate == 1m ? "1" : rate.ToString("0.##", VN) + " (" + code + ")";
                decimal tong = (decimal)d["TongTienVND"];

                tbView.Rows.Add("DT" + ((int)d["MaDat"]).ToString("000"), tenKH, tenTour,
                    (int)d["SoNguoi"], tyGiaText, tong, (DateTime)d["NgayDat"]);
                tongDoanhThu += tong;
            }

            lblDoanhThu.Text = "Tổng doanh thu: " + tongDoanhThu.ToString("N0", VN) + " đ";
        }
    }
}