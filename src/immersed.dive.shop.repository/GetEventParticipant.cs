using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class GetEventParticipant : ICriteria<EventParticipant>
    {
        private readonly Guid _eventParticipantId;

        public GetEventParticipant(Guid eventParticipantId)
        {
            _eventParticipantId = eventParticipantId;
        }
        public async Task<IList<EventParticipant>> MatchQueryFromAsync(IQueryable<EventParticipant> ds)
        {
            return await ds
                .Include(p => p.Participant)
                .Where(ep => ep.Id == _eventParticipantId)
                .ToListAsync();
        }
    }
}