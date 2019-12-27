namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhomTieuMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdNhomTieuMuc { get; set; }

        [StringLength(200)]
        public string TenNhomTieuMuc { get; set; }

        [StringLength(1500)]
        public string Ds_MaTieuMuc { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }

        public bool? Scan { get; set; }
    }
}
