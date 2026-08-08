using core.DTOs.EventDtos;

namespace core.Managers.Interfaces;

public interface IEventCategoryManager
{
    Task<EventCategoryDto?> GetCategoryAsync(int categoryId, string userId);
    Task<List<EventCategoryDto>> GetCategoriesAsync(string userId);
    Task<EventCategoryDto> CreateCategoryAsync(EventCategoryDto categoryDto, string userId);
    Task<EventCategoryDto?> UpdateCategoryAsync(int categoryId, EventCategoryDto categoryDto, string userId);
    Task<bool> DeleteCategoryAsync(int categoryId, string userId);
}
