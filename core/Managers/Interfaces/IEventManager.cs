using core.DTOs.EventDtos;

namespace core.Managers.Interfaces;

public interface IEventManager
{
    Task<EventDto?> GetEventAsync(int eventId, string userId);
    Task<List<EventDto>> GetEventsAsync(string userId);
    Task<List<EventDto>> GetEventsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
    Task<List<EventDto>> GetEventsByCategoryAsync(string userId, int categoryId);
    Task<EventDto> CreateEventAsync(CreateEventDto createEventDto, string userId);
    Task<EventDto?> UpdateEventAsync(int eventId, UpdateEventDto updateEventDto, string userId);
    Task<bool> DeleteEventAsync(int eventId, string userId);
}
