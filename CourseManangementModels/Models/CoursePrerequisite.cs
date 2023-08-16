using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class CoursePrerequisite
    {
        public int? CourseId { get; set; }
        public int? Prerequisite { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Course? PrerequisiteNavigation { get; set; }
    }
}
