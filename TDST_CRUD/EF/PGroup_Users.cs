namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PGroup_Users
    {
        [Key]
        public int IdGU { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public int? IdGroup { get; set; }

        public virtual PGroup PGroup { get; set; }
    }
}
