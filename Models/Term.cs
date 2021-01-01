using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Term
    {
        public Term()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string TermName { get; set; }
        public string TermDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
