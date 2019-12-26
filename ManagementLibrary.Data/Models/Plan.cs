namespace GetWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Plan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Plan()
        {
            Students = new HashSet<Student>();
        }

        public Guid PlanID { get; set; }

        public DateTime PGetWayate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateBy { get; set; }

        public string DriverName { get; set; }

        public string TeacherName { get; set; }

        public Guid? DriverID { get; set; }

        public Guid? CarID { get; set; }

        public Guid? TeacherID { get; set; }

        public virtual Car Car { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
