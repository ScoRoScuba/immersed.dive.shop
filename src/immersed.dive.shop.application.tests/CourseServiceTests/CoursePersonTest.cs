using System;
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
    public class CoursePersonTest
    {
        [Fact]
        public async Task PersonIsAddedToCourse()
        {
            var mockDataStore = new Mock<IDataStore<Course>>();
            var mockPersonService = new Mock<IPersonService>();

            mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<Course, bool>>>())).ReturnsAsync(new Course());

            mockPersonService.Setup(p => p.Get(It.IsAny<Guid>())).ReturnsAsync(new model.Person());

            var service = new CourseService(mockDataStore.Object, mockPersonService.Object);

            var result = await service.AddPersonToCourse(Guid.NewGuid(), Guid.NewGuid());

            Assert.False(result == -1);
        }
    }
}
