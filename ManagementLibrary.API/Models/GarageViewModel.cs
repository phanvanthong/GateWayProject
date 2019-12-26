using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetWay.API.Models
{
    public class GarageViewModel
    {
        public GarageViewModel()
        {
        }

        public Guid GarageID { get; set; }

        public string GarageName { get; set; }

        public string Address { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}