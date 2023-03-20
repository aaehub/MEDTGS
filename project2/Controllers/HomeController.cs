using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using project2.Models;
using System.Diagnostics;

namespace project2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> adminhome()
        {
            



            string ss = HttpContext.Session.GetString("role");
            if (ss == "admin" || ss == "expert")
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

     
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> customerhome()
        {
            


            List<article> li = new List<article>();

            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"L:\\project graduation\\DB\\db2.mdf\";Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "select * from article order by Id";
            SqlCommand comm = new SqlCommand(sql, conn);

            conn.Open();



            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new article
                {

                    Id = (int)reader["Id"],
                    topic = (string)reader["topic"],
                    category = (string)reader["category"],
                    tag = (string)reader["tag"],
                    description = (string)reader["description"],
                    imagefilename = (string)reader["imagefilename"]

                });
            }
            reader.Close();
            conn.Close();



            return View(li);

        }
       

        public IActionResult logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Cookies.Delete("username");
            HttpContext.Response.Cookies.Delete("role");
           
            return RedirectToAction("login", "home");

        }

        public IActionResult login()
        {
            return View();
        }

        //public IActionResult adminhome()
        //{
        //    ViewData["name"] = HttpContext.Session.GetString("name");



        //    string ss = HttpContext.Session.GetString("role");
        //    if (ss == "admin")
        //    {



        //        return View();
        //    }


        //    else
        //        return RedirectToAction("login", "home");


        //}
        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa, bool auto)
        {

            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("project2");


            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"L:\\project graduation\\DB\\db2.mdf\";Integrated Security=True;Connect Timeout=30");
            string sql;
            sql = "SELECT * FROM accounts where username ='" + na + "' and  password ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string id = Convert.ToString((int)reader["Id"]);
                string na1 = (string)reader["username"];

                string ro = (string)reader["role"];


                HttpContext.Session.SetString("Id", id);
                HttpContext.Session.SetString("name", na1);
                HttpContext.Session.SetString("role", ro);

                reader.Close();
                conn1.Close();




                if (auto == true)
                {
                    HttpContext.Response.Cookies.Append("username", na1);
                    HttpContext.Response.Cookies.Append("Id", id);
                    HttpContext.Response.Cookies.Append("role", ro);
                }

                if (ro == "customer")
                {

                    return RedirectToAction("customerhome", "Home");
                }   
                else
                {
                    return RedirectToAction("adminhome", "Home");
                }






            }
            else
            {
                ViewData["Message"] = "wrong user name and password";



            }

            return View();

        }








    }
}


    