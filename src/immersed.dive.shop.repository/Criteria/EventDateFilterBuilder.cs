using System;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.model.FilterParams;

namespace immersed.dive.shop.repository.Criteria;

public class EventDateFilterBuilder : IEventDateFilterBuilder
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public EventDateFilterBuilder(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public DateSpan GetDateCriteria(EventCalendarEnum filter)
    {
        DateTime utcDateTimeNow = _dateTimeProvider.UtcNow;

        switch (filter)
        {
            case EventCalendarEnum.ThisWeek:
            {
                int numberDaysLeftInWeek = WholeDaysLeftInCurrentWeek();
                var endOfWeekDate = utcDateTimeNow.AddDays(numberDaysLeftInWeek);
                return new DateSpan
                {
                    StartDate = _dateTimeProvider.UtcNow,
                    EndDate = endOfWeekDate
                };
            }
            case EventCalendarEnum.ComingWeek:
            {
                return new DateSpan
                {
                    StartDate = utcDateTimeNow,
                    EndDate = utcDateTimeNow.AddDays(7)
                };
            }
            case EventCalendarEnum.NextWeek:
            {
                int numberDaysLeftInWeek = WholeDaysLeftInCurrentWeek();

                return new DateSpan
                {
                    StartDate = utcDateTimeNow.AddDays(numberDaysLeftInWeek+1),
                    EndDate = utcDateTimeNow.AddDays(numberDaysLeftInWeek).AddDays(7)
                };
            }
            case EventCalendarEnum.ThisMonth:
            {
                int daysLeftInMonth = DaysLeftInMonth();
                return new DateSpan
                {
                    StartDate = utcDateTimeNow,
                    EndDate = utcDateTimeNow.AddDays(daysLeftInMonth)
                };
            }
            case EventCalendarEnum.ComingMonth:
            {
                int daysLeftInMonth = DaysLeftInMonth();
                return new DateSpan
                {
                    StartDate = utcDateTimeNow,
                    EndDate = utcDateTimeNow.AddDays(30)
                };
            }
            case EventCalendarEnum.NextMonth:
            {
                var startOfNextMonth = new DateTime(utcDateTimeNow.AddMonths(1).Year, utcDateTimeNow.AddMonths(1).Month, 1);
                return new DateSpan
                {
                    StartDate = startOfNextMonth,
                    EndDate = startOfNextMonth.AddMonths(1).AddDays(-1)
                };
            }
            default:
                return new DateSpan
                {
                    StartDate = utcDateTimeNow.AddYears(-100),
                    EndDate = utcDateTimeNow.AddYears(10)
                };
        }
    }

    private int WholeDaysLeftInCurrentWeek()
    {
        var dayOfWeekNowAdjusted = (_dateTimeProvider.UtcNow.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)_dateTimeProvider.UtcNow.DayOfWeek - 1);

        if (dayOfWeekNowAdjusted == 7)
        {
            return 0;
        }
        else
        {
            return 7-(dayOfWeekNowAdjusted + 1);
        }
    }

    private int DaysLeftInMonth()
    {
        int currentDay = _dateTimeProvider.UtcNow.Day;
        int daysInMonth = DateTime.DaysInMonth(_dateTimeProvider.UtcNow.Year, _dateTimeProvider.UtcNow.Month);

        return daysInMonth - currentDay;
    }
}