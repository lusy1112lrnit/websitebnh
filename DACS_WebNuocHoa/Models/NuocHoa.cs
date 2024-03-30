namespace DACS_WebNuocHoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NuocHoa")]
    public partial class NuocHoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NuocHoa()
        {
            CTDonHang = new HashSet<CTDonHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNH { get; set; }

        [StringLength(100)]
        public string TenNH { get; set; }

        [StringLength(1000)]
        public string MoTa { get; set; }

        public decimal? Gia { get; set; }

        [StringLength(250)]
        public string Hinh { get; set; }

        public int? MaLoai { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCapNhat { get; set; }

        public bool? DatBiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDonHang> CTDonHang { get; set; }

        public virtual Loai Loai { get; set; }
    }
}
