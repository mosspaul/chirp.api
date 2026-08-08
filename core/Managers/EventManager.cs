using core.DTOs.EventDtos;
using core.Managers.Interfaces;
using core.Utilities;
using data.Models;
using data.Repositories.Interfaces;

namespace core.Managers;

public class EventManager : IEventManager
{
    private readonly IEventRepository _eventRepository;

    public EventManager(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto?> GetEventAsync(int eventId, string userId)
    {
        var @event = await _eventRepository.GetEventByIdAsync(eventId, userId);
        return @event == null ? null : new EventDto(@event);
    }

    public async Task<List<EventDto>> GetEventsAsync(string userId)
    {
        var events = await _eventRepository.GetEventsByUserAsync(userId);
        return events.Select(e => new EventDto(e)).ToList();
    }

    public async Task<List<EventDto>> GetEventsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
    {
        var events = await _eventRepository.GetEventsByDateRangeAsync(userId, startDate, endDate);
        return events.Select(e => new EventDto(e)).ToList();
    }

    public async Task<List<EventDto>> GetEventsByCategoryAsync(string userId, int categoryId)
    {
        var events = await _eventRepository.GetEventsByCategoryAsync(userId, categoryId);
        return events.Select(e => new EventDto(e)).ToList();
    }

    public async Task<EventDto> CreateEventAsync(CreateEventDto createEventDto, string userId)
    {
        var @event = new Event
        {
            Title = createEventDto.Title,
            Description = createEventDto.Description,
            StartTime = createEventDto.StartTime,
            EndTime = createEventDto.EndTime,
            Location = createEventDto.Location,
            IsAllDay = createEventDto.IsAllDay,
            EventCategoryId = createEventDto.EventCategoryId,
            UserId = userId
        };

        if (createEventDto.RecurrenceRule != null)
        {
            if (!RecurrenceRuleHelper.IsValidRRule(createEventDto.RecurrenceRule.RRuleString))
                throw new InvalidOperationException("Invalid RRule string provided");

            @event.RecurrenceRule = new RecurrenceRule
            {
                RRuleString = createEventDto.RecurrenceRule.RRuleString,
                StartDate = createEventDto.RecurrenceRule.StartDate,
                EndDate = createEventDto.RecurrenceRule.EndDate,
                MaxOccurrences = createEventDto.RecurrenceRule.MaxOccurrences
            };
        }

        var createdEvent = await _eventRepository.CreateEventAsync(@event);
        return new EventDto(createdEvent);
    }

    public async Task<EventDto?> UpdateEventAsync(int eventId, UpdateEventDto updateEventDto, string userId)
    {
        var @event = await _eventRepository.GetEventByIdAsync(eventId, userId);
        if (@event == null)
            return null;

        if (!string.IsNullOrEmpty(updateEventDto.Title))
            @event.Title = updateEventDto.Title;

        if (updateEventDto.Description != null)
            @event.Description = updateEventDto.Description;

        if (updateEventDto.StartTime.HasValue)
            @event.StartTime = updateEventDto.StartTime.Value;

        if (updateEventDto.EndTime.HasValue)
            @event.EndTime = updateEventDto.EndTime.Value;

        if (updateEventDto.Location != null)
            @event.Location = updateEventDto.Location;

        if (updateEventDto.IsAllDay.HasValue)
            @event.IsAllDay = updateEventDto.IsAllDay.Value;

        if (updateEventDto.EventCategoryId.HasValue)
            @event.EventCategoryId = updateEventDto.EventCategoryId.Value;

        if (updateEventDto.RecurrenceRule != null)
        {
            if (updateEventDto.RecurrenceRule.RRuleString != null)
            {
                if (!RecurrenceRuleHelper.IsValidRRule(updateEventDto.RecurrenceRule.RRuleString))
                    throw new InvalidOperationException("Invalid RRule string provided");

                if (@event.RecurrenceRule == null)
                {
                    @event.RecurrenceRule = new RecurrenceRule();
                }

                @event.RecurrenceRule.RRuleString = updateEventDto.RecurrenceRule.RRuleString;
            }

            if (updateEventDto.RecurrenceRule.StartDate.HasValue)
            {
                if (@event.RecurrenceRule == null)
                    @event.RecurrenceRule = new RecurrenceRule();
                @event.RecurrenceRule.StartDate = updateEventDto.RecurrenceRule.StartDate.Value;
            }

            if (updateEventDto.RecurrenceRule.EndDate.HasValue)
            {
                if (@event.RecurrenceRule == null)
                    @event.RecurrenceRule = new RecurrenceRule();
                @event.RecurrenceRule.EndDate = updateEventDto.RecurrenceRule.EndDate.Value;
            }

            if (updateEventDto.RecurrenceRule.MaxOccurrences.HasValue)
            {
                if (@event.RecurrenceRule == null)
                    @event.RecurrenceRule = new RecurrenceRule();
                @event.RecurrenceRule.MaxOccurrences = updateEventDto.RecurrenceRule.MaxOccurrences.Value;
            }
        }

        var updatedEvent = await _eventRepository.UpdateEventAsync(@event);
        return new EventDto(updatedEvent);
    }

    public async Task<bool> DeleteEventAsync(int eventId, string userId)
    {
        return await _eventRepository.DeleteEventAsync(eventId, userId);
    }
}
