#nullable disable

namespace JeffPaulin.Models
{
    public partial class CategoryForSession
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SessionId { get; set; }

        public virtual Category1 Category { get; set; }
        public virtual Session Session { get; set; }
    }
}
