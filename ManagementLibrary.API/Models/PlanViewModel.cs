using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetWay.API.Models
{
    public class PlanViewModel
    {
        public PlanViewModel()
        {
        }

        public Guid PlanID { get; set; }

        public Guid? DriverID { get; set; }

        public Guid? CarID { get; set; }

        public Guid? TeacherID { get; set; }
    }
}