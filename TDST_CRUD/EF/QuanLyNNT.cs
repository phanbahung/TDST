namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuanLyNNT
    {
        public long Id { get; set; }

        [StringLength(14)]
        public string MST { get; set; }

        [StringLength(255)]
        public string TenNNT { get; set; }

        [StringLength(10)]
        public string MaDonVi { get; set; }

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
