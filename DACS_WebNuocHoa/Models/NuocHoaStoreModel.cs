using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DACS_WebNuocHoa.Models
{
    public partial class NuocHoaStoreModel : DbContext
    {
        public NuocHoaStoreModel()
            : base("name=NuocHoaStoreModel")
        {
        }
        public virtual DbSet<CTDonHang> CTDonHang { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<Loai> Loai { get; set; }
        public virtual DbSet<NuocHoa> NuocHoa { get; set; }

        public virtual DbSet<ThanhToan> ThanhToan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CTDonHang>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TongTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.CTDonHang)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NuocHoa>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NuocHoa>()
                .HasMany(e => e.CTDonHang)
                .WithRequired(e => e.NuocHoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThanhToan>()
                .HasMany(e => e.DonHang)
                .WithRequired(e => e.ThanhToan)
                .WillCascadeOnDelete(false);

        }
    }
}
