namespace DACS_WebNuocHoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTDonHang")]
    public partial class CTDonHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNH { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [StringLength(100)]
        public string TenNH { get; set; }

        public int? SoLuong { get; set; }

        public decimal? Gia { get; set; }

        public virtual DonHang DonHang { get; set; }

        public virtual NuocHoa NuocHoa { get; set; }
    }
}
