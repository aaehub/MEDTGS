using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project2.Data;
using project2.Models;

namespace project2.Controllers
{
    public class comments1Controller : Controller
    {
        private readonly project2Context _context;

        public comments1Controller(project2Context context)
        {
            _context = context;
        }

        // GET: comments1
        public async Task<IActionResult> Index()
        {
            var project2Context = _context.comments.Include(c => c.article);
            return View(await project2Context.ToListAsync());
        }

        // GET: comments1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.comments == null)
            {
                return NotFound();
            }

            var comments = await _context.comments
                .Include(c => c.article)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // GET: comments1/Create
        public IActionResult Create()
        {
            ViewData["articleid"] = new SelectList(_context.article, "Id", "Id");
            return View();
        }

        // POST: comments1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,comment,articleid")] comments comments)
        {
            
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
         
        }

        // GET: comments1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.comments == null)
            {
                return NotFound();
            }

            var comments = await _context.comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            ViewData["articleid"] = new SelectList(_context.article, "Id", "Id", comments.articleid);
            return View(comments);
        }

        // POST: comments1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,comment,articleid")] comments comments)
        {
            if (id != comments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!commentsExists(comments.Id))
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
            ViewData["articleid"] = new SelectList(_context.article, "Id", "Id", comments.articleid);
            return View(comments);
        }

        // GET: comments1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.comments == null)
            {
                return NotFound();
            }

            var comments = await _context.comments
                .Include(c => c.article)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comments == null)
            {
                return NotFound();
            }

            return View(comments);
        }

        // POST: comments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.comments == null)
            {
                return Problem("Entity set 'project2Context.comments'  is null.");
            }
            var comments = await _context.comments.FindAsync(id);
            if (comments != null)
            {
                _context.comments.Remove(comments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool commentsExists(int id)
        {
          return _context.comments.Any(e => e.Id == id);
        }
    }
}
