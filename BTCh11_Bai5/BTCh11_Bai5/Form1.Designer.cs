using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTCh11_Bai5
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabTour = new System.Windows.Forms.TabPage();
            this.tabKhach = new System.Windows.Forms.TabPage();
            this.tabDat = new System.Windows.Forms.TabPage();

            // ---- tab Tour ----
            this.grpTour = new System.Windows.Forms.GroupBox();
            this.lblTenTour = new System.Windows.Forms.Label();
            this.txtTenTour = new System.Windows.Forms.TextBox();
            this.lblDiemDen = new System.Windows.Forms.Label();
            this.txtDiemDen = new System.Windows.Forms.TextBox();
            this.lblNgoaiTe = new System.Windows.Forms.Label();
            this.cboNgoaiTe = new System.Windows.Forms.ComboBox();
            this.lblKhoiHanh = new System.Windows.Forms.Label();
            this.dtpKhoiHanh = new System.Windows.Forms.DateTimePicker();
            this.lblGiaTour = new System.Windows.Forms.Label();
            this.nudGia = new System.Windows.Forms.NumericUpDown();
            this.lblSoCho = new System.Windows.Forms.Label();
            this.nudSoCho = new System.Windows.Forms.NumericUpDown();
            this.btnTourThem = new System.Windows.Forms.Button();
            this.btnTourSua = new System.Windows.Forms.Button();
            this.btnTourXoa = new System.Windows.Forms.Button();
            this.btnTourLamMoi = new System.Windows.Forms.Button();
            this.lblTimTour = new System.Windows.Forms.Label();
            this.txtTimTour = new System.Windows.Forms.TextBox();
            this.dgvTour = new System.Windows.Forms.DataGridView();

            // ---- tab Khách hàng ----
            this.grpKhach = new System.Windows.Forms.GroupBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnKhachThem = new System.Windows.Forms.Button();
            this.btnKhachSua = new System.Windows.Forms.Button();
            this.btnKhachXoa = new System.Windows.Forms.Button();
            this.btnKhachLamMoi = new System.Windows.Forms.Button();
            this.lblTimKH = new System.Windows.Forms.Label();
            this.txtTimKH = new System.Windows.Forms.TextBox();
            this.dgvKhach = new System.Windows.Forms.DataGridView();

            // ---- tab Đặt tour ----
            this.grpDat = new System.Windows.Forms.GroupBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhach = new System.Windows.Forms.ComboBox();
            this.lblTour = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.lblSoNguoi = new System.Windows.Forms.Label();
            this.nudSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblConCho = new System.Windows.Forms.Label();
            this.lblGiaNguoi = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            this.lblTyGia = new System.Windows.Forms.Label();
            this.lblNguon = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();
            this.btnDat = new System.Windows.Forms.Button();
            this.lblTimDat = new System.Windows.Forms.Label();
            this.txtTimDat = new System.Windows.Forms.TextBox();
            this.lblThang = new System.Windows.Forms.Label();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvDat = new System.Windows.Forms.DataGridView();
            this.lblDoanhThu = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.nudGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDat)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabTour.SuspendLayout();
            this.grpTour.SuspendLayout();
            this.tabKhach.SuspendLayout();
            this.grpKhach.SuspendLayout();
            this.tabDat.SuspendLayout();
            this.grpDat.SuspendLayout();
            this.SuspendLayout();

            //
            // tabs
            //
            this.tabs.Controls.Add(this.tabTour);
            this.tabs.Controls.Add(this.tabKhach);
            this.tabs.Controls.Add(this.tabDat);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(880, 620);
            this.tabs.TabIndex = 0;
            this.tabs.SelectedIndexChanged += new System.EventHandler(this.tabs_SelectedIndexChanged);

            // =========================================================
            // tabTour
            // =========================================================
            this.tabTour.Controls.Add(this.grpTour);
            this.tabTour.Controls.Add(this.lblTimTour);
            this.tabTour.Controls.Add(this.txtTimTour);
            this.tabTour.Controls.Add(this.dgvTour);
            this.tabTour.Location = new System.Drawing.Point(4, 24);
            this.tabTour.Name = "tabTour";
            this.tabTour.Padding = new System.Windows.Forms.Padding(3);
            this.tabTour.Size = new System.Drawing.Size(872, 592);
            this.tabTour.TabIndex = 0;
            this.tabTour.Text = "Tour";
            this.tabTour.UseVisualStyleBackColor = true;
            //
            // grpTour
            //
            this.grpTour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTour.Controls.Add(this.lblTenTour);
            this.grpTour.Controls.Add(this.txtTenTour);
            this.grpTour.Controls.Add(this.lblDiemDen);
            this.grpTour.Controls.Add(this.txtDiemDen);
            this.grpTour.Controls.Add(this.lblNgoaiTe);
            this.grpTour.Controls.Add(this.cboNgoaiTe);
            this.grpTour.Controls.Add(this.lblKhoiHanh);
            this.grpTour.Controls.Add(this.dtpKhoiHanh);
            this.grpTour.Controls.Add(this.lblGiaTour);
            this.grpTour.Controls.Add(this.nudGia);
            this.grpTour.Controls.Add(this.lblSoCho);
            this.grpTour.Controls.Add(this.nudSoCho);
            this.grpTour.Controls.Add(this.btnTourThem);
            this.grpTour.Controls.Add(this.btnTourSua);
            this.grpTour.Controls.Add(this.btnTourXoa);
            this.grpTour.Controls.Add(this.btnTourLamMoi);
            this.grpTour.Location = new System.Drawing.Point(8, 8);
            this.grpTour.Name = "grpTour";
            this.grpTour.Size = new System.Drawing.Size(850, 130);
            this.grpTour.TabIndex = 0;
            this.grpTour.TabStop = false;
            this.grpTour.Text = "Thông tin tour";
            //
            // lblTenTour
            //
            this.lblTenTour.AutoSize = true;
            this.lblTenTour.Location = new System.Drawing.Point(12, 30);
            this.lblTenTour.Name = "lblTenTour";
            this.lblTenTour.Text = "Tên tour:";
            //
            // txtTenTour
            //
            this.txtTenTour.Location = new System.Drawing.Point(90, 26);
            this.txtTenTour.Name = "txtTenTour";
            this.txtTenTour.Size = new System.Drawing.Size(230, 23);
            this.txtTenTour.TabIndex = 1;
            //
            // lblDiemDen
            //
            this.lblDiemDen.AutoSize = true;
            this.lblDiemDen.Location = new System.Drawing.Point(345, 30);
            this.lblDiemDen.Name = "lblDiemDen";
            this.lblDiemDen.Text = "Điểm đến:";
            //
            // txtDiemDen
            //
            this.txtDiemDen.Location = new System.Drawing.Point(410, 26);
            this.txtDiemDen.Name = "txtDiemDen";
            this.txtDiemDen.Size = new System.Drawing.Size(170, 23);
            this.txtDiemDen.TabIndex = 2;
            //
            // lblNgoaiTe
            //
            this.lblNgoaiTe.AutoSize = true;
            this.lblNgoaiTe.Location = new System.Drawing.Point(600, 30);
            this.lblNgoaiTe.Name = "lblNgoaiTe";
            this.lblNgoaiTe.Text = "Ngoại tệ:";
            //
            // cboNgoaiTe
            //
            this.cboNgoaiTe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgoaiTe.Items.AddRange(new object[] { "VND", "THB", "KRW", "USD", "JPY" });
            this.cboNgoaiTe.Location = new System.Drawing.Point(660, 26);
            this.cboNgoaiTe.Name = "cboNgoaiTe";
            this.cboNgoaiTe.Size = new System.Drawing.Size(90, 23);
            this.cboNgoaiTe.TabIndex = 3;
            //
            // lblKhoiHanh
            //
            this.lblKhoiHanh.AutoSize = true;
            this.lblKhoiHanh.Location = new System.Drawing.Point(12, 66);
            this.lblKhoiHanh.Name = "lblKhoiHanh";
            this.lblKhoiHanh.Text = "Khởi hành:";
            //
            // dtpKhoiHanh
            //
            this.dtpKhoiHanh.CustomFormat = "dd/MM/yyyy";
            this.dtpKhoiHanh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpKhoiHanh.Location = new System.Drawing.Point(90, 62);
            this.dtpKhoiHanh.Name = "dtpKhoiHanh";
            this.dtpKhoiHanh.Size = new System.Drawing.Size(230, 23);
            this.dtpKhoiHanh.TabIndex = 4;
            //
            // lblGiaTour
            //
            this.lblGiaTour.AutoSize = true;
            this.lblGiaTour.Location = new System.Drawing.Point(345, 66);
            this.lblGiaTour.Name = "lblGiaTour";
            this.lblGiaTour.Text = "Giá/người:";
            //
            // nudGia
            //
            this.nudGia.DecimalPlaces = 2;
            this.nudGia.Location = new System.Drawing.Point(410, 62);
            this.nudGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudGia.Name = "nudGia";
            this.nudGia.Size = new System.Drawing.Size(170, 23);
            this.nudGia.TabIndex = 5;
            this.nudGia.ThousandsSeparator = true;
            //
            // lblSoCho
            //
            this.lblSoCho.AutoSize = true;
            this.lblSoCho.Location = new System.Drawing.Point(600, 66);
            this.lblSoCho.Name = "lblSoCho";
            this.lblSoCho.Text = "Số chỗ:";
            //
            // nudSoCho
            //
            this.nudSoCho.Location = new System.Drawing.Point(660, 62);
            this.nudSoCho.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudSoCho.Name = "nudSoCho";
            this.nudSoCho.Size = new System.Drawing.Size(90, 23);
            this.nudSoCho.TabIndex = 6;
            this.nudSoCho.ThousandsSeparator = true;
            this.nudSoCho.Value = new decimal(new int[] { 20, 0, 0, 0 });
            //
            // btnTourThem
            //
            this.btnTourThem.Location = new System.Drawing.Point(12, 94);
            this.btnTourThem.Name = "btnTourThem";
            this.btnTourThem.Size = new System.Drawing.Size(80, 28);
            this.btnTourThem.TabIndex = 7;
            this.btnTourThem.Text = "Thêm";
            this.btnTourThem.UseVisualStyleBackColor = true;
            this.btnTourThem.Click += new System.EventHandler(this.btnTourThem_Click);
            //
            // btnTourSua
            //
            this.btnTourSua.Location = new System.Drawing.Point(100, 94);
            this.btnTourSua.Name = "btnTourSua";
            this.btnTourSua.Size = new System.Drawing.Size(80, 28);
            this.btnTourSua.TabIndex = 8;
            this.btnTourSua.Text = "Sửa";
            this.btnTourSua.UseVisualStyleBackColor = true;
            this.btnTourSua.Click += new System.EventHandler(this.btnTourSua_Click);
            //
            // btnTourXoa
            //
            this.btnTourXoa.Location = new System.Drawing.Point(188, 94);
            this.btnTourXoa.Name = "btnTourXoa";
            this.btnTourXoa.Size = new System.Drawing.Size(80, 28);
            this.btnTourXoa.TabIndex = 9;
            this.btnTourXoa.Text = "Xóa";
            this.btnTourXoa.UseVisualStyleBackColor = true;
            this.btnTourXoa.Click += new System.EventHandler(this.btnTourXoa_Click);
            //
            // btnTourLamMoi
            //
            this.btnTourLamMoi.Location = new System.Drawing.Point(276, 94);
            this.btnTourLamMoi.Name = "btnTourLamMoi";
            this.btnTourLamMoi.Size = new System.Drawing.Size(80, 28);
            this.btnTourLamMoi.TabIndex = 10;
            this.btnTourLamMoi.Text = "Làm mới";
            this.btnTourLamMoi.UseVisualStyleBackColor = true;
            this.btnTourLamMoi.Click += new System.EventHandler(this.btnTourLamMoi_Click);
            //
            // lblTimTour
            //
            this.lblTimTour.AutoSize = true;
            this.lblTimTour.Location = new System.Drawing.Point(12, 152);
            this.lblTimTour.Name = "lblTimTour";
            this.lblTimTour.Text = "Tìm theo tên / điểm đến:";
            //
            // txtTimTour
            //
            this.txtTimTour.Location = new System.Drawing.Point(160, 148);
            this.txtTimTour.Name = "txtTimTour";
            this.txtTimTour.Size = new System.Drawing.Size(300, 23);
            this.txtTimTour.TabIndex = 1;
            this.txtTimTour.TextChanged += new System.EventHandler(this.txtTimTour_TextChanged);
            //
            // dgvTour
            //
            this.dgvTour.AllowUserToAddRows = false;
            this.dgvTour.AllowUserToDeleteRows = false;
            this.dgvTour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTour.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTour.BackgroundColor = System.Drawing.Color.White;
            this.dgvTour.Location = new System.Drawing.Point(8, 180);
            this.dgvTour.MultiSelect = false;
            this.dgvTour.Name = "dgvTour";
            this.dgvTour.ReadOnly = true;
            this.dgvTour.RowHeadersWidth = 24;
            this.dgvTour.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTour.Size = new System.Drawing.Size(850, 400);
            this.dgvTour.TabIndex = 2;
            this.dgvTour.SelectionChanged += new System.EventHandler(this.dgvTour_SelectionChanged);

            // =========================================================
            // tabKhach
            // =========================================================
            this.tabKhach.Controls.Add(this.grpKhach);
            this.tabKhach.Controls.Add(this.lblTimKH);
            this.tabKhach.Controls.Add(this.txtTimKH);
            this.tabKhach.Controls.Add(this.dgvKhach);
            this.tabKhach.Location = new System.Drawing.Point(4, 24);
            this.tabKhach.Name = "tabKhach";
            this.tabKhach.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhach.Size = new System.Drawing.Size(872, 592);
            this.tabKhach.TabIndex = 1;
            this.tabKhach.Text = "Khách hàng";
            this.tabKhach.UseVisualStyleBackColor = true;
            //
            // grpKhach
            //
            this.grpKhach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpKhach.Controls.Add(this.lblTenKH);
            this.grpKhach.Controls.Add(this.txtTenKH);
            this.grpKhach.Controls.Add(this.lblSDT);
            this.grpKhach.Controls.Add(this.txtSDT);
            this.grpKhach.Controls.Add(this.lblEmail);
            this.grpKhach.Controls.Add(this.txtEmail);
            this.grpKhach.Controls.Add(this.btnKhachThem);
            this.grpKhach.Controls.Add(this.btnKhachSua);
            this.grpKhach.Controls.Add(this.btnKhachXoa);
            this.grpKhach.Controls.Add(this.btnKhachLamMoi);
            this.grpKhach.Location = new System.Drawing.Point(8, 8);
            this.grpKhach.Name = "grpKhach";
            this.grpKhach.Size = new System.Drawing.Size(850, 100);
            this.grpKhach.TabIndex = 0;
            this.grpKhach.TabStop = false;
            this.grpKhach.Text = "Thông tin khách hàng";
            //
            // lblTenKH
            //
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(12, 30);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Text = "Họ tên:";
            //
            // txtTenKH
            //
            this.txtTenKH.Location = new System.Drawing.Point(70, 26);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(230, 23);
            this.txtTenKH.TabIndex = 1;
            //
            // lblSDT
            //
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(320, 30);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Text = "SĐT:";
            //
            // txtSDT
            //
            this.txtSDT.Location = new System.Drawing.Point(360, 26);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(150, 23);
            this.txtSDT.TabIndex = 2;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(530, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "Email:";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(580, 26);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 23);
            this.txtEmail.TabIndex = 3;
            //
            // btnKhachThem
            //
            this.btnKhachThem.Location = new System.Drawing.Point(12, 62);
            this.btnKhachThem.Name = "btnKhachThem";
            this.btnKhachThem.Size = new System.Drawing.Size(80, 28);
            this.btnKhachThem.TabIndex = 4;
            this.btnKhachThem.Text = "Thêm";
            this.btnKhachThem.UseVisualStyleBackColor = true;
            this.btnKhachThem.Click += new System.EventHandler(this.btnKhachThem_Click);
            //
            // btnKhachSua
            //
            this.btnKhachSua.Location = new System.Drawing.Point(100, 62);
            this.btnKhachSua.Name = "btnKhachSua";
            this.btnKhachSua.Size = new System.Drawing.Size(80, 28);
            this.btnKhachSua.TabIndex = 5;
            this.btnKhachSua.Text = "Sửa";
            this.btnKhachSua.UseVisualStyleBackColor = true;
            this.btnKhachSua.Click += new System.EventHandler(this.btnKhachSua_Click);
            //
            // btnKhachXoa
            //
            this.btnKhachXoa.Location = new System.Drawing.Point(188, 62);
            this.btnKhachXoa.Name = "btnKhachXoa";
            this.btnKhachXoa.Size = new System.Drawing.Size(80, 28);
            this.btnKhachXoa.TabIndex = 6;
            this.btnKhachXoa.Text = "Xóa";
            this.btnKhachXoa.UseVisualStyleBackColor = true;
            this.btnKhachXoa.Click += new System.EventHandler(this.btnKhachXoa_Click);
            //
            // btnKhachLamMoi
            //
            this.btnKhachLamMoi.Location = new System.Drawing.Point(276, 62);
            this.btnKhachLamMoi.Name = "btnKhachLamMoi";
            this.btnKhachLamMoi.Size = new System.Drawing.Size(80, 28);
            this.btnKhachLamMoi.TabIndex = 7;
            this.btnKhachLamMoi.Text = "Làm mới";
            this.btnKhachLamMoi.UseVisualStyleBackColor = true;
            this.btnKhachLamMoi.Click += new System.EventHandler(this.btnKhachLamMoi_Click);
            //
            // lblTimKH
            //
            this.lblTimKH.AutoSize = true;
            this.lblTimKH.Location = new System.Drawing.Point(12, 122);
            this.lblTimKH.Name = "lblTimKH";
            this.lblTimKH.Text = "Tìm theo tên / SĐT:";
            //
            // txtTimKH
            //
            this.txtTimKH.Location = new System.Drawing.Point(135, 118);
            this.txtTimKH.Name = "txtTimKH";
            this.txtTimKH.Size = new System.Drawing.Size(300, 23);
            this.txtTimKH.TabIndex = 1;
            this.txtTimKH.TextChanged += new System.EventHandler(this.txtTimKH_TextChanged);
            //
            // dgvKhach
            //
            this.dgvKhach.AllowUserToAddRows = false;
            this.dgvKhach.AllowUserToDeleteRows = false;
            this.dgvKhach.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKhach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKhach.BackgroundColor = System.Drawing.Color.White;
            this.dgvKhach.Location = new System.Drawing.Point(8, 150);
            this.dgvKhach.MultiSelect = false;
            this.dgvKhach.Name = "dgvKhach";
            this.dgvKhach.ReadOnly = true;
            this.dgvKhach.RowHeadersWidth = 24;
            this.dgvKhach.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvKhach.Size = new System.Drawing.Size(850, 430);
            this.dgvKhach.TabIndex = 2;
            this.dgvKhach.SelectionChanged += new System.EventHandler(this.dgvKhach_SelectionChanged);

            // =========================================================
            // tabDat
            // =========================================================
            this.tabDat.Controls.Add(this.grpDat);
            this.tabDat.Controls.Add(this.lblTimDat);
            this.tabDat.Controls.Add(this.txtTimDat);
            this.tabDat.Controls.Add(this.lblThang);
            this.tabDat.Controls.Add(this.cboThang);
            this.tabDat.Controls.Add(this.btnTim);
            this.tabDat.Controls.Add(this.dgvDat);
            this.tabDat.Controls.Add(this.lblDoanhThu);
            this.tabDat.Location = new System.Drawing.Point(4, 24);
            this.tabDat.Name = "tabDat";
            this.tabDat.Padding = new System.Windows.Forms.Padding(3);
            this.tabDat.Size = new System.Drawing.Size(872, 592);
            this.tabDat.TabIndex = 2;
            this.tabDat.Text = "Đặt tour";
            this.tabDat.UseVisualStyleBackColor = true;
            //
            // grpDat
            //
            this.grpDat.Controls.Add(this.lblKhachHang);
            this.grpDat.Controls.Add(this.cboKhach);
            this.grpDat.Controls.Add(this.lblTour);
            this.grpDat.Controls.Add(this.cboTour);
            this.grpDat.Controls.Add(this.lblSoNguoi);
            this.grpDat.Controls.Add(this.nudSoNguoi);
            this.grpDat.Controls.Add(this.lblConCho);
            this.grpDat.Controls.Add(this.lblGiaNguoi);
            this.grpDat.Controls.Add(this.lblGia);
            this.grpDat.Controls.Add(this.lblTyGia);
            this.grpDat.Controls.Add(this.lblNguon);
            this.grpDat.Controls.Add(this.lblTongTien);
            this.grpDat.Controls.Add(this.lblTong);
            this.grpDat.Controls.Add(this.btnDat);
            this.grpDat.Location = new System.Drawing.Point(8, 8);
            this.grpDat.Name = "grpDat";
            this.grpDat.Size = new System.Drawing.Size(520, 235);
            this.grpDat.TabIndex = 0;
            this.grpDat.TabStop = false;
            this.grpDat.Text = "Thông tin đặt tour";
            //
            // lblKhachHang
            //
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new System.Drawing.Point(12, 32);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Text = "Khách hàng:";
            //
            // cboKhach
            //
            this.cboKhach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhach.Location = new System.Drawing.Point(100, 28);
            this.cboKhach.Name = "cboKhach";
            this.cboKhach.Size = new System.Drawing.Size(300, 23);
            this.cboKhach.TabIndex = 1;
            //
            // lblTour
            //
            this.lblTour.AutoSize = true;
            this.lblTour.Location = new System.Drawing.Point(12, 66);
            this.lblTour.Name = "lblTour";
            this.lblTour.Text = "Tour:";
            //
            // cboTour
            //
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.Location = new System.Drawing.Point(100, 62);
            this.cboTour.Name = "cboTour";
            this.cboTour.Size = new System.Drawing.Size(300, 23);
            this.cboTour.TabIndex = 2;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.cboTour_SelectedIndexChanged);
            //
            // lblSoNguoi
            //
            this.lblSoNguoi.AutoSize = true;
            this.lblSoNguoi.Location = new System.Drawing.Point(12, 100);
            this.lblSoNguoi.Name = "lblSoNguoi";
            this.lblSoNguoi.Text = "Số người:";
            //
            // nudSoNguoi
            //
            this.nudSoNguoi.Location = new System.Drawing.Point(100, 96);
            this.nudSoNguoi.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoNguoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoNguoi.Name = "nudSoNguoi";
            this.nudSoNguoi.Size = new System.Drawing.Size(60, 23);
            this.nudSoNguoi.TabIndex = 3;
            this.nudSoNguoi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoNguoi.ValueChanged += new System.EventHandler(this.nudSoNguoi_ValueChanged);
            //
            // lblConCho
            //
            this.lblConCho.AutoSize = true;
            this.lblConCho.ForeColor = System.Drawing.Color.DimGray;
            this.lblConCho.Location = new System.Drawing.Point(175, 100);
            this.lblConCho.Name = "lblConCho";
            this.lblConCho.Text = "";
            //
            // lblGiaNguoi
            //
            this.lblGiaNguoi.AutoSize = true;
            this.lblGiaNguoi.Location = new System.Drawing.Point(12, 134);
            this.lblGiaNguoi.Name = "lblGiaNguoi";
            this.lblGiaNguoi.Text = "Giá/người:";
            //
            // lblGia
            //
            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(100, 134);
            this.lblGia.Name = "lblGia";
            this.lblGia.Text = "";
            //
            // lblTyGia
            //
            this.lblTyGia.AutoSize = true;
            this.lblTyGia.Location = new System.Drawing.Point(12, 160);
            this.lblTyGia.Name = "lblTyGia";
            this.lblTyGia.Text = "";
            //
            // lblNguon
            //
            this.lblNguon.AutoSize = true;
            this.lblNguon.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNguon.ForeColor = System.Drawing.Color.DimGray;
            this.lblNguon.Location = new System.Drawing.Point(250, 159);
            this.lblNguon.Name = "lblNguon";
            this.lblNguon.Padding = new System.Windows.Forms.Padding(2);
            this.lblNguon.Text = "";
            //
            // lblTongTien
            //
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(12, 188);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Text = "Tổng tiền:";
            //
            // lblTong
            //
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTong.ForeColor = System.Drawing.Color.Firebrick;
            this.lblTong.Location = new System.Drawing.Point(100, 184);
            this.lblTong.Name = "lblTong";
            this.lblTong.Text = "0 đ";
            //
            // btnDat
            //
            this.btnDat.BackColor = System.Drawing.Color.SeaGreen;
            this.btnDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDat.ForeColor = System.Drawing.Color.White;
            this.btnDat.Location = new System.Drawing.Point(410, 196);
            this.btnDat.Name = "btnDat";
            this.btnDat.Size = new System.Drawing.Size(95, 30);
            this.btnDat.TabIndex = 4;
            this.btnDat.Text = "Đặt tour";
            this.btnDat.UseVisualStyleBackColor = false;
            this.btnDat.Click += new System.EventHandler(this.btnDat_Click);
            //
            // lblTimDat
            //
            this.lblTimDat.AutoSize = true;
            this.lblTimDat.Location = new System.Drawing.Point(8, 258);
            this.lblTimDat.Name = "lblTimDat";
            this.lblTimDat.Text = "Tìm theo tên khách hoặc tên tour:";
            //
            // txtTimDat
            //
            this.txtTimDat.Location = new System.Drawing.Point(8, 278);
            this.txtTimDat.Name = "txtTimDat";
            this.txtTimDat.Size = new System.Drawing.Size(230, 23);
            this.txtTimDat.TabIndex = 1;
            this.txtTimDat.TextChanged += new System.EventHandler(this.txtTimDat_TextChanged);
            //
            // lblThang
            //
            this.lblThang.AutoSize = true;
            this.lblThang.Location = new System.Drawing.Point(256, 258);
            this.lblThang.Name = "lblThang";
            this.lblThang.Text = "Tháng khởi hành";
            //
            // cboThang
            //
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.Items.AddRange(new object[] {
            "Tất cả", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
            "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"});
            this.cboThang.Location = new System.Drawing.Point(256, 278);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(100, 23);
            this.cboThang.TabIndex = 2;
            this.cboThang.SelectedIndexChanged += new System.EventHandler(this.cboThang_SelectedIndexChanged);
            //
            // btnTim
            //
            this.btnTim.Location = new System.Drawing.Point(372, 277);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(70, 28);
            this.btnTim.TabIndex = 3;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            //
            // dgvDat
            //
            this.dgvDat.AllowUserToAddRows = false;
            this.dgvDat.AllowUserToDeleteRows = false;
            this.dgvDat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDat.BackgroundColor = System.Drawing.Color.White;
            this.dgvDat.Location = new System.Drawing.Point(8, 312);
            this.dgvDat.MultiSelect = false;
            this.dgvDat.Name = "dgvDat";
            this.dgvDat.ReadOnly = true;
            this.dgvDat.RowHeadersWidth = 24;
            this.dgvDat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDat.Size = new System.Drawing.Size(850, 240);
            this.dgvDat.TabIndex = 4;
            //
            // lblDoanhThu
            //
            this.lblDoanhThu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDoanhThu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThu.Location = new System.Drawing.Point(458, 560);
            this.lblDoanhThu.Name = "lblDoanhThu";
            this.lblDoanhThu.Size = new System.Drawing.Size(400, 24);
            this.lblDoanhThu.TabIndex = 5;
            this.lblDoanhThu.Text = "Tổng doanh thu: 0 đ";
            this.lblDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 620);
            this.Controls.Add(this.tabs);
            this.MinimumSize = new System.Drawing.Size(896, 659);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý đặt tour - Bình An v1.0.0";

            this.tabs.ResumeLayout(false);
            this.tabTour.ResumeLayout(false);
            this.tabTour.PerformLayout();
            this.grpTour.ResumeLayout(false);
            this.grpTour.PerformLayout();
            this.tabKhach.ResumeLayout(false);
            this.tabKhach.PerformLayout();
            this.grpKhach.ResumeLayout(false);
            this.grpKhach.PerformLayout();
            this.tabDat.ResumeLayout(false);
            this.tabDat.PerformLayout();
            this.grpDat.ResumeLayout(false);
            this.grpDat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDat)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TabControl tabs;
        private TabPage tabTour, tabKhach, tabDat;

        // Tab Tour
        private GroupBox grpTour;
        private Label lblTenTour, lblDiemDen, lblNgoaiTe, lblKhoiHanh, lblGiaTour, lblSoCho, lblTimTour;
        private TextBox txtTenTour, txtDiemDen, txtTimTour;
        private ComboBox cboNgoaiTe;
        private DateTimePicker dtpKhoiHanh;
        private NumericUpDown nudGia, nudSoCho;
        private Button btnTourThem, btnTourSua, btnTourXoa, btnTourLamMoi;
        private DataGridView dgvTour;

        // Tab Khách hàng
        private GroupBox grpKhach;
        private Label lblTenKH, lblSDT, lblEmail, lblTimKH;
        private TextBox txtTenKH, txtSDT, txtEmail, txtTimKH;
        private Button btnKhachThem, btnKhachSua, btnKhachXoa, btnKhachLamMoi;
        private DataGridView dgvKhach;

        // Tab Đặt tour
        private GroupBox grpDat;
        private Label lblKhachHang, lblTour, lblSoNguoi, lblConCho, lblGiaNguoi, lblGia, lblTyGia, lblNguon, lblTongTien, lblTong, lblTimDat, lblThang, lblDoanhThu;
        private ComboBox cboKhach, cboTour, cboThang;
        private NumericUpDown nudSoNguoi;
        private Button btnDat, btnTim;
        private TextBox txtTimDat;
        private DataGridView dgvDat;
    }
}