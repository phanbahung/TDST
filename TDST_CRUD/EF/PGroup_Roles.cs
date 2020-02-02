namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PGroup_Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdGR { get; set; }

        public int? IdGroup { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        public virtual PGroup PGroup { get; set; }
    }
}
