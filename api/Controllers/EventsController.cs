using System.Security.Claims;
using core.DTOs.EventDtos;
using core.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// Manages events and event categories for authenticated users.
/// All endpoints require JWT authentication.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EventsController : ControllerBase
{
    private readonly IEventManager _eventManager;
    private readonly IEventCategoryManager _categoryManager;

    public EventsController(IEventManager eventManager, IEventCategoryManager categoryManager)
    {
        _eventManager = eventManager;
        _categoryManager = categoryManager;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("User not found");
    }

    /// <summary>
    /// Retrieves a specific event by ID.
    /// </summary>
    /// <param name="id">The event ID</param>
    /// <returns>The event details or 404 if not found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetEvent(int id)
    {
        var userId = GetUserId();
        var @event = await _eventManager.GetEventAsync(id, userId);

        if (@event == null)
            return NotFound();

        return Ok(@event);
    }

    /// <summary>
    /// Retrieves all events for the authenticated user.
    /// </summary>
    /// <returns>List of all user events sorted by start time</returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllEvents()
    {
        var userId = GetUserId();
        var events = await _eventManager.GetEventsAsync(userId);
        return Ok(events);
    }

    /// <summary>
    /// Retrieves events within a date range.
    /// </summary>
    /// <param name="startDate">Start date for the range (ISO 8601 format)</param>
    /// <param name="endDate">End date for the range (ISO 8601 format)</param>
    /// <returns>List of events within the specified date range</returns>
    [HttpGet("date-range")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetEventsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var userId = GetUserId();
        var events = await _eventManager.GetEventsByDateRangeAsync(userId, startDate, endDate);
        return Ok(events);
    }

    /// <summary>
    /// Retrieves all events in a specific category.
    /// </summary>
    /// <param name="categoryId">The event category ID</param>
    /// <returns>List of events in the category</returns>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetEventsByCategory(int categoryId)
    {
        var userId = GetUserId();
        var events = await _eventManager.GetEventsByCategoryAsync(userId, categoryId);
        return Ok(events);
    }

    /// <summary>
    /// Creates a new event with optional recurring rules.
    /// </summary>
    /// <param name="createEventDto">Event details including optional RRule for recurrence</param>
    /// <returns>The created event with its ID</returns>
    /// <remarks>
    /// RRule must follow RFC 5545 format. Examples:
    /// - "FREQ=DAILY" for daily recurrence
    /// - "FREQ=WEEKLY;INTERVAL=2" for every 2 weeks
    /// - "FREQ=MONTHLY;COUNT=12" for 12 monthly occurrences
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto createEventDto)
    {
        try
        {
            var userId = GetUserId();
            var @event = await _eventManager.CreateEventAsync(createEventDto, userId);
            return CreatedAtAction(nameof(GetEvent), new { id = @event.Id }, @event);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="id">The event ID to update</param>
    /// <param name="updateEventDto">Fields to update (partial updates supported)</param>
    /// <returns>The updated event</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
    {
        try
        {
            var userId = GetUserId();
            var @event = await _eventManager.UpdateEventAsync(id, updateEventDto, userId);

            if (@event == null)
                return NotFound();

            return Ok(@event);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Deletes an event.
    /// </summary>
    /// <param name="id">The event ID to delete</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var userId = GetUserId();
        var success = await _eventManager.DeleteEventAsync(id, userId);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Retrieves all event categories for the authenticated user.
    /// </summary>
    /// <returns>List of event categories</returns>
    [HttpGet("categories/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventCategoryDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCategories()
    {
        var userId = GetUserId();
        var categories = await _categoryManager.GetCategoriesAsync(userId);
        return Ok(categories);
    }

    /// <summary>
    /// Retrieves a specific event category.
    /// </summary>
    /// <param name="id">The category ID</param>
    /// <returns>The category details or 404 if not found</returns>
    [HttpGet("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventCategoryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCategory(int id)
    {
        var userId = GetUserId();
        var category = await _categoryManager.GetCategoryAsync(id, userId);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    /// <summary>
    /// Creates a new event category.
    /// </summary>
    /// <param name="categoryDto">Category details (title, color, description)</param>
    /// <returns>The created category with its ID</returns>
    [HttpPost("categories")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EventCategoryDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateCategory([FromBody] EventCategoryDto categoryDto)
    {
        var userId = GetUserId();
        var category = await _categoryManager.CreateCategoryAsync(categoryDto, userId);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    /// <summary>
    /// Updates an existing event category.
    /// </summary>
    /// <param name="id">The category ID to update</param>
    /// <param name="categoryDto">Updated category details</param>
    /// <returns>The updated category</returns>
    [HttpPut("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventCategoryDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] EventCategoryDto categoryDto)
    {
        var userId = GetUserId();
        var category = await _categoryManager.UpdateCategoryAsync(id, categoryDto, userId);

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    /// <summary>
    /// Deletes an event category.
    /// </summary>
    /// <param name="id">The category ID to delete</param>
    /// <returns>No content on success</returns>
    [HttpDelete("categories/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var userId = GetUserId();
        var success = await _categoryManager.DeleteCategoryAsync(id, userId);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
