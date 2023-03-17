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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
            [HttpPost]
            [ValidateAntiForgeryToken]
       
            
            public async Task<IActionResult> register()
            {
                
                return View();
            }






          


     

        public async Task<IActionResult> login()
        {





            return View();


        }
    }

    }