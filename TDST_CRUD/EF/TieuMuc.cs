namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TieuMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTieuMuc { get; set; }

        [StringLength(10)]
        public string MaTieuMuc { get; set; }

        [StringLength(255)]
        public string Ten { get; set; }

        [StringLength(10)]
        public string Muc { get; set; }

        [StringLength(10)]
        public string Loai { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }
    }
}
