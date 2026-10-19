namespace BTCh11_Bai1.Forms;

partial class FormChiTieu
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private GroupBox grpInfo, grpTim;
    private Label lblNgayChi, lblLoaiChi, lblNoiDung, lblSoTien, lblGhiChu, lblLoc, lblTu, lblDen, lblTong;
    private DateTimePicker dtpNgayChi, dtpTu, dtpDen;
    private TextBox txtNoiDung, txtGhiChu, txtTim;
    private ComboBox cboLoai, cboLoc;
    private NumericUpDown nudSoTien;
    private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim;
    private DataGridView dgv;

    private void InitializeComponent()
    {
        SuspendLayout();

        // ----- Thông tin khoản chi -----
        grpInfo = new GroupBox { Text = "Thông tin khoản chi", Location = new Point(12, 10), Size = new Size(520, 176) };
        lblNgayChi = new Label { Text = "Ngày chi", Location = new Point(16, 30), AutoSize = true };
        dtpNgayChi = new DateTimePicker { Location = new Point(90, 26), Size = new Size(150, 23), Format = DateTimePickerFormat.Short };
        lblLoaiChi = new Label { Text = "Loại chi", Location = new Point(270, 30), AutoSize = true };
        cboLoai = new ComboBox { Location = new Point(330, 26), Size = new Size(170, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        lblNoiDung = new Label { Text = "Nội dung", Location = new Point(16, 64), AutoSize = true };
        txtNoiDung = new TextBox { Location = new Point(90, 60), Size = new Size(410, 23), MaxLength = 100 };
        lblSoTien = new Label { Text = "Số tiền", Location = new Point(16, 98), AutoSize = true };
        nudSoTien = new NumericUpDown
        {
            Location = new Point(90, 94), Size = new Size(150, 23),
            Minimum = 0, Maximum = 1000000000, Increment = 1000, DecimalPlaces = 0, ThousandsSeparator = true
        };
        lblGhiChu = new Label { Text = "Ghi chú", Location = new Point(16, 132), AutoSize = true };
        txtGhiChu = new TextBox { Location = new Point(90, 128), Size = new Size(410, 38), Multiline = true, MaxLength = 200, ScrollBars = ScrollBars.Vertical };
        grpInfo.Controls.AddRange(new Control[] {
            lblNgayChi, dtpNgayChi, lblLoaiChi, cboLoai, lblNoiDung, txtNoiDung,
            lblSoTien, nudSoTien, lblGhiChu, txtGhiChu });

        // ----- Tìm kiếm / lọc -----
        grpTim = new GroupBox { Text = "Tìm kiếm", Location = new Point(544, 10), Size = new Size(344, 176) };
        txtTim = new TextBox { Location = new Point(16, 26), Size = new Size(310, 23), PlaceholderText = "Nhập nội dung..." };
        lblLoc = new Label { Text = "Loại chi", Location = new Point(16, 64), AutoSize = true };
        cboLoc = new ComboBox { Location = new Point(90, 60), Size = new Size(236, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        lblTu = new Label { Text = "Từ ngày", Location = new Point(16, 98), AutoSize = true };
        dtpTu = new DateTimePicker { Location = new Point(90, 94), Size = new Size(236, 23), Format = DateTimePickerFormat.Short };
        lblDen = new Label { Text = "Đến ngày", Location = new Point(16, 136), AutoSize = true };
        dtpDen = new DateTimePicker { Location = new Point(90, 132), Size = new Size(150, 23), Format = DateTimePickerFormat.Short };
        btnTim = new Button { Text = "Tìm", Location = new Point(250, 130), Size = new Size(76, 28) };
        grpTim.Controls.AddRange(new Control[] { txtTim, lblLoc, cboLoc, lblTu, dtpTu, lblDen, dtpDen, btnTim });

        // ----- Nút CRUD -----
        btnThem = new Button { Text = "Thêm", Location = new Point(12, 196), Size = new Size(92, 32) };
        btnSua = new Button { Text = "Sửa", Location = new Point(110, 196), Size = new Size(92, 32) };
        btnXoa = new Button { Text = "Xóa", Location = new Point(208, 196), Size = new Size(92, 32) };
        btnLamMoi = new Button { Text = "Làm mới", Location = new Point(306, 196), Size = new Size(92, 32) };

        // ----- Lưới -----
        dgv = new DataGridView
        {
            Location = new Point(12, 240), Size = new Size(876, 270),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            AutoGenerateColumns = false, MultiSelect = false, RowHeadersVisible = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = Color.White
        };

        // ----- Tổng chi -----
        lblTong = new Label
        {
            Text = "Tổng chi: 0 đ", Location = new Point(528, 518), Size = new Size(360, 28),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right, TextAlign = ContentAlignment.MiddleRight,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.Red
        };

        Controls.AddRange(new Control[] { grpInfo, grpTim, btnThem, btnSua, btnXoa, btnLamMoi, dgv, lblTong });

        ClientSize = new Size(900, 556);
        MinimumSize = new Size(916, 595);
        Text = "Sổ chi tiêu cá nhân";
        StartPosition = FormStartPosition.CenterScreen;

        Load += FormChiTieu_Load;
        btnThem.Click += btnThem_Click;
        btnSua.Click += btnSua_Click;
        btnXoa.Click += btnXoa_Click;
        btnLamMoi.Click += btnLamMoi_Click;
        btnTim.Click += btnTim_Click;
        dgv.CellClick += dgv_CellClick;
        txtTim.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTim_Click(s, e); } };

        ResumeLayout(false);
        PerformLayout();
    }
}
