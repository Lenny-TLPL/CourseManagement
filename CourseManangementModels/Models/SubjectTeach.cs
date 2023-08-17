using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class SubjectTeach
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public bool? Status { get; set; }

        public virtual Subject Subject { get; set; } = null!;
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
