using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Organization")]
        [Required]
        public string OrganizationName { get; set; }
        [DisplayName("Organization Description")]
        public string OrganizationDescription { get; set; }
        [DisplayName("Organization Created Date")]
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
