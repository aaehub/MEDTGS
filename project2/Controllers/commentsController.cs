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
    public class commentsController : Controller
    {
        private readonly project2Context _context;

        public commentsController(project2Context context)
        {
            _context = context;
        }

        // GET: comments
        public async Task<IActionResult> Index()


        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {   var project2Context = _context.comments.Include(c => c.article);
            return View(await project2Context.ToListAsync());

            }


            else
                HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "home");



         
        }

        // GET: comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
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


            else
                HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "home");

          
        }

        // GET: comments/Create
        public IActionResult Create()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {  ViewData["articleid"] = new SelectList(_context.article, "Id", "Id");
            return View();

            }


            else
                HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "home");
          
        }

        // POST: comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,comment,articleid,accountid")] comments comments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["articleid"] = new SelectList(_context.article, "Id", "Id", comments.articleid);
            return View(comments);
        }

        // GET: comments/Edit/5
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

        // POST: comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,comment,articleid,accountid")] comments comments)
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

        // GET: comments/Delete/5
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

        // POST: comments/Delete/5
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
