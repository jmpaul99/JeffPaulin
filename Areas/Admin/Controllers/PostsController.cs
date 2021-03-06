﻿using System;
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
    public class PostsController : Controller
    {
        private readonly jpContext _context;

        public PostsController(jpContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Admin/Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            System.Security.Claims.ClaimsIdentity ci = new System.Security.Claims.ClaimsIdentity();
            if (User.Identity.Name != null)
            {
                ci = (System.Security.Claims.ClaimsIdentity)User.Identity;
            }
            else
            {
                return Challenge();
            }
                Post p = new Post() 
            {
                CreatedDate = DateTime.Now,
                BlogPostRecs = new List<BlogPostRec>(),
                IsDraft = true,
                IsActive = true,
                PostedBy = $"{ci.FindFirst(System.Security.Claims.ClaimTypes.GivenName).Value} {ci.FindFirst(System.Security.Claims.ClaimTypes.Surname).Value}"
            };
            foreach (Blog b in _context.Blogs)
            {
                BlogPostRec bpr = new BlogPostRec() { BlogId = b.Id, Blog = b };
                p.BlogPostRecs.Add(bpr);
            }
            p.BlogPostRecs.OrderBy(x => x.Blog.BlogName);
            return View(p);
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                foreach (BlogPostRec rec in post.BlogPostRecs.Where(x => x.isChecked == false))
                {
                    post.BlogPostRecs.Remove(rec);
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            post.BlogPostRecs = new List<BlogPostRec>();
            foreach (Blog b in _context.Blogs)
            {
                BlogPostRec bpr = new BlogPostRec() { BlogId = b.Id, Blog = b };
                post.BlogPostRecs.Add(bpr);
            }
            post.CreatedDate = DateTime.Now;
            post.BlogPostRecs.OrderBy(x => x.Blog.BlogName);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Where(x => x.Id == id).Include(x => x.BlogPostRecs).FirstAsync();
            if (post == null)
            {
                return NotFound();
            }
            foreach (Blog b in _context.Blogs)
            {
                if(post.BlogPostRecs.Any(x => x.BlogId == b.Id))
                {
                    post.BlogPostRecs.Where(x => x.BlogId == b.Id).First().isChecked = true;
                }
                else
                {
                    BlogPostRec bpr = new BlogPostRec() { BlogId = b.Id, Blog = b };
                    post.BlogPostRecs.Add(bpr);
                }
                
            }
            post.BlogPostRecs.OrderBy(x => x.Blog.BlogName);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (BlogPostRec pr in post.BlogPostRecs)
                    {
                        pr.PostId = post.Id;
                        if (_context.BlogPostRecs.Any(x=> x.PostId == pr.PostId && x.BlogId == pr.BlogId) && pr.isChecked == false)
                        {
                            post.BlogPostRecs.Remove(pr);
                            _context.RemoveRange(_context.BlogPostRecs.Where(x => x.PostId == pr.PostId && x.BlogId == pr.BlogId));
                        }
                        if (_context.BlogPostRecs.Any(x => x.PostId == pr.PostId && x.BlogId == pr.BlogId) && pr.isChecked == true)
                        {
                            post.BlogPostRecs.Remove(pr);
                        }
                        if(pr.isChecked == false)
                        {
                            post.BlogPostRecs.Remove(pr);
                        }
                    }
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            foreach (Blog b in _context.Blogs)
            {
                if (post.BlogPostRecs.Any(x => x.BlogId == b.Id))
                {
                    post.BlogPostRecs.Where(x => x.BlogId == b.Id).First().isChecked = true;
                }
                else
                {
                    BlogPostRec bpr = new BlogPostRec() { BlogId = b.Id, Blog = b };
                    post.BlogPostRecs.Add(bpr);
                }

            }
            post.BlogPostRecs.OrderBy(x => x.Blog.BlogName);
            return View(post);
        }

        // GET: Admin/Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
