using JeffPaulin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace JeffPaulin.Controllers
{
    public class BlogsController : Controller
    {
        private readonly jpContext _context;

        public BlogsController(jpContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.Where(x => x.Active == true && x.BlogPostRecs.Any()).Include(x => x.BlogPostRecs.OrderByDescending(y => y.Post.CreatedDate).Take(5)).ThenInclude(x=> x.Post).ToListAsync());
        }

        public async Task<IActionResult> Blog(string slug)
        {
            string val = HttpUtility.UrlDecode(slug);
            Blog b = await _context.Blogs.Where(x => x.Active == true && x.BlogPostRecs.Any() && x.BlogName == val).Include(x => x.BlogPostRecs).ThenInclude(x => x.Post).FirstOrDefaultAsync();
            if (b == null)
            {
                return NotFound();
            }
            return View(b);
        }

    }
}
