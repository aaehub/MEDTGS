using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;


namespace project2.Models
{

 
    public class comments
    {


        public int Id { get; set; }

        
      
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string comment { get; set; }

        

        [ForeignKey("articleid")]
      //  public virtual article articleid { get; set; }

        
        public int articleid { get; set; }
        public article article { get; set; }

    }



}
