using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Attendance
    {
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public bool? AttendanceCheck { get; set; }

        public virtual Session Session { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
