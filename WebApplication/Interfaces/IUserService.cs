using Microsoft.AspNetCore.Mvc;
using MyWebApp.Requests;

namespace MyWebApp.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUserAsync(RegisterUserDto userDto);
        Task<IActionResult> LoginAsync(LoginDto loginDto);
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> GetUserByIdAsync(int userId);
        Task<IActionResult> UpdateUserAsync(int userId, UpdateUserDto userDto);
        Task<IActionResult> DeleteUserAsync(int userId);
    }
}