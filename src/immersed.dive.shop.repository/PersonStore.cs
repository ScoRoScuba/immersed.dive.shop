using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class PersonStore : IDataStore<Person>
    {
        private readonly DiveShopDBContext _dataContext;
        public PersonStore(DiveShopDBContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> AddAsync(Person entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.Live = true;

            var result = await _dataContext.AddAsync(entity);
            var count = await _dataContext.SaveChangesAsync();
            return count;
        }

        public Task RemoveAsync(Person entity)
        {
            entity.Live = false;
            throw new NotImplementedException();
        }

        public async Task<IList<Person>> GetAllAsync()
        {
            return await _dataContext.People.AsQueryable().ToListAsync();
        }

        public Task<int> UpdateAsync(Person entity)
        {
            entity.LastUpdated = DateTime.UtcNow;
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _dataContext.People.SingleOrDefaultAsync(predicate);
        }

        public async Task<IList<Person>> MatchAsync(ICriteria<Person> criteria)
        {
            return await criteria.MatchQueryFromAsync(_dataContext.People);
        }
    }
}
