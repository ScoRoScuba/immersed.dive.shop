using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces.Data
{
    public interface IDataStore<TEntity> where TEntity : class, IEntity
    {
        Task<int> AddAsync(TEntity entity);

        Task RemoveAsync(TEntity entity);
        Task<IList<TEntity>> GetAllAsync();
        Task<int> UpdateAsync(TEntity entity);
        Task<int> CountAsync();

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IList<TEntity>> MatchAsync(ICriteria<TEntity> criteria);
    }
}
