using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using project2.Data;
using project2.Models;

namespace project2.Controllers
{
    public class articlesController : Controller
    {
        private readonly project2Context _context;

        public articlesController(project2Context context)
        {
            _context = context;
        }

        // GET: articles
        public async Task<IActionResult> Index()
        {
              return View(await _context.article.ToListAsync());
        }

        // GET: articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }





            return View(article);
        }

        // GET: articles/Create
        public IActionResult Create()
        {
            return View();
        }
      
        // POST: articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,topic,category,tag,description")] article article)
        {

            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                article.imagefilename = filename;
            }

            
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
           
        }












        // GET: articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        // POST: articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,topic,category,tag,description,imagefilename")] article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!articleExists(article.Id))
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
            return View(article);
        }

        // GET: articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.article == null)
            {
                return NotFound();
            }

            var article = await _context.article
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.article == null)
            {
                return Problem("Entity set 'project2Context.article'  is null.");
            }
            var article = await _context.article.FindAsync(id);
            if (article != null)
            {
                _context.article.Remove(article);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool articleExists(int id)
        {
          return _context.article.Any(e => e.Id == id);
        }
    }
}
