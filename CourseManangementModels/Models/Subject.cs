using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Term { get; set; }
        public string? Description { get; set; }
        public int? Credit { get; set; }
        public int? MajorId { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
