#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace JeffPaulin.Models
{
    public partial class BlogPostRec
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int PostId { get; set; }
        [NotMapped]
        public bool isChecked {get; set;}


        public virtual Blog Blog { get; set; }
        public virtual Post Post { get; set; }
    }
}
