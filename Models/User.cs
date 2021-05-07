using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class User
    {
        public User()
        {
            UserRoleRecs = new HashSet<UserRoleRec>();
        }

        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<UserRoleRec> UserRoleRecs { get; set; }
    }
}
