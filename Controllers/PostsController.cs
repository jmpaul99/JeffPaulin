using JeffPaulin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JeffPaulin.Controllers
{
    public class PostsController : Controller
    {
        private readonly jpContext _context;

        public PostsController(jpContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.Where(x => x.IsActive == true && x.IsDraft == false && x.IsDeleted == false && x.BlogPostRecs.Any()).Include(x => x.BlogPostRecs.OrderByDescending(y => y.Blog.BlogName).Take(5)).ThenInclude(x => x.Blog).ToListAsync());
        }
        public async Task<IActionResult> Post(string slug)
        {
            string val = HttpUtility.UrlDecode(slug);
            List<string> vals = val.Split("-", 2).ToList();
            int id = Int32.Parse(vals[0]);
            string title = vals[1];
            Post p = await _context.Posts.Where(x => x.IsActive == true && x.IsDeleted == false && x.IsDraft == false).FirstOrDefaultAsync();
            if (p == null || p.PostHeader != title)
            {
                return NotFound();
            }
            return View(p);
        }
    }
}
