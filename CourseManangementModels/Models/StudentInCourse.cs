using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class StudentInCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public bool? PassedCheck { get; set; }
        public double? Gpa { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
