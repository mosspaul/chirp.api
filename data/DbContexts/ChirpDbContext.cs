using data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace data.DbContexts;
public class ChirpDbContext : IdentityDbContext<User>
{
    public ChirpDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses {get; set; }
} 