using Microsoft.AspNetCore.Mvc;
using MyWebApp.Requests;

namespace MyWebApp.Interfaces
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUserAsync(RegisterUserDto userDto);
        Task<IActionResult> LoginAsync(LoginDto loginDto);
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> DeleteUserAsync(int userId);
    }
}
