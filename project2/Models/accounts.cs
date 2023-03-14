namespace project2.Models
{

    public class accounts
    {

        public int Id { get; set; }

        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string gender { get; set; }

        public bool isadmin    { get; set; }
        public bool isexpert { get; set; }

    }
}
