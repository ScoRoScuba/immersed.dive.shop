using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using immersed.dive.shop.webapi.Core;
using immersed.dive.shop.webapi.WebDtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using Xunit;
using static Xunit.Assert;

namespace immersed.dive.shop.webapi.tests.CoursesControllerTests
{
    public class CoursesControllerTests
    {
        private Mock<ILogger> mockLogger = new Mock<ILogger>();

        private readonly IMapper mapper;

        public CoursesControllerTests()
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile(new MappingProfiles());
            });

            mapper = new AutoMapper.Mapper(mapperConfiguration);
        }

        [Fact]
        public async Task GET_ReturnsOkResultWithCourses()
        {
            var mockService = new Mock<ICourseService>();

            mockService.Setup(g => g.GetAll()).ReturnsAsync(new List<Course>
            {
                new(),
                new()
            });

            var controller = new CoursesController(mockService.Object, mapper, mockLogger.Object);

            var result = await controller.Get();

            var okObjectResult = result as OkObjectResult;
            
            _ = IsAssignableFrom<IList<CourseDto>>(okObjectResult.Value);
        }

        [Fact]
        public async Task POST_ReturnsAcceptedResultWithUrl()
        {
            var mockService = new Mock<ICourseService>();

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object, mockLogger.Object);

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

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object, mockLogger.Object);

            var result = await controller.Get(Guid.Empty);

            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }


    }
}
