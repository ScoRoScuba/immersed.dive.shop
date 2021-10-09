using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

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
            
            _ = Assert.IsAssignableFrom<IList<Course>>(okObjectResult.Value);
        }
    }
}
