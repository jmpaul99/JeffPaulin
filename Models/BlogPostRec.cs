using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class BlogPostRec
    {
        [NotMapped]
        public bool isChecked { get; set; }
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int PostId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Post Post { get; set; }
    }
}
