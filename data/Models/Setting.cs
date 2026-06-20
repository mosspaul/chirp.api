using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using data.Models;

public class Setting {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public bool DarkMode {get; set;}
    public int UserId {get;set;}
    public User? User {get;set;}
}