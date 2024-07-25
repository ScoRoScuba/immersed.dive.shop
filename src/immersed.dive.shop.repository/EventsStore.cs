using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository;

public class EventsStore : IDataStore<Event>
{
    private readonly DiveShopDBContext _dataContext;

    public EventsStore(DiveShopDBContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<int> AddAsync(Event entity)
    {
        entity.DateCreated = DateTime.UtcNow;
        entity.Live = true;

        var result = await _dataContext.AddAsync(entity);
        var count = await _dataContext.SaveChangesAsync();
        return count;
    }

    public Task RemoveAsync(Event entity)
    {
        entity.Live = false;
        throw new NotImplementedException();
    }

    public async Task<IList<Event>> GetAllAsync()
    {
        return await _dataContext.Events.AsQueryable().ToListAsync();
    }

    public async Task<int> UpdateAsync(Event entity)
    {
        entity.LastUpdated = DateTime.UtcNow;

        var result = _dataContext.Events.Update(entity);
        var count = await _dataContext.SaveChangesAsync();

        return count;
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Event> FindAsync(Expression<Func<Event, bool>> predicate)
    {
        return await _dataContext.Events.SingleOrDefaultAsync(predicate);
    }

    public async Task<IList<Event>> MatchAsync(ICriteria<Event> criteria)
    {
        return await criteria.MatchQueryFromAsync(_dataContext.Events);
    }
}