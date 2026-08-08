using core.DTOs.EventDtos;
using core.Managers.Interfaces;
using data.Models;
using data.Repositories.Interfaces;

namespace core.Managers;

public class EventCategoryManager : IEventCategoryManager
{
    private readonly IEventCategoryRepository _categoryRepository;

    public EventCategoryManager(IEventCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<EventCategoryDto?> GetCategoryAsync(int categoryId, string userId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, userId);
        if (category == null)
            return null;

        return new EventCategoryDto
        {
            Id = category.Id,
            Title = category.Title,
            Color = category.Color,
            Description = category.Description
        };
    }

    public async Task<List<EventCategoryDto>> GetCategoriesAsync(string userId)
    {
        var categories = await _categoryRepository.GetCategoriesByUserAsync(userId);
        return categories.Select(c => new EventCategoryDto
        {
            Id = c.Id,
            Title = c.Title,
            Color = c.Color,
            Description = c.Description
        }).ToList();
    }

    public async Task<EventCategoryDto> CreateCategoryAsync(EventCategoryDto categoryDto, string userId)
    {
        var category = new EventCategory
        {
            Title = categoryDto.Title,
            Color = categoryDto.Color,
            Description = categoryDto.Description,
            UserId = userId
        };

        var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
        return new EventCategoryDto
        {
            Id = createdCategory.Id,
            Title = createdCategory.Title,
            Color = createdCategory.Color,
            Description = createdCategory.Description
        };
    }

    public async Task<EventCategoryDto?> UpdateCategoryAsync(int categoryId, EventCategoryDto categoryDto, string userId)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, userId);
        if (category == null)
            return null;

        if (!string.IsNullOrEmpty(categoryDto.Title))
            category.Title = categoryDto.Title;

        if (!string.IsNullOrEmpty(categoryDto.Color))
            category.Color = categoryDto.Color;

        if (categoryDto.Description != null)
            category.Description = categoryDto.Description;

        var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);
        return new EventCategoryDto
        {
            Id = updatedCategory.Id,
            Title = updatedCategory.Title,
            Color = updatedCategory.Color,
            Description = updatedCategory.Description
        };
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId, string userId)
    {
        return await _categoryRepository.DeleteCategoryAsync(categoryId, userId);
    }
}
