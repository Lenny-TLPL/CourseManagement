using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
            SubjectTeaches = new HashSet<SubjectTeach>();
            Majors = new HashSet<Major>();
            Prerequisites = new HashSet<Subject>();
            Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Term { get; set; }
        public string? Description { get; set; }
        public int? Credit { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SubjectTeach> SubjectTeaches { get; set; }

        public virtual ICollection<Major> Majors { get; set; }
        public virtual ICollection<Subject> Prerequisites { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
