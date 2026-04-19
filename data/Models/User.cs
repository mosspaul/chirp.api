using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace data.Models;

public class User : IdentityUser
/// The User Model is the data representation of a Chirp user
/// login, create/delete account all impact the user model
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? SimpleFinAccessUrl { get; set; }
    public DateTime? DateCreated { get; set; }
}