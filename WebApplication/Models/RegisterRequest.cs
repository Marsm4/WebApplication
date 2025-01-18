namespace WebApplication.Models
{
    // Models/RegisterRequest.cs
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
    }

    // Models/LoginRequest.cs
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    // Models/TokenResponse.cs
    public class TokenResponse
    {
        public string Token { get; set; }
    }

}
