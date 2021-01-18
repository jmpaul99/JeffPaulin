using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Post Category")]
        [Required]
        public string Category1 { get; set; }

        public virtual ICollection<PostCategoryRec> PostCategoryRecs { get; set; }
    }
}
