using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Category1
    {
        public Category1()
        {
            CategoryForSessions = new HashSet<CategoryForSession>();
        }

        public int Id { get; set; }
        [DisplayName("Session Category")]
        [Required]
        public string Category { get; set; }

        public virtual ICollection<CategoryForSession> CategoryForSessions { get; set; }
    }
}
