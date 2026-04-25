using System.Text;
using core.Gateways;
using core.Managers;
using core.Managers.Interfaces;
using core.Mappers;
using core.Mappers.Interfaces;
using data.DbContexts;
using data.Models;
using data.Repositories;
using data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


var host = Environment.GetEnvironmentVariable("DB_HOST");
var db = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString =
    $"Host={host};Port=5432;Database={db};Username={user};Password={pass}";

builder.Services.AddControllers();

// Add Core layer -> make function later
builder.Services.AddScoped<IUserAccountManager, UserAccountManager>();
builder.Services.AddScoped<IDtoToModelMapper, DtoToModelMapper>();
builder.Services.AddHttpClient<SimpleFinBridgeGateway>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

// Add Data layer -> make function later
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddDbContext<ChirpDbContext>(opt =>
    opt.UseNpgsql(connectionString));

builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ChirpDbContext>();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") 
    ?? builder.Configuration["Jwt:Secret"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<ChirpDbContext>();

    for (int i = 0; i < 10; i++)
    {
        try
        {
            database.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
	    Console.WriteLine($"{ex.Message}");
            Console.WriteLine("DB not ready yet...");
            Thread.Sleep(5000);
        }
    }
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();