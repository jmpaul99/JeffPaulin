using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class UserRoleRec
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? OrganizationId { get; set; }
        public bool? Active { get; set; }
        public DateTime AssignedDate { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
