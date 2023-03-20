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
    public class reportsController : Controller
    {
        private readonly project2Context _context;

        public reportsController(project2Context context)
        {
            _context = context;
        }

        // GET: reports
        public async Task<IActionResult> Index()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
            {
   return View(await _context.report.ToListAsync());
            }


            else
                HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "home");
         
        }
       
            // GET: reports/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
            {

                if (id == null || _context.report == null)
                {
                    return NotFound();
                }

                var report = await _context.report
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (report == null)
                {
                    return NotFound();
                }

                return View(report);
            }

            else
            {

                

                return RedirectToAction("login", "home");
            }
        }

        // GET: reports/Create
        public IActionResult Create()
        {


            ViewData["role"] = HttpContext.Session.GetString("role");


            return View();
           


   

        }

        // POST: reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,title,date,content,isSolved")] report report)
        {
            if (ModelState.IsValid)
            {
                report.date = DateTime.Now;
                report.isSolved = false;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.report == null)
            {
                return NotFound();
            }

            var report = await _context.report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,title,date,content,isSolved")] report report)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss != "admin" || ss != "expert")
            {

                return RedirectToAction("login", "home");
            }

            else
            {

                if (id != report.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        ModelState.Remove("title");
                        ModelState.Remove("date");
                        ModelState.Remove("title");
                        _context.Update(report);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!reportExists(report.Id))
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
                return View(report);
            }
        }

        // GET: reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.report == null)
            {
                return NotFound();
            }

            var report = await _context.report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.report == null)
            {
                return Problem("Entity set 'project2Context.report'  is null.");
            }
            var report = await _context.report.FindAsync(id);
            if (report != null)
            {
                _context.report.Remove(report);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool reportExists(int id)
        {
          return _context.report.Any(e => e.Id == id);
        }
    }
}
