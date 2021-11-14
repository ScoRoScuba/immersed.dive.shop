using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class EventParticipantStore : IDataStore<EventParticipant>
    {
        private readonly DiveShopDBContext _dataContext;

        public EventParticipantStore(DiveShopDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(EventParticipant entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.Live = true;

            var result = await _dataContext.AddAsync(entity);
            var count = await _dataContext.SaveChangesAsync();
            return count;
        }

        public Task RemoveAsync(EventParticipant entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<EventParticipant>> GetAllAsync()
        {
            return await _dataContext.EventParticipants.ToListAsync();
        }

        public Task<int> UpdateAsync(EventParticipant entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EventParticipant> FindAsync(Expression<Func<EventParticipant, bool>> predicate)
        {
            return await _dataContext.EventParticipants.SingleOrDefaultAsync(predicate);
        }

        public async Task<IList<EventParticipant>> MatchAsync(ICriteria<EventParticipant> criteria)
        {
            return await criteria.MatchQueryFromAsync(_dataContext.EventParticipants);
        }
    }
}
