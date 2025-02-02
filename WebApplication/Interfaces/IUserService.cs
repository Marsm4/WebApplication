using Microsoft.AspNetCore.Mvc;

namespace MyWebApp.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUserAsync(RegisterUserDto userDto);  // уже есть в интерфейсе
        Task<IActionResult> LoginAsync(LoginDto loginDto);
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> GetUserByIdAsync(int userId);
        Task<IActionResult> UpdateUserAsync(int userId, UpdateUserDto userDto);
        Task<IActionResult> DeleteUserAsync(int userId);

        // Добавьте метод для регистрации пользователя
        //Task<bool> RegisterAsync(RegisterUserDto userDto);  // Добавьте сюда
    }
}
