using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JeffPaulin.Models;

namespace JeffPaulin.Controllers
{
    //[Microsoft.AspNetCore.Authorization.Authorize]
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
            return View(await _context.Blogs.Where(x=> x.Active == true && x.LastPostDate != DateTime.MinValue).ToListAsync());
        }

        public async Task<IActionResult> Blog(string slug)
        {
            Blog b = await _context.Blogs.Where(x => x.Active == true && x.LastPostDate != DateTime.MinValue && x.BlogName == slug).FirstOrDefaultAsync();
            if (b == null)
            {
                return NotFound();
            }
            return View();
        }

    }
}
