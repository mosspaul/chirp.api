using data.Models;

namespace data.Repositories.Interfaces;

public interface IEventCategoryRepository
{
    Task<EventCategory?> GetCategoryByIdAsync(int categoryId, string userId);
    Task<List<EventCategory>> GetCategoriesByUserAsync(string userId);
    Task<EventCategory> CreateCategoryAsync(EventCategory category);
    Task<EventCategory?> UpdateCategoryAsync(EventCategory category);
    Task<bool> DeleteCategoryAsync(int categoryId, string userId);
}
