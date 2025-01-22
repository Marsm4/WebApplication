using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    // Models/RegisterRequest.cs
    public class Logins
    {
        [Key]
        public int id_Login { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int User_id { get; set; }
        public Users Users { get; set; }
    }

}
