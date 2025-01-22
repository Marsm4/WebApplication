using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyWebApp.Data;
using MyWebApp.Services;
using MyWebApp.Interfaces;

using Microsoft.AspNetCore.Builder;
using static MyWebApp.Data.ContextDb;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавляем DbContext
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Регистрируем сервисы
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Настраиваем приложение
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();