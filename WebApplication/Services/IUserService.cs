using WebApplication.Models;

namespace WebApplication.Services
{
    // Services/IUserService.cs
    
    public interface IUserService
    {
        Task<User> RegisterAsync(string email, string name, string description, string password, string role = "User");
        Task<User> AuthenticateAsync(string email, string password);
        Task<string> GenerateJwtToken(User user);
        Task<List<User>> GetAllUsersAsync();  // Добавляем этот метод
    }


}
