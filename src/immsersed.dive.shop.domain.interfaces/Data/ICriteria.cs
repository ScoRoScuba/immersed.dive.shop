using System.Collections.Generic;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces.Data
{
    public interface ICriteria<TEntity> where TEntity : class, IEntity
    {
        IList<TEntity> MatchQueryFrom( IDataStore<TEntity> ds);
    }
}
