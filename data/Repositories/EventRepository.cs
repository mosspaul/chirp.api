using data.DbContexts;
using data.Models;
using data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace data.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ChirpDbContext _context;

    public EventRepository(ChirpDbContext context)
    {
        _context = context;
    }

    public async Task<Event?> GetEventByIdAsync(int eventId, string userId)
    {
        return await _context.Events
            .Include(e => e.Category)
            .Include(e => e.RecurrenceRule)
            .FirstOrDefaultAsync(e => e.Id == eventId && e.UserId == userId);
    }

    public async Task<List<Event>> GetEventsByUserAsync(string userId)
    {
        return await _context.Events
            .Where(e => e.UserId == userId)
            .Include(e => e.Category)
            .Include(e => e.RecurrenceRule)
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
    {
        return await _context.Events
            .Where(e => e.UserId == userId && e.StartTime >= startDate && e.StartTime <= endDate)
            .Include(e => e.Category)
            .Include(e => e.RecurrenceRule)
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<List<Event>> GetEventsByCategoryAsync(string userId, int categoryId)
    {
        return await _context.Events
            .Where(e => e.UserId == userId && e.EventCategoryId == categoryId)
            .Include(e => e.Category)
            .Include(e => e.RecurrenceRule)
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<Event> CreateEventAsync(Event @event)
    {
        _context.Events.Add(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<Event?> UpdateEventAsync(Event @event)
    {
        @event.UpdatedAt = DateTime.UtcNow;
        _context.Events.Update(@event);
        await _context.SaveChangesAsync();
        return @event;
    }

    public async Task<bool> DeleteEventAsync(int eventId, string userId)
    {
        var @event = await GetEventByIdAsync(eventId, userId);
        if (@event == null)
            return false;

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();
        return true;
    }
}
