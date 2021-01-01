using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [DisplayName("Blog Name")]
        [Required]
        public string BlogName { get; set; }
        [DisplayName("Blog Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Blog Last Post Date")]
        public DateTime LastPostDate { get; set; }
        [DisplayName("Blog Header")]
        public string BlogHeader { get; set; }
        [DisplayName("Blog Subheader")]
        public string BlogSubHeader { get; set; }
        [DisplayName("Blog Description")]
        public string BlogDescription { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<BlogPostRec> BlogPostRecs { get; set; }
    }
}
