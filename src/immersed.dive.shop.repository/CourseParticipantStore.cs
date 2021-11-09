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
    public class CourseParticipantStore : IDataStore<CourseParticipant>
    {
        private readonly DiveShopDBContext _dataContext;

        public CourseParticipantStore(DiveShopDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(CourseParticipant entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.Live = true;

            var result = await _dataContext.AddAsync(entity);
            var count = await _dataContext.SaveChangesAsync();
            return count;
        }

        public Task RemoveAsync(CourseParticipant entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CourseParticipant>> GetAllAsync()
        {
            return await _dataContext.CourseParticipants.ToListAsync();
        }

        public Task<int> UpdateAsync(CourseParticipant entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CourseParticipant> FindAsync(Expression<Func<CourseParticipant, bool>> predicate)
        {
            return await _dataContext.CourseParticipants.SingleOrDefaultAsync(predicate);
        }

        public async Task<IList<CourseParticipant>> MatchAsync(ICriteria<CourseParticipant> criteria)
        {
            return criteria.MatchQueryFrom(_dataContext.CourseParticipants);
        }
    }
}
