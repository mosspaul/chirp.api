using data.DbContexts;
using data.Models;
using data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace data.Repositories;

public class EventCategoryRepository : IEventCategoryRepository
{
    private readonly ChirpDbContext _context;

    public EventCategoryRepository(ChirpDbContext context)
    {
        _context = context;
    }

    public async Task<EventCategory?> GetCategoryByIdAsync(int categoryId, string userId)
    {
        return await _context.EventCategories
            .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
    }

    public async Task<List<EventCategory>> GetCategoriesByUserAsync(string userId)
    {
        return await _context.EventCategories
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<EventCategory> CreateCategoryAsync(EventCategory category)
    {
        _context.EventCategories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<EventCategory?> UpdateCategoryAsync(EventCategory category)
    {
        _context.EventCategories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId, string userId)
    {
        var category = await GetCategoryByIdAsync(categoryId, userId);
        if (category == null)
            return false;

        _context.EventCategories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
