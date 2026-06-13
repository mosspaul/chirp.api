using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Holding
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public int AccountId {get;set;}
    public Account Account {get;set;}
    public double Shares {get;set;}
    public string SFinId {get;set;}
    public double CostBasis {get;set;}
    public double PurchasePrice {get;set;}
    public double MarketValue {get;set;}
    public string? Description {get;set;}
    public string? Currency {get;set;}
    public string? Symbol {get; set;}
    public DateTime CreatedAt {get;set;}
}
