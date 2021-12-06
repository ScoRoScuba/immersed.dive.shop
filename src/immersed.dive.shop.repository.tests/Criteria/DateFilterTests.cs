using System;
using System.Collections.Generic;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model.FilterParams;
using immersed.dive.shop.repository.Criteria;
using Moq;
using Xunit;

namespace immersed.dive.shop.repository.tests.Criteria
{
    public class DateFilterTests
    {
        public class DateTimeTheoryData
        {
            public DateTime SeedDateTime { get; set; }
            public DateTime ExpectedStartDate { get; set; }
            public DateTime ExpectedEndDate { get; set; }
        }

        public static IEnumerable<object[]> DaysInWeekTestData()
        {
            yield return new[]
            {new DateTimeTheoryData { 
                SeedDateTime = new DateTime( 2021, 11, 29), 
                ExpectedStartDate = new DateTime( 2021, 11, 29), 
                ExpectedEndDate = new DateTime( 2021, 12, 5)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 12, 1),
                ExpectedStartDate = new DateTime( 2021, 12, 1),
                ExpectedEndDate = new DateTime( 2021, 12, 5)}};
            yield return new[] { new DateTimeTheoryData
            {
                SeedDateTime = new DateTime( 2021, 12, 3),
                ExpectedStartDate = new DateTime( 2021, 12, 3),
                ExpectedEndDate = new DateTime( 2021, 12, 5)}
            };
        }

        [Theory, MemberData(nameof(DaysInWeekTestData))]
        public void ThisWeek_GetsRemainingDaysInWeek(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.ThisWeek);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }

        public static IEnumerable<object[]> ComingWeekTestData()
        {
            yield return new[]
            {new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 11, 29),
                ExpectedStartDate = new DateTime( 2021, 11, 29),
                ExpectedEndDate = new DateTime( 2021, 12, 6)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 12, 1),
                ExpectedStartDate = new DateTime( 2021, 12, 1),
                ExpectedEndDate = new DateTime( 2021, 12, 8)}};
            yield return new[] { new DateTimeTheoryData
                {
                    SeedDateTime = new DateTime( 2021, 12, 3),
                    ExpectedStartDate = new DateTime( 2021, 12, 3),
                    ExpectedEndDate = new DateTime( 2021, 12, 10)}
            };
        }

        [Theory, MemberData(nameof(ComingWeekTestData))]
        public void ComingWeek_GetsDatesForComingWeek(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.ComingWeek);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }

        public static IEnumerable<object[]> NextWeekTestData()
        {
            yield return new[]
            {new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 11, 29),
                ExpectedStartDate = new DateTime( 2021, 12, 6),
                ExpectedEndDate = new DateTime( 2021, 12, 12)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 12, 1),
                ExpectedStartDate = new DateTime( 2021, 12, 6),
                ExpectedEndDate = new DateTime( 2021, 12, 12)}};
            yield return new[] { new DateTimeTheoryData
                {
                    SeedDateTime = new DateTime( 2021, 12, 12),
                    ExpectedStartDate = new DateTime( 2021, 12, 13),
                    ExpectedEndDate = new DateTime( 2021, 12, 19)}
            };
        }

        [Theory, MemberData(nameof(NextWeekTestData))]
        public void NextWeek_GetsStartAndEndDateForNextWeek(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.NextWeek);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }

        public static IEnumerable<object[]> ThisMonthTestData()
        {
            yield return new[]
            {new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 11, 29),
                ExpectedStartDate = new DateTime( 2021, 11, 29),
                ExpectedEndDate = new DateTime( 2021, 11, 30)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 12, 1),
                ExpectedStartDate = new DateTime( 2021, 12, 1),
                ExpectedEndDate = new DateTime( 2021, 12, 31)}};
            yield return new[] { new DateTimeTheoryData
                {
                    SeedDateTime = new DateTime( 2021, 12, 12),
                    ExpectedStartDate = new DateTime( 2021, 12, 12),
                    ExpectedEndDate = new DateTime( 2021, 12, 31)}
            };
        }
        [Theory, MemberData(nameof(ThisMonthTestData))]
        public void ThisMonth_GetsStartAndEndDateForRestOfMonth(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.ThisMonth);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }

        public static IEnumerable<object[]> ComingMonthTestData()
        {
            yield return new[]
            {new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 11, 29),
                ExpectedStartDate = new DateTime( 2021, 11, 29),
                ExpectedEndDate = new DateTime( 2021, 12, 29)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 12, 1),
                ExpectedStartDate = new DateTime( 2021, 12, 1),
                ExpectedEndDate = new DateTime( 2021, 12, 31)}};
            yield return new[] { new DateTimeTheoryData
                {
                    SeedDateTime = new DateTime( 2021, 12, 12),
                    ExpectedStartDate = new DateTime( 2021, 12, 12),
                    ExpectedEndDate = new DateTime( 2022, 1, 11)}
            };
        }
        [Theory, MemberData(nameof(ComingMonthTestData))]
        public void ComingMOnth_GetsStartAndEndDateForRestOfMonth(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.ComingMonth);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }

        public static IEnumerable<object[]> NextMonthTestData()
        {
            yield return new[]
            {new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 11, 29),
                ExpectedStartDate = new DateTime( 2021, 12, 1),
                ExpectedEndDate = new DateTime( 2021, 12, 31)}
            };
            yield return new[] { new DateTimeTheoryData {
                SeedDateTime = new DateTime( 2021, 10, 1),
                ExpectedStartDate = new DateTime( 2021, 11, 1),
                ExpectedEndDate = new DateTime( 2021, 11, 30)}};
        }
        [Theory, MemberData(nameof(NextMonthTestData))]
        public void NextMonth_GetsStartAndEndDateForNextMonth(DateTimeTheoryData theoryData)
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();

            mockDateTimeProvider.SetupGet(dt => dt.UtcNow).Returns(theoryData.SeedDateTime);

            var dateRangeBuilder = new EventDateFilterBuilder(mockDateTimeProvider.Object);

            var result = dateRangeBuilder.GetDateCriteria(EventCalendarEnum.NextMonth);

            Assert.Equal(theoryData.ExpectedStartDate.Date, result.StartDate.Date);
            Assert.Equal(theoryData.ExpectedEndDate.Date, result.EndDate.Date);
        }



    }
}
