using core.DTOs.FinanceDtos;

namespace core.Managers.Interfaces;

public interface ITransactionManager
{
    Task<List<TransactionDto>> GetTransactions(string userId);
}