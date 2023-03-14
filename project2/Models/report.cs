using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace project2.Models
{
    public class report
    {
        public int Id { get; set; }

        public string title { get; set; }

   [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }


        public string content { get; set; }


     
        public bool isSolved { get; set; }
    }
}
