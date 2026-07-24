using core.DTOs.FinanceDtos;
using data.Models;
using data.Repositories.Interfaces;
using core.Managers.Interfaces;

namespace Managers;
public class TransactionManager : ITransactionManager
{
    private readonly IFinanceRepository _financeRepo;

    public TransactionManager(IFinanceRepository financeRepo)
    {
        _financeRepo = financeRepo;
    }
    public async Task<List<TransactionDto>> GetTransactions(string userId)
    {

        var transactions = await _financeRepo.GetTransactionsForUser(userId);
        return transactions.Select(transaction => new TransactionDto(transaction)).ToList();
    }
}