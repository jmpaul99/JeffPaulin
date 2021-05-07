using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
