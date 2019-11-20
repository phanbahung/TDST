namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LOG_UNGDUNG
    {
        [Key]
        public long IdLog { get; set; }

        [StringLength(50)]
        public string FieldName { get; set; }

        [StringLength(200)]
        public string OldValue { get; set; }

        [StringLength(200)]
        public string NewValue { get; set; }

        [StringLength(50)]
        public string TableName { get; set; }

        [StringLength(30)]
        public string UserUpdate { get; set; }

        [StringLength(20)]
        public string IDValue { get; set; }

        public DateTime? TimeUpdate { get; set; }

        [StringLength(10)]
        public string MaDonVi { get; set; }
    }
}
