using System;
using data.Models;

namespace core.DTOs.FinanceDtos;

public class AccountDto
{
    public AccountDto(Account account)
    {
        Id = account.Id;
        Name = account.Name;
        Currency = account.Currency;
        Balance = account.Balance;
        AvailableBalance = account.AvailableBalance;
        BalanceDate = account.BalanceDate;
        Holdings = account.Holdings.Select(h => new HoldingDto(h)).ToList();
        Transactions = account.Transactions.Select(t => new TransactionDto(t)).ToList();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Currency { get; set; }
    public double Balance { get; set; }
    public double AvailableBalance { get; set; }
    public DateTime BalanceDate { get; set; }
    public List<HoldingDto> Holdings { get; set; }
    public List<TransactionDto> Transactions { get; set; }
}
