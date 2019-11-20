namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DonVi
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDonVi { get; set; }

        [StringLength(255)]
        public string TenDonVi { get; set; }

        [StringLength(255)]
        public string TenVietTat1 { get; set; }

        [StringLength(255)]
        public string TenVietTat2 { get; set; }

        [StringLength(10)]
        public string MaDonViCha { get; set; }

        public int? Cap { get; set; }

        public int? STT { get; set; }

        [StringLength(100)]
        public string MaCqThu { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
