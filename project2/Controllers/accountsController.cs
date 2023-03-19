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
    public class accountsController : Controller
    {
        private readonly project2Context _context;

        public accountsController(project2Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> register([Bind("username,email,password,gender,role,Date")] accounts myusers)
        {



            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"L:\\project graduation\\DB\\db2.mdf\";Integrated Security=True;Connect Timeout=30");
            string sql;
            conn.Open();

            Boolean flage = false;
            sql = "select * from accounts where username = '" + myusers.username + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                flage = true;
            }
            reader.Close();
          
            if (flage == true)
            {
                ViewData["message"] = "name already exists";

            }
            else
            {

                myusers.role = "customer";
               
                 myusers.Date = DateTime.Now;
          
              



                HttpContext.Session.SetString("username", myusers.username);
                HttpContext.Session.SetString("role", myusers.role);


                _context.Add(myusers);



                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Id", Convert.ToString(myusers.Id));
                return RedirectToAction("customerhome", "home");
            }
            conn.Close();
            return View();





        }
        // GET: accounts
        public async Task<IActionResult> Index()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {


                return View(await _context.accounts.ToListAsync());
            }


            else
             HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "home");

           
        }

        // GET: accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {



                if (id == null || _context.accounts == null)
                {
                    return NotFound();
                }

                var accounts = await _context.accounts
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (accounts == null)
                {
                    return NotFound();
                }

                return View(accounts);
            }


            else
                return RedirectToAction("login", "home");
          
        }

        // GET: accounts/Create
        public IActionResult Create()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {
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

        // POST: accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,username,email,password,gender,role,Date")] accounts accounts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accounts);
        }

        // GET: accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.accounts == null)
            {
                return NotFound();
            }

            var accounts = await _context.accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            return View(accounts);
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public async Task<IActionResult> Edit(int id, [Bind("Id,username,email,password,gender,role,Date")] accounts accounts )
        {

            accounts.Date = DateTime.Now;

            if (id != accounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    ModelState.Remove("Date");
                    _context.Update(accounts);
                   
                    await _context.SaveChangesAsync();
                   
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!accountsExists(accounts.Id))
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
            return View(accounts);
        }

        // GET: accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.accounts == null)
            {
                return NotFound();
            }

            var accounts = await _context.accounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.accounts == null)
            {
                return Problem("Entity set 'project2Context.accounts'  is null.");
            }
            var accounts = await _context.accounts.FindAsync(id);
            if (accounts != null)
            {
                _context.accounts.Remove(accounts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool accountsExists(int id)
        {
          return _context.accounts.Any(e => e.Id == id);
        }
    }
}
