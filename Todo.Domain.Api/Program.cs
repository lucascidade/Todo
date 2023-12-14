using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TodoHandler, TodoHandler>();
builder.Services.AddTransient<ITodoRepository, TodoRepository>();
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/todo-c3743";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/todo-c3743",
            ValidateAudience = true,
            ValidAudience = "todo-3743",
            ValidateLifetime = true
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
