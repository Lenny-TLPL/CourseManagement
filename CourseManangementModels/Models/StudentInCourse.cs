using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class StudentInCourse
    {
        public int Id { get; set; }
        public string MajorName { get; set; } = null!;
        public int? PassedCheck { get; set; }
        public double? Gpa { get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}
