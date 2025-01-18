﻿
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
namespace WebApplication.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

}
