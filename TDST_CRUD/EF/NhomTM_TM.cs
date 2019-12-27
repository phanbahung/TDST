namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhomTM_TM
    {
        [Key]
        public long IdNhomTM_TM { get; set; }

        public long? IdNhomTieuMuc { get; set; }

        [StringLength(10)]
        public string MaTieuMuc { get; set; }

        public DateTime? HieuLucTu { get; set; }

        public DateTime? DieuLucDen { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
