using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApp.Interfaces;
using MyWebApp.Models;
using MyWebApp.Requests;
using MyWebApp.Services;

namespace MyWebApp.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto userDto)
        {
            return await _userService.RegisterUserAsync(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return await _userService.LoginAsync(loginDto);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }
    }
}