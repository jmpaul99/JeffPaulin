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
    public class SessionsController : Controller
    {
        private readonly jpContext _context;

        public SessionsController(jpContext context)
        {
            _context = context;
        }

        // GET: Admin/Sessions
        public async Task<IActionResult> Index()
        {
            var jpContext = _context.Sessions.Include(s => s.Group).Include(s => s.Post).Include(s => s.Term);
            return View(await jpContext.ToListAsync());
        }

        // GET: Admin/Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Group)
                .Include(s => s.Post)
                .Include(s => s.Term)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Admin/Sessions/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName");
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostHeader");
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "TermName");
            Session s = new Session() { IsActive = true, IsDraft = true, SessionDate = DateTime.Now, PlayerForSessions = new List<PlayerForSession>() };
            foreach (Player p in _context.Players.OrderBy(x => x.LastName))
            {
                PlayerForSession ps = new PlayerForSession() { Player = p, PlayerId = p.Id };
                s.PlayerForSessions.Add(ps);
            }
            return View(s);
        }

        // POST: Admin/Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Session session)
        {
            if (ModelState.IsValid)
            {
                foreach (PlayerForSession rec in session.PlayerForSessions)
                {
                    if (rec.isChecked == false)
                    {
                        session.PlayerForSessions.Remove(rec);
                    }
                    else
                    {
                        rec.Attended = true;
                    }
                    if (rec.Notes == null)
                    {
                        rec.Notes = "";
                    }
                }
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Session s = new Session() { IsActive = true, IsDraft = true, SessionDate = DateTime.Now, PlayerForSessions = new List<PlayerForSession>() };
            foreach (Player p in _context.Players.OrderBy(x => x.LastName))
            {
                PlayerForSession ps = new PlayerForSession() { Player = p, PlayerId = p.Id };
                s.PlayerForSessions.Add(ps);
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", session.GroupId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostHeader", session.PostId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "TermName", session.TermId);
            return View(session);
        }

        // GET: Admin/Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.Where(x => x.Id == id).Include(x => x.PlayerForSessions).FirstAsync();
            if (session == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", session.GroupId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostHeader", session.PostId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "TermName", session.TermId);
            foreach (Player p in _context.Players)
            {
                if (session.PlayerForSessions.Any(x => x.PlayerId == p.Id))
                {
                    session.PlayerForSessions.Where(x => x.PlayerId == p.Id).First().isChecked = true;
                }
                else
                {
                    PlayerForSession ps = new PlayerForSession() { PlayerId = p.Id, Player = p };
                    session.PlayerForSessions.Add(ps);
                }
            }
            session.PlayerForSessions.OrderBy(x => x.Player.LastName);
            return View(session);
        }

        // POST: Admin/Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (PlayerForSession ps in session.PlayerForSessions)
                    {
                        ps.SessionId = session.Id;
                        if (_context.PlayerForSessions.Any(x => x.SessionId == ps.SessionId && x.PlayerId == ps.PlayerId) && ps.isChecked == false)
                        {
                            session.PlayerForSessions.Remove(ps);
                            _context.RemoveRange(_context.PlayerForSessions.Where(x => x.SessionId == ps.SessionId && x.PlayerId == ps.PlayerId));
                        }
                        if (_context.PlayerForSessions.Any(x => x.SessionId == ps.SessionId && x.PlayerId == ps.PlayerId) && ps.isChecked == true)
                        {
                            session.PlayerForSessions.Remove(ps);
                        }
                        if (ps.isChecked == false)
                        {
                            session.PlayerForSessions.Remove(ps);
                        }
                        if (ps.isChecked == true)
                        {
                            ps.Attended = true;
                        }
                    }
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "GroupName", session.GroupId);
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "PostHeader", session.PostId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "TermName", session.TermId);
            foreach (Player p in _context.Players)
            {
                if (session.PlayerForSessions.Any(x => x.PlayerId == p.Id))
                {
                    session.PlayerForSessions.Where(x => x.PlayerId == p.Id).First().isChecked = true;
                }
                else
                {
                    PlayerForSession ps = new PlayerForSession() { PlayerId = p.Id, Player = p };
                    session.PlayerForSessions.Add(ps);
                }
            }
            session.PlayerForSessions.OrderBy(x => x.Player.LastName);
            return View(session);
        }

        // GET: Admin/Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Group)
                .Include(s => s.Post)
                .Include(s => s.Term)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Admin/Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
