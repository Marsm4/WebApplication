using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication.Data;
using WebApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices((context, services) =>
        {
            // Добавляем контекст БД
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            // Добавляем сервисы
            services.AddScoped<IUserService, UserService>();

            // Настройка аутентификации с использованием JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = context.Configuration["Jwt:Issuer"],
                        ValidAudience = context.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.Configuration["Jwt:SecretKey"]))
                    };
                });

            // Настройка авторизации
            services.AddAuthorization();

            // Добавляем контроллеры
            services.AddControllers();
        });

        webBuilder.Configure(app =>
        {
            // Получаем доступ к информации о среде
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            // Применяем миграции и заполняем базу данных начальными данными
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                SeedData.Initialize(services);
            }

            // Настройка обработки ошибок в зависимости от окружения
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Включаем аутентификацию и авторизацию
            app.UseAuthentication();
            app.UseAuthorization();

            // Используем эндпоинты для контроллеров
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Здесь используется MapControllers на IEndpointRouteBuilder
            });
        });
    })
    .Build();

// Запуск приложения
host.Run();
