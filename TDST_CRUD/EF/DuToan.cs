namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DuToan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DuToan()
        {
            DuToanChiTiets = new HashSet<DuToanChiTiet>();
        }

        [Key]
        public int IdDuToan { get; set; }

        [StringLength(200)]
        public string TenDuToan { get; set; }

        public int IdBoChiTieu { get; set; }

        public bool? TrangThai { get; set; }

        public int? NamDuToan { get; set; }

        [StringLength(2)]
        public string CoQuanBanHanh { get; set; }

        [StringLength(10)]
        public string MaDonVi_Tao { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(30)]
        public string UserUpdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DuToanChiTiet> DuToanChiTiets { get; set; }
    }
}
