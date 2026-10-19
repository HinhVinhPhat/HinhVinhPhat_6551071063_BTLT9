namespace BTCh11_Bai2;

partial class FormKhachThue
{
    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private GroupBox grpInfo, grpGioiTinh;
    private Label lblHoTen, lblNgaySinh, lblSdt, lblPhong, lblTinh, lblPhuongXa, lblLocPhong, lblLocTinh;
    private TextBox txtHoTen, txtSdt, txtTim;
    private RadioButton rdoNam, rdoNu;
    private DateTimePicker dtpNgaySinh;
    private ComboBox cboPhong, cboTinh, cboPhuongXa, cboLocPhong, cboLocTinh;
    private CheckBox chkTamTru;
    private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim, btnBoLoc, btnGayLoi;
    private DataGridView dgv;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel lblStatus;

    private void InitializeComponent()
    {
        SuspendLayout();

        // ----- Nhóm thông tin khách -----
        grpInfo = new GroupBox { Text = "Thông tin khách thuê", Location = new Point(12, 12), Size = new Size(400, 340) };
        lblHoTen = new Label { Text = "Họ tên", Location = new Point(16, 34), AutoSize = true };
        txtHoTen = new TextBox { Location = new Point(130, 30), Size = new Size(250, 23), MaxLength = 100 };

        grpGioiTinh = new GroupBox { Text = "Giới tính", Location = new Point(130, 58), Size = new Size(250, 44) };
        rdoNam = new RadioButton { Text = "Nam", Location = new Point(14, 17), AutoSize = true, Checked = true };
        rdoNu = new RadioButton { Text = "Nữ", Location = new Point(110, 17), AutoSize = true };
        grpGioiTinh.Controls.AddRange(new Control[] { rdoNam, rdoNu });

        lblNgaySinh = new Label { Text = "Ngày sinh", Location = new Point(16, 120), AutoSize = true };
        dtpNgaySinh = new DateTimePicker { Location = new Point(130, 116), Size = new Size(250, 23), Format = DateTimePickerFormat.Short };
        lblSdt = new Label { Text = "Số điện thoại", Location = new Point(16, 158), AutoSize = true };
        txtSdt = new TextBox { Location = new Point(130, 154), Size = new Size(250, 23), MaxLength = 15 };
        lblPhong = new Label { Text = "Phòng", Location = new Point(16, 196), AutoSize = true };
        cboPhong = new ComboBox { Location = new Point(130, 192), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        lblTinh = new Label { Text = "Tỉnh/Thành", Location = new Point(16, 234), AutoSize = true };
        cboTinh = new ComboBox { Location = new Point(130, 230), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        lblPhuongXa = new Label { Text = "Phường/Xã", Location = new Point(16, 272), AutoSize = true };
        cboPhuongXa = new ComboBox { Location = new Point(130, 268), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        chkTamTru = new CheckBox { Text = "Đã đăng ký tạm trú", Location = new Point(130, 304), AutoSize = true };
        grpInfo.Controls.AddRange(new Control[] {
            lblHoTen, txtHoTen, grpGioiTinh, lblNgaySinh, dtpNgaySinh, lblSdt, txtSdt,
            lblPhong, cboPhong, lblTinh, cboTinh, lblPhuongXa, cboPhuongXa, chkTamTru });

        // ----- Nút CRUD -----
        btnThem = new Button { Text = "Thêm", Location = new Point(12, 366), Size = new Size(92, 32) };
        btnSua = new Button { Text = "Sửa", Location = new Point(110, 366), Size = new Size(92, 32) };
        btnXoa = new Button { Text = "Xóa", Location = new Point(208, 366), Size = new Size(92, 32) };
        btnLamMoi = new Button { Text = "Làm mới", Location = new Point(306, 366), Size = new Size(92, 32) };
        // Nút kiểm tra lỗi toàn cục (luôn hiện)
        btnGayLoi = new Button { Text = "Gây lỗi thử", Location = new Point(12, 412), Size = new Size(120, 28) };

        // ----- Tìm kiếm / lọc -----
        txtTim = new TextBox { Location = new Point(430, 14), Size = new Size(300, 23), PlaceholderText = "Tìm theo họ tên hoặc SĐT" };
        lblLocPhong = new Label { Text = "Phòng:", Location = new Point(745, 18), AutoSize = true };
        cboLocPhong = new ComboBox { Location = new Point(795, 14), Size = new Size(110, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        lblLocTinh = new Label { Text = "Tỉnh:", Location = new Point(430, 52), AutoSize = true };
        cboLocTinh = new ComboBox { Location = new Point(470, 48), Size = new Size(260, 23), DropDownStyle = ComboBoxStyle.DropDownList };
        btnTim = new Button { Text = "Tìm", Location = new Point(745, 46), Size = new Size(75, 28) };
        btnBoLoc = new Button { Text = "Bỏ lọc", Location = new Point(828, 46), Size = new Size(77, 28) };

        // ----- Lưới -----
        dgv = new DataGridView
        {
            Location = new Point(430, 84), Size = new Size(570, 470),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            AutoGenerateColumns = false, MultiSelect = false, RowHeadersVisible = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = Color.White
        };

        // ----- Status bar -----
        statusStrip = new StatusStrip();
        lblStatus = new ToolStripStatusLabel { Text = "Sẵn sàng" };
        statusStrip.Items.Add(lblStatus);

        Controls.AddRange(new Control[] {
            grpInfo, btnThem, btnSua, btnXoa, btnLamMoi, btnGayLoi,
            txtTim, lblLocPhong, cboLocPhong, lblLocTinh, cboLocTinh, btnTim, btnBoLoc, dgv, statusStrip });

        ClientSize = new Size(1010, 600);
        MinimumSize = new Size(1026, 639);
        Text = "Quản lý nhà trọ - Khách thuê";
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview = true;

        // ----- Sự kiện -----
        Load += FormKhachThue_Load;
        KeyDown += FormKhachThue_KeyDown;
        btnThem.Click += btnThem_Click;
        btnSua.Click += btnSua_Click;
        btnXoa.Click += btnXoa_Click;
        btnLamMoi.Click += btnLamMoi_Click;
        btnTim.Click += btnTim_Click;
        btnBoLoc.Click += btnBoLoc_Click;
        btnGayLoi.Click += btnGayLoi_Click;
        cboTinh.SelectedIndexChanged += cboTinh_SelectedIndexChanged;
        dgv.CellClick += dgv_CellClick;
        txtTim.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnTim_Click(s, e); } };

        ResumeLayout(false);
        PerformLayout();
    }
}
