namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Groups
    {
        [Key]
        public int IdUG { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public int? IdGroup { get; set; }

        public virtual Group Group { get; set; }
    }
}
