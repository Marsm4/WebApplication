using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    // Models/User.cs
    public class Users
    {
        [Key]
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool isAdmin { get; set; }
    }
}
