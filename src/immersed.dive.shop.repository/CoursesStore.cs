using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository
{
    public class CoursesStore : IDataStore<Course>
    {
        private readonly DiveShopDBContext _dataContext;

        public CoursesStore(DiveShopDBContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> AddAsync(Course entity)
        {
            var result = await _dataContext.AddAsync(entity);
            var count = await _dataContext.SaveChangesAsync();
            return count;
        }

        public Task RemoveAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Course>> GetAllAsync()
        {
            return await _dataContext.Courses.AsQueryable().ToListAsync();
        }

        public Task<int> UpdateAsync(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Course> FindAsync(Expression<Func<Course, bool>> predicate)
        {
            return await _dataContext.Courses.SingleOrDefaultAsync(predicate);
        }

        public Task<IList<Course>> FindAllAsync(Expression<Func<Course, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Course>> MatchAsync(ICriteria<Course> criteria)
        {
            throw new NotImplementedException();
        }
    }
}
