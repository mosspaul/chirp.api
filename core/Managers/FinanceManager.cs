using core.DTOs.FinanceDtos;
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
    public async Task<List<ConnectionDto>> GetConnections(string userId)
    {
        var connections = await _financeRepo.GetConnectionsForUser(userId);
        return connections.Select(c => new ConnectionDto(c)).ToList();
    }
}