using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public int ConnectionId {get;set;}
    public Connection? Connection {get;set;}
    public string Name {get;set;}
    public string Currency {get;set;}
    public double Balance {get;set;}
    public double AvailableBalance {get;set;}
    public string SimpleFinConnId {get;set;}

    public ICollection<Holding> Holdings { get; set; } = new List<Holding>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
