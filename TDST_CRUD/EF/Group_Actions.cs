namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Group_Actions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long IdGA { get; set; }

        public int? IdGroup { get; set; }

        [StringLength(50)]
        public string ActionName { get; set; }

        public virtual Group Group { get; set; }
    }
}
