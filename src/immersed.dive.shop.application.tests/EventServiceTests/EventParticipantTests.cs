using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Moq;
using Serilog;
using Xunit;

namespace immersed.dive.shop.application.tests.EventServiceTests;

public class EventParticipantTests
{
    private Mock<ILogger> mockLogger = new Mock<ILogger>();

    [Fact]
    public async Task ParticipantIsAddedToEvent()
    {
        var mockEventDataStore = new Mock<IDataStore<Event>>();
        var mockPersonService = new Mock<IPersonService>();
        var mockCourseParticipantService = new Mock<IEventParticipantService>();
        var mockDateFilterBuilder = new Mock<IEventDateFilterBuilder>();

        mockEventDataStore.Setup(d => d.MatchAsync(It.IsAny<ICriteria<Event>>()))
            .ReturnsAsync(new List<Event>() {new Event()});

        mockPersonService.Setup(p => p.Get(It.IsAny<Guid>())).ReturnsAsync(new model.Person());

        var service = new EventService(mockEventDataStore.Object, mockCourseParticipantService.Object, mockDateFilterBuilder.Object, mockLogger.Object);

        var result = await service.AddParticipant(Guid.NewGuid(), Guid.NewGuid());

        Assert.True( result != Guid.Empty);
    }

    [Fact]
    public async Task CanGetParticipantsForCourse()
    {
        var mockCourseDataStore = new Mock<IDataStore<Event>>();
        var mockCourseParticipantService = new Mock<IEventParticipantService>();
        var mockDateFilterBuilder = new Mock<IEventDateFilterBuilder>();

        mockCourseParticipantService.Setup(cp => cp.GetParticipants(It.IsAny<Guid>())).ReturnsAsync(new List<model.Person> {new model.Person()});

        var service = new EventService(mockCourseDataStore.Object, mockCourseParticipantService.Object, mockDateFilterBuilder.Object, mockLogger.Object);

        var result = await service.GetParticipants(Guid.NewGuid());

        Assert.NotEmpty(result);
    }
}