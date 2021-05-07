using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Blog
    {
        public Blog()
        {
            BlogPostRecs = new HashSet<BlogPostRec>();
        }

        public int Id { get; set; }
        public string BlogName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastPostDate { get; set; }
        public string BlogHeader { get; set; }
        public string BlogSubHeader { get; set; }
        public string BlogDescription { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<BlogPostRec> BlogPostRecs { get; set; }
    }
}
