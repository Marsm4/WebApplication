using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return; // База данных уже заполнена
                }

                // Создаем администратора
                context.Users.Add(new User
                {
                    Email = "admin@example.com",
                    Name = "Admin",
                    Description = "Administrator",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin"
                });

                context.SaveChanges();
            }
        }
    }
}