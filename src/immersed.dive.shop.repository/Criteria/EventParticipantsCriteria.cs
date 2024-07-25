using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository.Criteria;

public class EventParticipantsCriteria : ICriteria<EventParticipant>
{
    private readonly Guid _eventId;

    public EventParticipantsCriteria(Guid eventId )
    {
        _eventId = eventId;
    }

    public async Task<IList<EventParticipant>> MatchQueryFromAsync(IQueryable<EventParticipant> ds)
    {
        return  await ds
            .Include(p => p.Participant)
            .Include( e =>e.Event)
            .Include(c=>c.Event.Course)
            .Where(ep => ep.EventId == _eventId).ToListAsync();
    }
}