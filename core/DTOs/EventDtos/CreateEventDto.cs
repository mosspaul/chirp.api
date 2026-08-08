namespace core.DTOs.EventDtos;

/// <summary>
/// Data transfer object for creating a new event.
/// </summary>
public class CreateEventDto
{
    /// <summary>
    /// The event title (required).
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Detailed description of the event.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Start time of the event (required, ISO 8601 format).
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// End time of the event (ISO 8601 format).
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// Physical location or virtual meeting link.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Whether this is an all-day event (default: false).
    /// </summary>
    public bool IsAllDay { get; set; } = false;

    /// <summary>
    /// Event category ID to organize events.
    /// </summary>
    public int? EventCategoryId { get; set; }

    /// <summary>
    /// Recurrence rule for repeating events.
    /// </summary>
    public CreateRecurrenceRuleDto? RecurrenceRule { get; set; }
}

/// <summary>
/// Recurrence rule data for creating repeating events.
/// </summary>
public class CreateRecurrenceRuleDto
{
    /// <summary>
    /// RRule string following RFC 5545 format (required).
    /// Examples: "FREQ=DAILY", "FREQ=WEEKLY;INTERVAL=2", "FREQ=MONTHLY;COUNT=12"
    /// </summary>
    public string RRuleString { get; set; } = null!;

    /// <summary>
    /// Start date for the recurrence.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End date for the recurrence.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Maximum number of occurrences to generate.
    /// </summary>
    public int? MaxOccurrences { get; set; }
}
