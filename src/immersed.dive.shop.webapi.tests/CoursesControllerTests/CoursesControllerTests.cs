using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Xunit.Assert;

namespace immersed.dive.shop.webapi.tests.CoursesControllerTests
{
    public class CoursesControllerTests
    {
        [Fact]
        public async Task GET_ReturnsOkResultWithCourses()
        {
            var mockService = new Mock<ICourseService>();

            mockService.Setup(g => g.GetAll()).ReturnsAsync(new List<Course>
            {
                new(),
                new()
            });

            var controller = new CoursesController(mockService.Object);

            var result = await controller.Get();

            var okObjectResult = result as OkObjectResult;
            
            _ = IsAssignableFrom<IList<Course>>(okObjectResult.Value);
        }

        [Fact]
        public async Task POST_ReturnsAcceptedResultWithUrl()
        {
            var mockService = new Mock<ICourseService>();

            var controller = new CoursesController(mockService.Object);

            var course = new Course();

            var result = await controller.Post(course);

            var createdObjectResult = result as CreatedResult;

            EndsWith(course.Id.ToString(), createdObjectResult.Location);
        }

        [Fact]
        public async Task GET_ReturnsNotFoundWhenIncorrectIdUsed()
        {
            var mockService = new Mock<ICourseService>();

            mockService.Setup(g => g.Get(It.IsAny<Guid>())).ReturnsAsync(()=>null);

            var controller = new CoursesController(mockService.Object);

            var result = await controller.Get(Guid.Empty);

            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task POST_AddingPersonToCourseReturnsNumberOnCourse()
        {
            var mockService = new Mock<ICourseService>();

            mockService.Setup(g => g.AddParticipant(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(() => 1);

            var controller = new CoursesController(mockService.Object);

            var result = await controller.AddParticipantToCourse(Guid.NewGuid(), Guid.NewGuid());

            var createdObjectResult = result as OkObjectResult;

            Assert.True((int)createdObjectResult.Value == 1);
        }
    }
}
