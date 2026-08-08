using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Event
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Location { get; set; }
    public bool IsAllDay { get; set; } = false;

    public string UserId { get; set; } = null!;

    [ForeignKey("UserId")]
    public User? User { get; set; }

    public int? EventCategoryId { get; set; }

    [ForeignKey("EventCategoryId")]
    public EventCategory? Category { get; set; }

    public int? RecurrenceRuleId { get; set; }

    [ForeignKey("RecurrenceRuleId")]
    public RecurrenceRule? RecurrenceRule { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
