using System;
using System.Collections.Generic;
using System.Linq;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class EventParticipantsQuery : ICriteria<EventParticipant>
    {
        private readonly Guid _eventId;

        public EventParticipantsQuery(Guid eventId )
        {
            _eventId = eventId;
        }

        public IList<EventParticipant> MatchQueryFrom(IQueryable<EventParticipant> ds)
        {
            var result = ds
                .Include(p => p.Participant)
                .Include( e =>e.Event)
                .Include(c=>c.Event.Course)
                .Where(cp => cp.EventId == _eventId).ToList();

            return result.ToList();
        }
    }
}
