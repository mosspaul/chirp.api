using data.Models;
using data.Repositories.Interfaces;
using Managers.Interfaces;

namespace Managers;
public class FinanceManager : IFinanceManager
{
    private readonly IFinanceRepository _financeRepo;

    public FinanceManager(IFinanceRepository financeRepo)
    {
        _financeRepo = financeRepo;
    }
    public async Task<List<Connection>> GetConnections(string userId)
    {
        return await _financeRepo.GetConnectionsForUser(userId);
    }
}