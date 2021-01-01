using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Gender")]
        [Required]
        public string GenderName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
