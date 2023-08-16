using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            StudentInCourses = new HashSet<StudentInCourse>();
        }

        public int Id { get; set; }
        public string StudentName { get; set; } = null!;
        public string? Image { get; set; }
        public string Email { get; set; } = null!;
        public int Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public int MajorId { get; set; }
        public string? Status { get; set; }
        public int? Term { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Address { get; set; }

        public virtual Major Major { get; set; } = null!;
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<StudentInCourse> StudentInCourses { get; set; }
    }
}
