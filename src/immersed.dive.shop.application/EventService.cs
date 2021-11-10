using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Serilog;

namespace immersed.dive.shop.application
{
    public class EventService : IEventService
    {
        private readonly IDataStore<Event> _eventDataStore;
        private readonly IEventParticipantService _eventParticipantService;
        private readonly ILogger _logger;

        public EventService(IDataStore<Event> eventDataStore, IEventParticipantService eventParticipantService, ILogger logger)
        {
            _eventDataStore = eventDataStore;
            _eventParticipantService = eventParticipantService;
            _logger = logger;
        }

        public async Task<EventParticipant> GetEventParticipant(Guid eventId, Guid eventParticipantId)
        {
            var @event = await _eventDataStore.FindAsync(c => c.Id == eventId);

            if (@event == null)
            {
                _logger.Warning("{class}:{action}-{message}-{eventId}", nameof(CourseService), nameof(GetEventParticipant), "EventNotFound", eventId);
                return null;
            }

            var result = await _eventParticipantService.GetParticipant(eventParticipantId);

            return result;
        }

        public async Task<Guid> AddParticipant(Guid eventId, Guid personId)
        {
            var course = await _eventDataStore.FindAsync(c => c.Id == eventId);

            var courseParticipant = new EventParticipant
            {
                EventId = eventId,
                ParticipantId = personId,
                Live = true,
                DateCreated = DateTime.UtcNow
            };

            course.Participants.Add(courseParticipant);

            var result = await _eventDataStore.UpdateAsync(course);

            return courseParticipant.Id;
        }

        public async Task<List<model.Person>> GetParticipants(Guid eventId)
        {
            var result = await _eventParticipantService.GetParticipants(eventId);

            return result;
        }
    }
}
