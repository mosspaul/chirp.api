using data.Models;

namespace core.DTOs.EventDtos;

/// <summary>
/// Represents an event in the system.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Unique identifier for the event.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The event title.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Detailed description of the event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Start time of the event (ISO 8601 format).
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End time of the event (ISO 8601 format).
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Physical location or virtual meeting link for the event.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Whether this is an all-day event.
    /// </summary>
    public bool IsAllDay { get; set; }

    /// <summary>
    /// Associated category ID.
    /// </summary>
    public int? EventCategoryId { get; set; }

    /// <summary>
    /// Associated category details.
    /// </summary>
    public EventCategoryDto? Category { get; set; }

    /// <summary>
    /// Recurrence rule for repeating events.
    /// </summary>
    public RecurrenceRuleDto? RecurrenceRule { get; set; }

    /// <summary>
    /// Timestamp when the event was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp of the last update.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public EventDto() { }

    public EventDto(Event eventModel)
    {
        Id = eventModel.Id;
        Title = eventModel.Title;
        Description = eventModel.Description;
        StartTime = eventModel.StartTime;
        EndTime = eventModel.EndTime;
        Location = eventModel.Location;
        IsAllDay = eventModel.IsAllDay;
        EventCategoryId = eventModel.EventCategoryId;
        CreatedAt = eventModel.CreatedAt;
        UpdatedAt = eventModel.UpdatedAt;

        if (eventModel.Category != null)
        {
            Category = new EventCategoryDto
            {
                Id = eventModel.Category.Id,
                Title = eventModel.Category.Title,
                Color = eventModel.Category.Color,
                Description = eventModel.Category.Description
            };
        }

        if (eventModel.RecurrenceRule != null)
        {
            RecurrenceRule = new RecurrenceRuleDto
            {
                Id = eventModel.RecurrenceRule.Id,
                RRuleString = eventModel.RecurrenceRule.RRuleString,
                StartDate = eventModel.RecurrenceRule.StartDate,
                EndDate = eventModel.RecurrenceRule.EndDate,
                MaxOccurrences = eventModel.RecurrenceRule.MaxOccurrences
            };
        }
    }
}
