namespace core.DTOs.EventDtos;

/// <summary>
/// Represents a recurrence rule for repeating events.
/// Follows RFC 5545 iCalendar specification.
/// </summary>
public class RecurrenceRuleDto
{
    /// <summary>
    /// Unique identifier for this recurrence rule.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// RRule string in RFC 5545 format.
    /// Examples:
    /// - "FREQ=DAILY" - repeats every day
    /// - "FREQ=WEEKLY" - repeats every week
    /// - "FREQ=WEEKLY;BYDAY=MO,WE,FR" - repeats on specific weekdays
    /// - "FREQ=MONTHLY" - repeats every month
    /// - "FREQ=YEARLY" - repeats every year
    /// - "FREQ=DAILY;INTERVAL=2" - repeats every 2 days
    /// - "FREQ=MONTHLY;COUNT=12" - repeats 12 times
    /// - "FREQ=WEEKLY;UNTIL=20261231" - repeats until end date
    /// </summary>
    public string RRuleString { get; set; } = null!;

    /// <summary>
    /// Start date for the recurrence range (ISO 8601 format).
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End date for the recurrence range (ISO 8601 format).
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Maximum number of occurrences to generate.
    /// </summary>
    public int? MaxOccurrences { get; set; }
}
