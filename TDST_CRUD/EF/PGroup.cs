namespace TDST_CRUD.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PGroup()
        {
            PGroup_Roles = new HashSet<PGroup_Roles>();
            PGroup_Users = new HashSet<PGroup_Users>();
        }

        [Key]
        public int IdGroup { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        [StringLength(3)]
        public string DataZone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PGroup_Roles> PGroup_Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PGroup_Users> PGroup_Users { get; set; }
    }
}
