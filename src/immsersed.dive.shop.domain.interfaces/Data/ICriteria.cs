using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces.Data
{
    public interface ICriteria<TEntity> where TEntity : class, IEntity
    {
        Task<IList<TEntity>> MatchQueryFromAsync(IQueryable<TEntity> ds);
    }
}
