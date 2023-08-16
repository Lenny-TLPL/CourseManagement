using System;
using System.Collections.Generic;

namespace CourseManangementModels.Models
{
    public partial class Room
    {
        public Room()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string RoomNo { get; set; } = null!;

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
