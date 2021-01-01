using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class BlogPostRec
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int PostId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Post Post { get; set; }
    }
}
