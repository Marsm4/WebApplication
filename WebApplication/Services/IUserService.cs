using WebApplication.Models;

namespace WebApplication.Services
{
    // Services/IUserService.cs
    public interface IUserService
    {
        Task<User> RegisterAsync(string email, string name, string description, string password);
        Task<User> AuthenticateAsync(string email, string password);
        Task<string> GenerateJwtToken(User user);
    }

}
