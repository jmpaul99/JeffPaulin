using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class Post
    {
        public Post()
        {
            BlogPostRecs = new HashSet<BlogPostRec>();
            PostCategoryRecs = new HashSet<PostCategoryRec>();
            PostEditRecs = new HashSet<PostEditRec>();
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        [DisplayName("Title")]
        [Required]
        public string PostHeader { get; set; }
        [DisplayName("Subtitle")]
        public string PostSubheader { get; set; }
        [AllowHtml]
        [DisplayName("Body")]
        [Required]
        public string PostBody { get; set; }
        [DisplayName("Post Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Author")]
        public string PostedBy { get; set; }
        [DisplayName("Draft")]
        public bool IsDraft { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        [DisplayName("Deleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<BlogPostRec> BlogPostRecs { get; set; }
        public virtual ICollection<PostCategoryRec> PostCategoryRecs { get; set; }
        public virtual ICollection<PostEditRec> PostEditRecs { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
