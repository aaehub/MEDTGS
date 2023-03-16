using System;
using System.Collections.Generic;
using System.Data;
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
    public class articles1Controller : Controller
    {
        private readonly project2Context _context;

        public articles1Controller(project2Context context)
        {
            _context = context;
        }

        // GET: articles1
        public async Task<IActionResult> Index()
        {
              return View(await _context.article.ToListAsync());
        }





       
        // GET: articles1/Details/5
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




            List<comments> comments = new List<comments>();


            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"L:\\project graduation\\DB\\db2.mdf\";Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "select * from comments where articleid =" + article.Id;
            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();



            SqlDataReader reader = comm.ExecuteReader();


            while (reader.Read())
            {



                comments.Add(new comments
                {

                    Id = (int)reader["Id"],
                    Date = (DateTime)reader["Date"],
                    comment = (string)reader["comment"],
                    articleid = (int)reader["articleid"],
                    article = article

                });



            }



            reader.Close();
            conn.Close();

            ViewData["test"] = comments;








            return View(article);
        }

        // GET: articles1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: articles1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,topic,category,tag,description,imagefilename")] article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: articles1/Edit/5
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment( string commenttext, int articleid)
        {


            
        

            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("project2");


           

          
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"L:\\project graduation\\DB\\db2.mdf\";Integrated Security=True;Connect Timeout=30");
            string sql;

            
            /*
             * 
            
            List<comments> comments = new List<comments>();
            comments.Add(new comments
            {

                Id = default(int),
                Date = DateTime.Now,
                comment = commenttext,

                articleid = articleid,
                article=article
            }) ;

             ViewData["test"] = comments;

            */

           

            sql = " INSERT INTO comments VALUES(  GETDATE() " + ", '"  + commenttext + "' ," + articleid + ")";

            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();


           

            comm.ExecuteNonQuery();
            comm.Dispose();

            conn.Close();




            return RedirectToAction("Details", "articles1", new { id = articleid });




        }
















        // POST: articles1/Edit/5
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

        // GET: articles1/Delete/5
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

        // POST: articles1/Delete/5
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



        public async Task<IActionResult> slider()
        {

            return View(await _context.article.ToListAsync());


        }





    }












}
