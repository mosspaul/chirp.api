using System;
using data.DbContexts;
using data.Models;
using data.Repositories.Interfaces;

namespace data.Repositories;

public class FinanceRepository : IFinanceRepository
{
    public readonly ChirpDbContext _db;
    public FinanceRepository(ChirpDbContext db)
    {
        _db = db;
    }
    public async Task UpsertConnections(List<Connection> connections)
    {
        await _db.Connections.AddRangeAsync(connections);
        await _db.SaveChangesAsync();
    }
}
