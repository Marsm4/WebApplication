using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Interfaces;
using MyWebApp.Models;
using MyWebApp.Requests;

namespace MyWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly ContextDb _context; // Используем ContextDb, а не DbContext

        public UserService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> RegisterUserAsync(RegisterUserDto userDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                return new BadRequestObjectResult("Email already exists.");

            var user = new User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Description = userDto.Description,
                Password = userDto.Password, // Note: Hash the password in production
                Role = "User"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User registered successfully.");
        }

        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user == null)
                return new UnauthorizedObjectResult("Invalid credentials.");

            return new OkObjectResult(new { Message = "Login successful", Role = user.Role });
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return new OkObjectResult(users);
        }

        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new NotFoundObjectResult("User not found.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User deleted successfully.");
        }
    }
}
