using data.Models;
using Microsoft.EntityFrameworkCore;

public class ChirpDbContext : DbContext
{
    public ChirpDbContext(DbContextOptions options) : base(options) { }
    public DbSet<AppUser> AppUsers { get; set; }
    
} 