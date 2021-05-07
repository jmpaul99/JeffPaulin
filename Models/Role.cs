using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoleRecs = new HashSet<UserRoleRec>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<UserRoleRec> UserRoleRecs { get; set; }
    }
}
