using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.model.FilterParams;
using Microsoft.EntityFrameworkCore;

namespace immersed.dive.shop.repository.Criteria;

public class GetFilteredEventsCriteria : ICriteria<Event>
{
    private readonly EventFilterParams _eventFilterParams;
    private readonly DateSpan _dateSpan;
    private readonly EventDateFilterBuilder _eventDateFilterBuilder;

    public GetFilteredEventsCriteria(EventFilterParams eventFilterParams, DateSpan dateSpan)
    {
        _eventFilterParams = eventFilterParams;
        _dateSpan = dateSpan;
    }

    public async Task<IList<Event>> MatchQueryFromAsync(IQueryable<Event> ds)
    {
        return await ds
            .Include( d=>d.Course)
            .Include(d=>d.Dates)
            .Where(ep => 
                ep.Dates.Any( dt=>dt.Date >= _dateSpan.StartDate && dt.Date <= _dateSpan.EndDate) && 
                ( _eventFilterParams.course.ToLower() == "all" || _eventFilterParams.course == null ? 
                    ep.Course.Name.ToLower() != "all" : 
                    ep.Course.Name.ToLower() == _eventFilterParams.course.ToLower()))
            .ToListAsync();
    }

}