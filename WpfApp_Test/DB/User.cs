using System.ComponentModel.DataAnnotations;

namespace WpfApp_Test.DB
{
    public class User
    {
        [Key]
        public int UserId { get;set; }

        public string Name { get;set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string ImagePath { get; set; }

        public string Status { get; set; }
    }
}