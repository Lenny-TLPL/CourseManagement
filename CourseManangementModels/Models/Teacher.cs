using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public int UserBasicId { get; set; }

        public virtual UserBasic UserBasic { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
