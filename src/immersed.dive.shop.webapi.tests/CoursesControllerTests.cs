using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using static Xunit.Assert;

namespace immersed.dive.shop.webapi.tests
{
    public class CoursesControllerTests
    {
        [Fact]
        public async Task GET_ReturnsOkResultWithCourses()
        {
            var controller = new CoursesController();

            var result = await controller.Get();

            var okObjectResult = result as OkObjectResult;
            
            _ = IsAssignableFrom<IList<Course>>(okObjectResult.Value);
        }

        [Fact]
        public async Task POST_ReturnsAcceptedResultWithUrl()
        {
            var controller = new CoursesController();

            var course = new Course();

            var result = await controller.Post(course);

            var createdObjectResult = result as CreatedResult;

            EndsWith(course.Id.ToString(), createdObjectResult.Location);
        }
    }
}
