namespace DACS_WebNuocHoa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Reflection;

    [Table("NuocHoa")]
    public partial class NuocHoa
    {
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

        public virtual Loai Loai { get; set; }

        
    }
}
