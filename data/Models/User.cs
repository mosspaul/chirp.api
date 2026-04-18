using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;
public class User
/// The User Model is the data representation of a MossFlash user
/// login, create/delete account all impact the user model
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public DateTime? DateCreated { get; set; }
    public string? Email { get; set; }
}