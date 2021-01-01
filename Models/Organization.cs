using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Organization
    {
        public Organization()
        {
            Groups = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
