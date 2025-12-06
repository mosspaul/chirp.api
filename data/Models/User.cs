using System.ComponentModel.DataAnnotations;

namespace data.Models;
public class AppUser
{
    [Key]
    public Guid Uuid { get; set; }
}