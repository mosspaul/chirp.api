using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Connection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public string UserId {get;set;}
    public User User {get;set;}
    public string Name {get;set;}
    public string MxId {get;set;}
    public string SimpleFinConnId {get;set;}
    public string Url {get;set;}

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}
