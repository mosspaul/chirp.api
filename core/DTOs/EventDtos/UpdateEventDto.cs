namespace core.DTOs.EventDtos;

/// <summary>
/// Data transfer object for updating an existing event.
/// All fields are optional for partial updates.
/// </summary>
public class UpdateEventDto
{
    /// <summary>
    /// Updated event title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Updated description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Updated start time (ISO 8601 format).
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// Updated end time (ISO 8601 format).
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Updated location or meeting link.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Updated all-day status.
    /// </summary>
    public bool? IsAllDay { get; set; }

    /// <summary>
    /// Updated category ID.
    /// </summary>
    public int? EventCategoryId { get; set; }

    /// <summary>
    /// Updated recurrence rule settings.
    /// </summary>
    public UpdateRecurrenceRuleDto? RecurrenceRule { get; set; }
}

/// <summary>
/// Recurrence rule updates for existing events.
/// </summary>
public class UpdateRecurrenceRuleDto
{
    /// <summary>
    /// Updated RRule string following RFC 5545 format.
    /// </summary>
    public string? RRuleString { get; set; }

    /// <summary>
    /// Updated start date for recurrence.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Updated end date for recurrence.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Updated maximum occurrence count.
    /// </summary>
    public int? MaxOccurrences { get; set; }
}
