using Microsoft.EntityFrameworkCore;
using SKTest.Db.Repositories;
using SKTest.Infrastructure.Data;
using SKTest.Infrastructure.Repositories;
using SKTest.Server.Interfaces;
using SKTest.Server.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CodeTestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CodeTestContext>();
    context.Database.EnsureCreated();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
