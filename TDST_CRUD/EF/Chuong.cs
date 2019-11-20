namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chuong
    {
        [Key]
        public int IdChuong { get; set; }

        [StringLength(10)]
        public string MaChuong { get; set; }

        [StringLength(255)]
        public string TenChuong { get; set; }
    }
}
