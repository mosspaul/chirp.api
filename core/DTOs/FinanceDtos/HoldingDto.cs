using System;
using data.Models;

namespace core.DTOs.FinanceDtos;

public class HoldingDto
{
    public HoldingDto(Holding holding)
    {
        Id = holding.Id;
        Shares = holding.Shares;
        CostBasis = holding.CostBasis;
        PurchasePrice = holding.PurchasePrice;
        MarketValue = holding.MarketValue;
        Description = holding.Description;
        Currency = holding.Currency;
        Symbol = holding.Symbol;
        CreatedAt = holding.CreatedAt;
    }
    public int Id {get; set;}
    public double Shares {get;set;}
    public double CostBasis {get;set;}
    public double PurchasePrice {get;set;}
    public double MarketValue {get;set;}
    public string? Description {get;set;}
    public string? Currency {get;set;}
    public string? Symbol {get; set;}
    public DateTime CreatedAt {get;set;}
}
