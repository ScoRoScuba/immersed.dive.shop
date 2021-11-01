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
            var result = await _dataContext.AddAsync(entity);
            var count = await _dataContext.SaveChangesAsync();
            return count;
        }

        public Task RemoveAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Person>> GetAllAsync()
        {
            return await _dataContext.Persons.AsQueryable().ToListAsync();
        }

        public Task<int> UpdateAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _dataContext.Persons.SingleOrDefaultAsync(predicate);
        }

        public Task<IList<Person>> FindAllAsync(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Person>> MatchAsync(ICriteria<Person> criteria)
        {
            throw new NotImplementedException();
        }
    }
}
