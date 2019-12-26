using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetWay.API.Models
{
    public class CarUpdateViewModel
    {
        public CarUpdateViewModel()
        {
        }

        public string LicensePlate { get; set; }

        public Guid? GarageID { get; set; }

        public Guid? CarTypeID { get; set; }

        public string CarNumber { get; set; }
    }
}