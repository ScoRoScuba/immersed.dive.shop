using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static Xunit.Assert;

namespace immersed.dive.shop.webapi.tests
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
    }
}
