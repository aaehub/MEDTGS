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

        public async Task<IActionResult> search()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
            {


                List<accounts> brItems = new List<accounts>();

                return View(brItems);

            }
            else {
                 return RedirectToAction("login", "home");

            }



        }
     

        // POST: items/search
        [HttpPost]
        public async Task<IActionResult> Search(string s)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
            {

                var brItems = await _context.accounts.FromSqlRaw("select * from accounts where username LIKE '%" + s + "%' ").ToListAsync();
                return View(brItems);
            }
            else return RedirectToAction("login","home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> register([Bind("username,email,password,gender,role,Date")] accounts myusers)
        {



            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("project2Context");
            SqlConnection conn = new SqlConnection(conStr); string sql;
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
             //   HttpContext.Session.SetString("Id", Convert.ToString(myusers.Id));
           
                return RedirectToAction("login", "home");
            }
            conn.Close();
            return View();





        }
        // GET: accounts
        public async Task<IActionResult> Index()
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
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
            if (ss == "admin" || ss == "expert")
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
        public async Task<IActionResult> Create([Bind("Id,username,email,password,gender,role,Date")] accounts myusers)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin")
            {

                var builder = WebApplication.CreateBuilder();
                string conStr = builder.Configuration.GetConnectionString("project2Context");
                SqlConnection conn = new SqlConnection(conStr); string sql;
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
                    conn.Close();

                }
                else
                {




                    if (ModelState.IsValid)
                    {
                        myusers.Date = DateTime.Now;
                        _context.Add(myusers);
                        await _context.SaveChangesAsync();
                        conn.Close();
                        return RedirectToAction(nameof(Index));


                    }










                }

                conn.Close();

                return View(myusers);
            }
            else {
                return RedirectToAction("login", "home");        
                    }
        }

        // GET: accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss != "admin")
            {

                return RedirectToAction("login", "home");
            }

            else { 


                if (id == null || _context.accounts == null)
            {
                return NotFound(); }


            var accounts = await _context.accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            return View(accounts);
        }
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public async Task<IActionResult> Edit(int id, [Bind("Id,username,email,password,gender,role,Date")] accounts accounts )
        {

            string ss = HttpContext.Session.GetString("role");
            if (ss != "admin")
            {

                return RedirectToAction("login", "home");
            }

            else
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
            } }

        // GET: accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss != "admin")
            {

                return RedirectToAction("login", "home");
            }

            else
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
        }

        // POST: accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string ss = HttpContext.Session.GetString("role");
            if (ss != "admin")
            {

                return RedirectToAction("login", "home");
            }

            else
            {

                if (_context.accounts == null)
                {
                    return Problem("Entity set 'project2Context.accounts'  is null.");
                }
                var accounts = await _context.accounts.FindAsync(id);
                if (accounts != null)
                {
                    var builder = WebApplication.CreateBuilder();
                    string conStr = builder.Configuration.GetConnectionString("project2Context");
                    SqlConnection conn1 = new SqlConnection(conStr);


                    string sql;
                    sql = "delete  from comments where accountid = " + id + " ";
                    SqlCommand comm = new SqlCommand(sql, conn1);
                    conn1.Open();
                    comm.ExecuteNonQuery();


                    _context.accounts.Remove(accounts);



                    conn1.Close();

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool accountsExists(int id)
        {
          return _context.accounts.Any(e => e.Id == id);
        }
    }
}
