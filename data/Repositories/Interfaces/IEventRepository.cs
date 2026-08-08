using data.Models;

namespace data.Repositories.Interfaces;

public interface IEventRepository
{
    Task<Event?> GetEventByIdAsync(int eventId, string userId);
    Task<List<Event>> GetEventsByUserAsync(string userId);
    Task<List<Event>> GetEventsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
    Task<List<Event>> GetEventsByCategoryAsync(string userId, int categoryId);
    Task<Event> CreateEventAsync(Event @event);
    Task<Event?> UpdateEventAsync(Event @event);
    Task<bool> DeleteEventAsync(int eventId, string userId);
}
