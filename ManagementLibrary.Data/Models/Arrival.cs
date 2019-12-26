namespace GetWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Arrival
    {
        public Guid ArrivalID { get; set; }

        public float Longitude { get; set; }

        public float Latitude { get; set; }
    }
}
