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
        private readonly ContextDb _context;

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
                Password = userDto.Password, // Храним пароль в открытом виде (небезопасно!)
                Role = "User"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User registered successfully.");
        }

        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || user.Password != loginDto.Password) // Без хеширования
                return new UnauthorizedObjectResult("Invalid credentials.");

            // Формируем приветственное сообщение
            string message = user.Role == "User"
                ? $"Привет, дорогой {user.Name}!"
                : "Login successful"; // Для админа оставляем стандартное сообщение

            return new OkObjectResult(new { Message = message, Role = user.Role });
        }


        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return new OkObjectResult(users);
        }

        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new NotFoundObjectResult("User not found.");

            return new OkObjectResult(user);
        }

        public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserDto userDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return new NotFoundObjectResult("User not found.");

            user.Name = userDto.Name ?? user.Name;
            user.Description = userDto.Description ?? user.Description;
            user.Password = userDto.Password ?? user.Password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult("User updated successfully.");
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

        public async Task<bool> RegisterAsync(RegisterUserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password, // Пароль без хеширования
                Name = userDto.Name,
                Description = userDto.Description
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }
     

    }
}
