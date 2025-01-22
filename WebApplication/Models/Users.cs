using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    // Models/User.cs
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "User"; // Default role is "User"
    }
}
