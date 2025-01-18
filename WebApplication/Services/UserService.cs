using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication.Data;
using WebApplication.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
namespace WebApplication.Services
{
    // Services/UserService.cs
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(string email, string name, string description, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
                throw new Exception("Email is already taken.");

            var user = new User
            {
                Email = email,
                Name = name,
                Description = description,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
