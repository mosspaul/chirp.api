using System;
using data.Models;

namespace core.DTOs.FinanceDtos;

public class TransactionDto
{
    public TransactionDto(Transaction transaction)
    {
        Id = transaction.Id;
        Posted = transaction.Posted;
        TransactedAt = transaction.TransactedAt;
        Description = transaction.Description;
        Memo = transaction.Memo;
        Payee = transaction.Payee;
        Amount = transaction.Amount;
    }
    public int Id {get; set;}
    public DateTime Posted {get;set;}
    public DateTime TransactedAt {get;set;}
    public string? Description {get;set;}
    public string? Memo {get;set;}
    public string? Payee {get;set;}
    public double Amount {get;set;}
}
