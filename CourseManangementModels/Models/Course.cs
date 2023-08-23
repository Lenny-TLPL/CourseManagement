using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Course
    {
        public Course()
        {
            Sessions = new HashSet<Session>();
            StudentInCourses = new HashSet<StudentInCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? TeacherId { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StudentQuantity { get; set; }

        public virtual Semester Semester { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual ICollection<StudentInCourse> StudentInCourses { get; set; }
    }
}
