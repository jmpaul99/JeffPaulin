using System;
using System.Collections.Generic;

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
        public string PostHeader { get; set; }
        public string PostSubheader { get; set; }
        public string PostBody { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PostedBy { get; set; }
        public bool IsDraft { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<BlogPostRec> BlogPostRecs { get; set; }
        public virtual ICollection<PostCategoryRec> PostCategoryRecs { get; set; }
        public virtual ICollection<PostEditRec> PostEditRecs { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
