namespace WebApplication.Models
{
    // Models/User.cs
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }  // "Admin" or "User"
    }

}
