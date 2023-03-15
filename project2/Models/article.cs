using Microsoft.Extensions.Hosting;

namespace project2.Models
{
    public class article
    {
 
        public int Id { get; set; }

        public string topic { get; set; }
        public string category { get; set; }
        public string tag { get; set; }
        public string description { get; set; }

        public string imagefilename { get; set; }

      //  public virtual ICollection<comments> comments { get; set; }


    }
}
