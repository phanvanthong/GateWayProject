namespace GetWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notification
    {
        public Guid NotificationID { get; set; }

        public string NotificationContent { get; set; }

        public Guid? Driver_DriverID { get; set; }

        public Guid? Student_StudentID { get; set; }

        public Guid? Teacher_TeacherID { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Student Student { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
