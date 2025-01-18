using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication.Data;
using WebApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы в контейнер
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService>();

// Настройка аутентификации с использованием JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

// Применяем миграции и заполняем базу данных начальными данными
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    SeedData.Initialize(services);
}

// Настройка обработки ошибок в зависимости от окружения
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Включаем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов для контроллеров
app.MapControllers();

app.Run();