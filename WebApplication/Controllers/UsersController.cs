using Microsoft.AspNetCore.Mvc;
using MyWebApp.Interfaces;
using MyWebApp.Requests;

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

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterUser(RegisterUserDto userDto)
        //{
        //    var result = await _userService.RegisterAsync(userDto);
        //    if (result)
        //    {
        //        return Ok("User registered successfully.");
        //    }
        //    return BadRequest("Registration failed.");
        //}
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await _userService.GetUserByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto userDto)
        {
            return await _userService.UpdateUserAsync(id, userDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return await _userService.DeleteUserAsync(id);
        }


    }
}