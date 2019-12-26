namespace GetWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Garage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Garage()
        {
            Cars = new HashSet<Car>();
            Drivers = new HashSet<Driver>();
        }

        public Guid GarageID { get; set; }

        public string GarageName { get; set; }

        public string Address { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string Descript { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
