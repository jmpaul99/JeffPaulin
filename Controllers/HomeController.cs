using JeffPaulin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using JeffPaulin.Models.ViewModels;

namespace JeffPaulin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly jpContext _context;

        public HomeController(ILogger<HomeController> logger, jpContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            HomeViewModel h = new HomeViewModel();
            h.blogs = await _context.Blogs.Where(x => x.Active == true && x.BlogPostRecs.Any()).ToListAsync();
            h.posts = await _context.Posts.Where(x => x.IsActive == true && x.IsDeleted == false && x.IsDraft == false && x.BlogPostRecs.Any()).OrderByDescending(x => x.CreatedDate).Take(5).ToListAsync();
            return View(h);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
