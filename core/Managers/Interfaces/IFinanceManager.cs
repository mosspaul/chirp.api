using core.DTOs.FinanceDtos;

namespace Managers.Interfaces;

public interface IFinanceManager
{
    Task<List<ConnectionDto>> GetConnections(string userId);
}