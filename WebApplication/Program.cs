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
            // ��������� �������� ��
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

            // ��������� �������
            services.AddScoped<IUserService, UserService>();

            // ��������� �������������� � �������������� JWT
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

            // ��������� �����������
            services.AddAuthorization();

            // ��������� �����������
            services.AddControllers();
        });

        webBuilder.Configure(app =>
        {
            // �������� ������ � ���������� � �����
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            // ��������� �������� � ��������� ���� ������ ���������� �������
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                SeedData.Initialize(services);
            }

            // ��������� ��������� ������ � ����������� �� ���������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // �������� �������������� � �����������
            app.UseAuthentication();
            app.UseAuthorization();

            // ���������� ��������� ��� ������������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ����� ������������ MapControllers �� IEndpointRouteBuilder
            });
        });
    })
    .Build();

// ������ ����������
host.Run();
