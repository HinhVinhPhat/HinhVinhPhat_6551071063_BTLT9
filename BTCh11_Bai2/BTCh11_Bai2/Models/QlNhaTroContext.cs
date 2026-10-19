using Microsoft.EntityFrameworkCore;

namespace BTCh11_Bai2.Models;

public partial class QlNhaTroContext : DbContext
{
    public QlNhaTroContext(DbContextOptions<QlNhaTroContext> options) : base(options) { }

    public virtual DbSet<KhachThue> KhachThues { get; set; }
    public virtual DbSet<PhongTro> PhongTros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhongTro>(e =>
        {
            e.HasKey(x => x.MaPhong);
            e.ToTable("PhongTro");
            e.Property(x => x.TenPhong).HasMaxLength(20);
            e.Property(x => x.GiaThue).HasColumnType("decimal(18, 0)");
            e.Property(x => x.TinhTrang).HasMaxLength(20);
        });

        modelBuilder.Entity<KhachThue>(e =>
        {
            e.HasKey(x => x.MaKhach);
            e.ToTable("KhachThue");
            e.Property(x => x.HoTen).HasMaxLength(100);
            e.Property(x => x.GioiTinh).HasMaxLength(5);
            e.Property(x => x.SoDienThoai).HasMaxLength(15).IsUnicode(false);
            e.Property(x => x.TenTinh).HasMaxLength(100);
            e.Property(x => x.TenPhuongXa).HasMaxLength(100);
            e.HasOne(x => x.MaPhongNavigation)
             .WithMany(p => p.KhachThues)
             .HasForeignKey(x => x.MaPhong)
             .OnDelete(DeleteBehavior.ClientSetNull)
             .HasConstraintName("FK_KhachThue_PhongTro");
        });
    }
}
