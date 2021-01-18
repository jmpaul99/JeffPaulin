using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerForSessions = new HashSet<PlayerForSession>();
        }

        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Player Created Date")]
        public DateTime CreatedDate { get; set; }
        public int GenderId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual ICollection<PlayerForSession> PlayerForSessions { get; set; }
    }
}
