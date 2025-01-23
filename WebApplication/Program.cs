using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWebApp.Data;
using MyWebApp.Services;
using MyWebApp.Interfaces;
using Microsoft.AspNetCore.Builder;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ �����������
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ������������ �������
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ����������� ����������
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