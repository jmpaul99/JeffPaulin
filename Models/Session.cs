using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Session
    {
        public Session()
        {
            CategoryForSessions = new HashSet<CategoryForSession>();
            PlayerForSessions = new HashSet<PlayerForSession>();
        }

        public int Id { get; set; }
        public int? PostId { get; set; }
        public int GroupId { get; set; }
        public int TermId { get; set; }
        public DateTime SessionDate { get; set; }
        public bool IsDraft { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Group Group { get; set; }
        public virtual Post Post { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<CategoryForSession> CategoryForSessions { get; set; }
        public virtual ICollection<PlayerForSession> PlayerForSessions { get; set; }
    }
}
