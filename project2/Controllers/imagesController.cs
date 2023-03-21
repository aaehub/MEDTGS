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
    public class imagesController : Controller
    {
        private readonly project2Context _context;

        public imagesController(project2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> addimage()
        {

            return View();
        }


        // GET: images
        public async Task<IActionResult> Index()
        {
              return _context.images != null ? 
                          View(await _context.images.ToListAsync()) :
                          Problem("Entity set 'project2Context.images'  is null.");
        }

        // GET: images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.images == null)
            {
                return NotFound();
            }

            var images = await _context.images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // GET: images/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,articleid,image")] images images)
        {
            if (ModelState.IsValid)
            {
                _context.Add(images);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(images);
        }

        // GET: images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.images == null)
            {
                return NotFound();
            }

            var images = await _context.images.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }
            return View(images);
        }

        // POST: images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,articleid,image")] images images)
        {
            if (id != images.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(images);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!imagesExists(images.Id))
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
            return View(images);
        }

        // GET: images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.images == null)
            {
                return NotFound();
            }

            var images = await _context.images
                .FirstOrDefaultAsync(m => m.Id == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        // POST: images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.images == null)
            {
                return Problem("Entity set 'project2Context.images'  is null.");
            }
            var images = await _context.images.FindAsync(id);
            if (images != null)
            {
                _context.images.Remove(images);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool imagesExists(int id)
        {
          return (_context.images?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
