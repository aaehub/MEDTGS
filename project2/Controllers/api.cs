using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using project2.Models;

namespace project2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class api : ControllerBase
    {


        [HttpGet("{id}")]
        public IEnumerable<article> Get(int id)
        {
            List<article> li = new List<article>();

            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("project2Context");
            SqlConnection conn1 = new SqlConnection(conStr);

            
            string sql;
            sql = "SELECT * FROM article where id CONTAINS '" + id + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                li.Add(new article
                {

                    Id = (int)reader["Id"],
                    topic = (string)reader["topic"],
                    category = (string)reader["category"],
                    tag = (string)reader["tag"],
                    description = (string)reader["imagefilename"]

                });

            }
            reader.Close();
            conn1.Close();
            return View(li);
        }

        private IEnumerable<article> View(List<article> li)
        {
            throw new NotImplementedException();
        }
    }
}
