using Microsoft.EntityFrameworkCore;
using QlSach.Model;

namespace QlSach.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            
        }
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<CTHoaDon> CTHoaDones { get; set;}
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public DbSet<Sach> Saches { get; set; }
        public DbSet<TacGia> TacGias { get; set;}
        public DbSet<ThamGia> ThamGias { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTHoaDon>()
                .HasKey(ct => new { ct.MaDH, ct.MaSach });
            modelBuilder.Entity<CTHoaDon>()
                .HasOne(dh => dh.DonHang)
                .WithMany(dh=> dh.CTHoaDons)
                .HasForeignKey(dh => dh.MaDH)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CTHoaDon>()
                .HasOne(s=>s.Sach)
                .WithMany(s=> s.CTHoaDons)
                .HasForeignKey(s=>s.MaSach)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThamGia>()
                .HasKey(ct => new { ct.MaTacGia, ct.MaSach });
            modelBuilder.Entity<ThamGia>()
                .HasOne(dh => dh.TacGia)
                .WithMany(dh => dh.ThamGias)
                .HasForeignKey(dh => dh.MaTacGia)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ThamGia>()
                .HasOne(s => s.Sach)
                .WithMany(s => s.ThamGias)
                .HasForeignKey(s => s.MaSach)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
