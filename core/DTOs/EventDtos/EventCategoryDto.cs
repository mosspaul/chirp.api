namespace core.DTOs.EventDtos;

/// <summary>
/// Represents an event category for organizing events.
/// </summary>
public class EventCategoryDto
{
    /// <summary>
    /// Unique identifier for the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Category name.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Hex color code for UI representation (e.g., #FF5733).
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Description of the category's purpose.
    /// </summary>
    public string? Description { get; set; }
}
