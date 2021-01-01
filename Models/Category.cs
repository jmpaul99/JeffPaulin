using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Category
    {
        public Category()
        {
            PostCategoryRecs = new HashSet<PostCategoryRec>();
        }

        public int Id { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<PostCategoryRec> PostCategoryRecs { get; set; }
    }
}
