using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Interfaces;
using WebApplication.Models;
using WebApplication.Requests;

namespace WebApplication.Services
{
    public class UserLoginService : IUsersLoginsService
    {
        private readonly ContextDb _context;

        public UserLoginService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return new OkObjectResult(new
            {
                data = new { users = users },
                status = true
            });
        }

        public async Task<IActionResult> CreateNewUserAndLoginAsync(CreateNewUserAndLogin newUser)
        {
            var existinglogins = await _context.Logins.ToListAsync();
            if (existinglogins.Any(x => x.Login.ToLower() == newUser.Login.ToLower()))
            {
                return new OkObjectResult(new { status = false, message = "Пользователь с таким логином уже существует" });
            }

            var user = new Users()
            {
                Name = newUser.Name,
                Description = newUser.Description,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var login = new Logins()
            {
                User_id = user.id_User,
                Login = newUser.Login,
                Password = newUser.Password,
            };

            await _context.Logins.AddAsync(login);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "none"
            });
        }
    }
}
