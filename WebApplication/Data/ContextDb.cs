
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using MyWebApp.Models;
namespace MyWebApp.Data
{

    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }


}
