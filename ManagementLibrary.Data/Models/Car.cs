namespace GetWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Plans = new HashSet<Plan>();
        }

        public Guid CarID { get; set; }

        public string LicensePlate { get; set; }

        public Guid? GarageID { get; set; }

        public Guid? CarTypeID { get; set; }

        public string CarNumber { get; set; }

        public virtual CarType CarType { get; set; }

        public virtual Garage Garage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plan> Plans { get; set; }
    }
}
