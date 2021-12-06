using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository.Criteria
{
    public class GetEventParticipantCriteria : ICriteria<EventParticipant>
    {
        private readonly Guid _eventParticipantId;

        public GetEventParticipantCriteria(Guid eventParticipantId)
        {
            _eventParticipantId = eventParticipantId;
        }
        public async Task<IList<EventParticipant>> MatchQueryFromAsync(IQueryable<EventParticipant> ds)
        {
            var val = await ds.Where(psa => psa.Id == _eventParticipantId).ToListAsync();

            var result = await ds.Where(ep => ep.Id == _eventParticipantId)
                                        .Include(p => p.Participant)
                                        .ToListAsync();

            return result;
        }
    }
}