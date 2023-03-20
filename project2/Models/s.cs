using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace project2.Models
{
    public class s
    {
        public int Id { get; set; }



        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string comment { get; set; }




        //  public virtual article articleid { get; set; }

        [ForeignKey("articleid")]

        [Display(Name = "article")]
        public int? articleid { get; set; }
        public int? accountid { get; set; }

        public virtual article article { get; set; }

        public string username { get; internal set; }
    }
}
