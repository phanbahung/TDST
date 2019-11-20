namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GiaoDichs")]
    public partial class GiaoDich
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string Khct { get; set; }

        [StringLength(255)]
        public string Khct_soct { get; set; }

        public DateTime? Ngay_ht { get; set; }

        public DateTime? Ngay_kbac { get; set; }

        public DateTime? Ngay_kb { get; set; }

        [StringLength(14)]
        public string Tin { get; set; }

        [StringLength(7)]
        public string Ma_cqthu { get; set; }

        [StringLength(255)]
        public string Ten_nnthue { get; set; }

        [StringLength(255)]
        public string Ky_thue { get; set; }

        [StringLength(255)]
        public string Ma_xa { get; set; }

        [StringLength(255)]
        public string So_tkno { get; set; }

        [StringLength(10)]
        public string Ma_chuong { get; set; }

        [StringLength(255)]
        public string Ma_tmuc { get; set; }

        public decimal? So_tien { get; set; }

        [StringLength(255)]
        public string Tk_co_dtl { get; set; }

        [StringLength(10)]
        public string MaDonViQLKhoanThu { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        [StringLength(100)]
        public string FileChungTu { get; set; }

        public int? NhomChuong { get; set; }

        public int? NhomTieuMuc { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
