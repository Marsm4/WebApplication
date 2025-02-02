using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Data;
using MyWebApp.Interfaces;
using MyWebApp.Services;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Настройка строки подключения к БД
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Регистрация сервисов
builder.Services.AddScoped<IUserService, UserService>();

// Добавление CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Настройка JWT-аутентификации
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Строим приложение
var app = builder.Build();

// Настройка обработки ошибок
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();  // Swagger работает в режиме разработки
    app.UseSwaggerUI();  // Добавление UI для Swagger
}
else
{
    app.UseExceptionHandler("/error");
}

// Настройка культуры по умолчанию
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

// Middleware
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Маршрутизация
app.MapControllers();

// Запуск приложения
app.Run();


// ErrorController (с обработчиком ошибок)
[ApiController]
[Route("error")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public IActionResult HandleError()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        return Problem(
            title: "Internal Server Error",
            detail: exception?.Message ?? "An unexpected error occurred.",
            statusCode: StatusCodes.Status500InternalServerError
        );
    }
}
