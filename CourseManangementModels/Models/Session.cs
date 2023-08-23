using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Session
    {
        public Session()
        {
            Attendances = new HashSet<Attendance>();
        }

        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? TeacherId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool? CanceledCheck { get; set; }
        public DateTime Date { get; set; }
        public string Slot { get; set; } = null!;

        public virtual Course Course { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
