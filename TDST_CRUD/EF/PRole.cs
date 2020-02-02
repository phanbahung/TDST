namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PRole
    {
        [Key]
        public int IdRole { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionName { get; set; }

        [StringLength(50)]
        public string ControllerName { get; set; }

        [StringLength(200)]
        public string MoTa { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }
    }
}
