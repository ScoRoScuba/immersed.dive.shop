using immersed.dive.shop.model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model.FilterParams;

namespace immersed.dive.shop.domain.interfaces
{
    public interface IEventService
    {
        Task<EventParticipant> GetEventParticipant(Guid eventId, Guid eventParticipantId);
        Task<Guid> AddParticipant(Guid eventId, Guid personId);
        Task<List<Person>> GetParticipants(Guid eventId);
        Task Add(Event @event);
        Task<Event> Get(Guid eventId);

        Task<IList<Event>> GetAllEvents();
        Task<IList<Event>> GetFilteredEvents(EventFilterParams eventFilterParams);
    }
}