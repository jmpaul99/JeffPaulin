using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeffPaulin.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Blog> blogs { get; set; }
        public List<Post> posts { get; set; }
    }
}
