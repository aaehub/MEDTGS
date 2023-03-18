using Microsoft.EntityFrameworkCore;
using project2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace project2.Models
{

    public class accounts
    {

        public int Id { get; set; }

        public string username { get; set; }
        public string email { get; set; }   
    
        public string password { get; set; }
        public string gender { get; set; }

        public string role { get; set; }

       public DateTime Date { get; set; }
  


    }
}
