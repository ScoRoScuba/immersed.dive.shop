using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Moq;
using Serilog;
using Xunit;

namespace immersed.dive.shop.application.tests.CourseServiceTests
{
    public class EventParticipantTests
    {
        private Mock<ILogger> mockLogger = new Mock<ILogger>();

        [Fact]
        public async Task ParticipantIsAddedToEvent()
        {
            var mockDataStore = new Mock<IDataStore<Event>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockCourseParticipantService = new Mock<IEventParticipantService>();

            mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<Event, bool>>>())).ReturnsAsync(new Event());
            mockPersonService.Setup(p => p.Get(It.IsAny<Guid>())).ReturnsAsync(new model.Person());

            var service = new EventService(mockDataStore.Object, mockCourseParticipantService.Object, mockLogger.Object);

            var result = await service.AddParticipant(Guid.NewGuid(), Guid.NewGuid());

            Assert.True( result != Guid.Empty);
        }

        [Fact]
        public async Task CanGetParticipantsForCourse()
        {
            var mockCourseDataStore = new Mock<IDataStore<Event>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockCourseParticipantService = new Mock<IEventParticipantService>();

            mockCourseParticipantService.Setup(cp => cp.GetParticipants(It.IsAny<Guid>())).ReturnsAsync(new List<model.Person>{new model.Person()});

            var service = new EventService(mockCourseDataStore.Object, mockCourseParticipantService.Object, mockLogger.Object);

            var result = await service.GetParticipants(Guid.NewGuid());

            Assert.NotEmpty(result);
        }
    }
}
