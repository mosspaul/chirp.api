using core.Managers;
using core.Managers.Interfaces;
using core.Mappers;
using core.Mappers.Interfaces;
using data.DbContexts;
using data.Repositories;
using data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var host = Environment.GetEnvironmentVariable("DB_HOST");
var db = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString =
    $"Host={host};Port=5432;Database={db};Username={user};Password={pass}";

builder.Services.AddControllers();

// Add Core layer -> make function later
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IDtoToModelMapper, DtoToModelMapper>();

// Add Data layer -> make function later
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<ChirpDbContext>(opt =>
    opt.UseNpgsql(connectionString));

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

app.MapControllers();
app.Run();