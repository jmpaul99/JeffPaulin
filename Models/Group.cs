using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Group
    {
        public Group()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        [DisplayName("Group")]
        [Required]
        public string GroupName { get; set; }
        [DisplayName("Group Description")]
        public string GroupDescription { get; set; }
        [DisplayName("Group Created Date")]
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
