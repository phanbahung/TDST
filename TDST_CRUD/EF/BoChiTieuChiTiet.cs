namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BoChiTieuChiTiet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoChiTieuChiTiet()
        {
            DuToanChiTiets = new HashSet<DuToanChiTiet>();
        }

        [Key]
        public int IdChiTieu { get; set; }

        [StringLength(255)]
        public string TenChiTieu { get; set; }

        public int? STT { get; set; }

        public int? IdNhomChuong { get; set; }

        public int? IdNhomTieuMuc { get; set; }

        public int IdBoChiTieu { get; set; }

        [StringLength(10)]
        public string MaDonVi_Tao { get; set; }

        [StringLength(50)]
        public string UserUpdate { get; set; }

        public DateTime? LastUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DuToanChiTiet> DuToanChiTiets { get; set; }
    }
}
