using System;
using data.Models;

namespace core.DTOs.FinanceDtos;

public class ConnectionDto
{
    public ConnectionDto(Connection connection)
    {
        Id = connection.Id;
        UserId = connection.UserId;
        Name = connection.Name;
        Url = connection.Url;
        Accounts = connection.Accounts.Select(c => new AccountDto(c)).ToList();
    }
    public int Id {get; set;}
    public string UserId {get;set;}
    public string Name {get;set;}
    public string Url {get;set;}
    public List<AccountDto> Accounts { get; set; } 
}
