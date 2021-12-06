using immersed.dive.shop.model;
using immersed.dive.shop.model.FilterParams;

namespace immersed.dive.shop.domain.interfaces
{
    public interface IEventDateFilterBuilder
    {
        DateSpan GetDateCriteria(EventCalendarEnum filter);
    }
}