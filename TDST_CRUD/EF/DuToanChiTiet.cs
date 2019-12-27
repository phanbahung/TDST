namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DuToanChiTiet
    {
        [Key]
        public long IdChiTietDuToan { get; set; }

        public int? IdChiTieu { get; set; }

        [StringLength(10)]
        public string MaDonVi { get; set; }

        public int? MaCqThu { get; set; }

        public int? Quy { get; set; }

        public int? IdDuToan { get; set; }

        [StringLength(50)]
        public string TenVietTat { get; set; }

        public decimal? SoDuToanGiao { get; set; }

        public decimal? SoThucHien { get; set; }

        public decimal? SoThucHien_KBac { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(10)]
        public string MaDonVi_Tao { get; set; }

        public virtual BoChiTieuChiTiet BoChiTieuChiTiet { get; set; }

        public virtual DuToan DuToan { get; set; }
    }
}
