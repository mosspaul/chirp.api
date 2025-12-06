using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ChirpDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("ChirpDbConnectionString")));

var app = builder.Build();
app.MapControllers();
app.Run();