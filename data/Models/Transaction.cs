using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public int AccountId {get;set;}
    public Account Account {get;set;}
    public DateTime Posted {get;set;}
    public DateTime TransactedAt {get;set;}
    public string? Description {get;set;}
    public string? Memo {get;set;}
    public string? Payee {get;set;}
    public double Amount {get;set;}
}
