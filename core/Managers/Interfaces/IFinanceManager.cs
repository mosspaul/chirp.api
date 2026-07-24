using core.DTOs.FinanceDtos;

namespace core.Managers.Interfaces;

public interface IFinanceManager
{
    Task<List<ConnectionDto>> GetConnections(string userId);
}