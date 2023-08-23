using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class UserBasic
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
