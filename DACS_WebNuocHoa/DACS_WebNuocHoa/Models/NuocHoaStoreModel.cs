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

        public virtual DbSet<Loai> Loaies { get; set; }
        public virtual DbSet<NuocHoa> NuocHoaes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
