namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NhomChuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdNhomChuong { get; set; }

        [StringLength(100)]
        public string TenNhomChuong { get; set; }

        [StringLength(1500)]
        public string Ds_MaChuong { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(10)]
        public string MaDonVi_Tao { get; set; }
    }
}
