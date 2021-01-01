using System;
using System.Collections.Generic;

#nullable disable

namespace JeffPaulin.Models
{
    public partial class PostEditRec
    {
        public PostEditRec()
        {
            InverseLastEdit = new HashSet<PostEditRec>();
        }

        public int Id { get; set; }
        public int PostId { get; set; }
        public int? LastEditId { get; set; }
        public string EditedPostHeader { get; set; }
        public string EditedPostSubheader { get; set; }
        public string EditedPostBody { get; set; }
        public string EditNotes { get; set; }
        public DateTime EditDate { get; set; }
        public string EditedBy { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PostEditRec LastEdit { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<PostEditRec> InverseLastEdit { get; set; }
    }
}
