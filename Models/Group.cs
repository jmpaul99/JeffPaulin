using System;
using System.Collections.Generic;

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
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
