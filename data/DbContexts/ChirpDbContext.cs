using data.Models;
using Microsoft.EntityFrameworkCore;

namespace data.DbContexts;
public class ChirpDbContext : DbContext
{
    public ChirpDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses {get; set; }
} 