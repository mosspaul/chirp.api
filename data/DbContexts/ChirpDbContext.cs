using data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace data.DbContexts;
public class ChirpDbContext : IdentityDbContext<User>
{
    public ChirpDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Connection> Connections {get; set; }
    public DbSet<Account> Accounts {get; set; }
    public DbSet<Transaction> Transactions {get; set; }
    public DbSet<Holding> Holdings {get; set; }
    public DbSet<Setting> Settings {get;set;}
    public DbSet<Event> Events {get; set; }
    public DbSet<EventCategory> EventCategories {get; set; }
    public DbSet<RecurrenceRule> RecurrenceRules {get; set; }
} 