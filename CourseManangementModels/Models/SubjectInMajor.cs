using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class SubjectInMajor
    {
        public int? MajorId { get; set; }
        public int? SubjectId { get; set; }

        public virtual Major? Major { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
