using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository.Criteria;

public class GetEventCriteria : ICriteria<Event>
{
    private readonly Guid _eventId;

    public GetEventCriteria(Guid eventId)
    {
        _eventId = eventId;
    }

    public async Task<IList<Event>> MatchQueryFromAsync(IQueryable<Event> ds)
    {
        return await ds
            .Include(p => p.Participants)
            .Where(e => e.Id == _eventId)
            .ToListAsync();
    }
}