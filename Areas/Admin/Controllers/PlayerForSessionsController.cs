using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JeffPaulin.Models;
using Microsoft.AspNetCore.Authorization;

namespace JeffPaulin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PlayerForSessionsController : Controller
    {
        private readonly jpContext _context;

        public PlayerForSessionsController(jpContext context)
        {
            _context = context;
        }

        // GET: Admin/PlayerForSessions
        public async Task<IActionResult> Index()
        {
            List<PlayerForSession> ps = new List<PlayerForSession>();
            ps = await _context.PlayerForSessions.Include(p => p.Player).Include(p => p.Session).ToListAsync();
            return View(ps);
        }

        // GET: Admin/PlayerForSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerForSession = await _context.PlayerForSessions
                .Include(p => p.Player)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerForSession == null)
            {
                return NotFound();
            }

            return View(playerForSession);
        }

        // GET: Admin/PlayerForSessions/Create
        public IActionResult Create()
        {
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");
            return View();
        }

        // POST: Admin/PlayerForSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerId,SessionId,Attended,WorkEthic,TechnicalImprovementDuringSession,TechnicalImprovementFromPreviousSession,Notes")] PlayerForSession playerForSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerForSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", playerForSession.PlayerId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", playerForSession.SessionId);
            return View(playerForSession);
        }

        // GET: Admin/PlayerForSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerForSession = await _context.PlayerForSessions.Where(x => x.Id == id).Include(x => x.Session).Include(x => x.Player).FirstAsync();
            if (playerForSession == null)
            {
                return NotFound();
            }
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", playerForSession.PlayerId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", playerForSession.SessionId);
            return View(playerForSession);
        }

        // POST: Admin/PlayerForSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerId,SessionId,Attended,WorkEthic,TechnicalImprovementDuringSession,TechnicalImprovementFromPreviousSession,Notes")] PlayerForSession playerForSession)
        {
            if (id != playerForSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerForSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerForSessionExists(playerForSession.Id))
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
            ViewData["PlayerId"] = new SelectList(_context.Players, "Id", "FirstName", playerForSession.PlayerId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", playerForSession.SessionId);
            return View(playerForSession);
        }

        // GET: Admin/PlayerForSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerForSession = await _context.PlayerForSessions
                .Include(p => p.Player)
                .Include(p => p.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerForSession == null)
            {
                return NotFound();
            }

            return View(playerForSession);
        }

        // POST: Admin/PlayerForSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerForSession = await _context.PlayerForSessions.FindAsync(id);
            _context.PlayerForSessions.Remove(playerForSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerForSessionExists(int id)
        {
            return _context.PlayerForSessions.Any(e => e.Id == id);
        }
    }
}
