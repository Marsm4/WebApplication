
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using WebApplication.Models;
namespace WebApplication.Data
{

    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Logins> Logins { get; set; }
    }

}
