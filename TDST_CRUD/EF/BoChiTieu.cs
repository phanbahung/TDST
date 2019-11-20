namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoChiTieus")]
    public partial class BoChiTieu
    {
        [Key]
        public int IdBoChiTieu { get; set; }

        [StringLength(100)]
        public string TenBoChiTieu { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public bool Locked { get; set; }

        public int? MaDonVi_Tao { get; set; }
    }
}
