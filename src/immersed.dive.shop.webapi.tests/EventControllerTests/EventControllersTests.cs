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
using Serilog;
using Xunit;

namespace immersed.dive.shop.webapi.tests.EventControllerTests
{
    public class EventControllersTests
    {
        private Mock<ILogger> mockLogger = new Mock<ILogger>();

        [Fact]
        public async Task POST_AddingPersonToCourseReturnsCourseParticipantID()
        {
            var mockService = new Mock<IEventService>();

            var courseParticipantGuid = Guid.NewGuid();

            mockService.Setup(g => g.AddParticipant(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(() => courseParticipantGuid);

            var controller = new EventsController(mockService.Object, new Mock<IMapper>().Object, mockLogger.Object);

            var result = await controller.AddParticipantToEvent(Guid.NewGuid(), Guid.NewGuid());

            var createdObjectResult = result as CreatedResult;

            Assert.EndsWith(courseParticipantGuid.ToString(), createdObjectResult.Location);
        }

        [Fact]
        public async void GET_CourseParticipantsReturnsPersonsOnCourse()
        {
            var mockService = new Mock<IEventService>();

            var mockMapper = new Mock<IMapper>();

            mockService.Setup(g => g.GetParticipants(It.IsAny<Guid>())).ReturnsAsync(() => new List<Person>() { new(), new() });

            mockMapper.Setup(m => m.Map<IList<Person>, IList<PersonDto>>(It.IsAny<IList<Person>>())).Returns(() => new List<PersonDto>() { new(), new() });

            var controller = new EventsController(mockService.Object, mockMapper.Object, mockLogger.Object);

            var result = await controller.GetEventsParticipants(Guid.NewGuid());

            Assert.IsType<List<PersonDto>>(result.Value);
        }
    }
}
