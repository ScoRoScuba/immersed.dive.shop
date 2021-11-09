using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.Controllers;
using immersed.dive.shop.webapi.WebDtos;
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

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object);

            var result = await controller.Get();

            var okObjectResult = result as OkObjectResult;
            
            _ = IsAssignableFrom<IList<Course>>(okObjectResult.Value);
        }

        [Fact]
        public async Task POST_ReturnsAcceptedResultWithUrl()
        {
            var mockService = new Mock<ICourseService>();

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object);

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

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object);

            var result = await controller.Get(Guid.Empty);

            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task POST_AddingPersonToCourseReturnsCourseParticipantID()
        {
            var mockService = new Mock<ICourseService>();

            var courseParticipantGuid = Guid.NewGuid();

            mockService.Setup(g => g.AddParticipant(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(() => courseParticipantGuid);

            var controller = new CoursesController(mockService.Object, new Mock<IMapper>().Object);

            var result = await controller.AddParticipantToCourse(Guid.NewGuid(), Guid.NewGuid());

            var createdObjectResult = result as CreatedResult;

            Assert.EndsWith(courseParticipantGuid.ToString(), createdObjectResult.Location);
        }

        [Fact]
        public async void GET_CourseParticipantsReturnsPersonsOnCourse()
        {
            var mockService = new Mock<ICourseService>();

            var mockMapper = new Mock<IMapper>();

            mockService.Setup(g => g.GetParticipants(It.IsAny<Guid>())).ReturnsAsync(() => new List<Person>(){new(),new()});

            mockMapper.Setup(m=> m.Map<IList<Person>, IList<PersonDto>>(It.IsAny<IList<Person>>())).Returns(() => new List<PersonDto>() { new(), new() });

            var controller = new CoursesController(mockService.Object, mockMapper.Object);

            var result = await controller.GetCourseParticipants(Guid.NewGuid());

            Assert.IsType<List<PersonDto>>(result.Value);
        }
    }
}
