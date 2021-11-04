using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Moq;
using Xunit;

namespace immersed.dive.shop.application.tests.CourseServiceTests
{
    public class CourseParticipantTests
    {
        [Fact]
        public async Task ParticipantIsAddedToCourse()
        {
            var mockDataStore = new Mock<IDataStore<Course>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockCourseParticipantService = new Mock<ICourseParticipantService>();

            mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(new Course());

            mockPersonService.Setup(p => p.Get(It.IsAny<Guid>())).ReturnsAsync(new model.Person());

            var service = new CourseService(mockDataStore.Object, mockPersonService.Object, mockCourseParticipantService.Object);

            var result = await service.AddParticipant(Guid.NewGuid(), Guid.NewGuid());

            Assert.False(result == -1);
        }

        [Fact]
        public async Task CanGetParticipantsForCourse()
        {
            var mockCourseDataStore = new Mock<IDataStore<Course>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockCourseParticipantService = new Mock<ICourseParticipantService>();

            mockCourseParticipantService.Setup(cp => cp.GetCourseParticipants(It.IsAny<Guid>())).ReturnsAsync(new List<CourseParticipant>{new CourseParticipant()});

            var service = new CourseService(mockCourseDataStore.Object, mockPersonService.Object, mockCourseParticipantService.Object);

            var result = await service.GetParticipants(Guid.NewGuid());

            Assert.NotEmpty(result);
        }
    }
}
