using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class EventCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Title { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; } = null!;

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
