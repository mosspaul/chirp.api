using System;
using data.DbContexts;
using data.Models;
using data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace data.Repositories;

public class FinanceRepository : IFinanceRepository
{
    public readonly ChirpDbContext _db;
    public FinanceRepository(ChirpDbContext db)
    {
        _db = db;
    }

    public async Task<List<Connection>> GetConnectionsForUser(string userId)
    {
        var query =  _db.Connections
        .Where(conn => conn.UserId == userId)
        .Include(conn => conn.Accounts)
            .ThenInclude(acc => acc.Holdings)
        .Include(conn => conn.Accounts)
            .ThenInclude(acc => acc.Transactions)
        .AsSplitQuery();
        
        return await query.ToListAsync();
    }

    public async Task UpsertConnections(List<Connection> connections, CancellationToken ct)
{
    foreach (var incoming in connections)
    {
        var existing = await _db.Connections
            .Include(c => c.Accounts)
                .ThenInclude(a => a.Holdings)
            .Include(c => c.Accounts)
                .ThenInclude(a => a.Transactions)
            .FirstOrDefaultAsync(c => c.SimpleFinConnId == incoming.SimpleFinConnId, ct);

        if (existing is null)
        {
            _db.Connections.Add(incoming);
        }
        else
        {
            existing.Name = incoming.Name;
            existing.Url = incoming.Url;

            foreach (var incomingAccount in incoming.Accounts)
            {
                var existingAccount = existing.Accounts
                    .FirstOrDefault(a => a.SFinId == incomingAccount.SFinId);

                if (existingAccount is null)
                {
                    existing.Accounts.Add(incomingAccount);
                }
                else
                {
                    existingAccount.Name = incomingAccount.Name;
                    existingAccount.Balance = incomingAccount.Balance;
                    existingAccount.AvailableBalance = incomingAccount.AvailableBalance;
                    existingAccount.BalanceDate = incomingAccount.BalanceDate;
                    existingAccount.Currency = incomingAccount.Currency;

                    foreach (var incomingHolding in incomingAccount.Holdings)
                    {
                        var existingHolding = existingAccount.Holdings
                            .FirstOrDefault(h => h.SFinId == incomingHolding.SFinId);

                        if (existingHolding is null)
                            existingAccount.Holdings.Add(incomingHolding);
                        else
                        {
                            existingHolding.Shares = incomingHolding.Shares;
                            existingHolding.MarketValue = incomingHolding.MarketValue;
                            existingHolding.CostBasis = incomingHolding.CostBasis;
                            existingHolding.PurchasePrice = incomingHolding.PurchasePrice;
                            existingHolding.Symbol = incomingHolding.Symbol;
                            existingHolding.Description = incomingHolding.Description;
                        }
                    }

                    foreach (var incomingTx in incomingAccount.Transactions)
                    {
                        var existingTx = existingAccount.Transactions
                            .FirstOrDefault(t => t.SFinId == incomingTx.SFinId);

                        if (existingTx is null)
                            existingAccount.Transactions.Add(incomingTx);
                        else
                        {
                            existingTx.Posted = incomingTx.Posted;
                            existingTx.TransactedAt = incomingTx.TransactedAt;
                            existingTx.Amount = incomingTx.Amount;
                            existingTx.Description = incomingTx.Description;
                            existingTx.Memo = incomingTx.Memo;
                            existingTx.Payee = incomingTx.Payee;
                        }
                    }
                }
            }
        }
    }

    await _db.SaveChangesAsync(ct);
}
}
