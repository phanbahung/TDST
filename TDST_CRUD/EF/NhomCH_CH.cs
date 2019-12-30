namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhomCH_CH
    {
        [Key]
        public long IdNhomCH_CH { get; set; }

        public long? IdNhomChuong { get; set; }

        [StringLength(10)]
        public string MaChuong { get; set; }

        public DateTime? HieuLucTu { get; set; }

        public DateTime? HieuLucDen { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
