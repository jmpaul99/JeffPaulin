using JeffPaulin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JeffPaulin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SessionCategoriesController : Controller
    {
        private readonly jpContext _context;

        public SessionCategoriesController(jpContext context)
        {
            _context = context;
        }

        // GET: Admin/SessionCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories1.ToListAsync());
        }

        // GET: Admin/SessionCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Categories1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category1 == null)
            {
                return NotFound();
            }

            return View(category1);
        }

        // GET: Admin/SessionCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SessionCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category")] Category1 category1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category1);
        }

        // GET: Admin/SessionCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Categories1.FindAsync(id);
            if (category1 == null)
            {
                return NotFound();
            }
            return View(category1);
        }

        // POST: Admin/SessionCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category")] Category1 category1)
        {
            if (id != category1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Category1Exists(category1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category1);
        }

        // GET: Admin/SessionCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category1 = await _context.Categories1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category1 == null)
            {
                return NotFound();
            }

            return View(category1);
        }

        // POST: Admin/SessionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category1 = await _context.Categories1.FindAsync(id);
            _context.Categories1.Remove(category1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Category1Exists(int id)
        {
            return _context.Categories1.Any(e => e.Id == id);
        }
    }
}
