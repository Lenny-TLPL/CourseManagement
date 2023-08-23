using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class SubjectPrerequisite
    {
        public int? SubjectId { get; set; }
        public int? PrerequisiteId { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual Subject? Prerequisite { get; set; }
    }
}
