using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using Moq;
using Serilog;
using Xunit;

namespace immersed.dive.shop.application.tests.EventServiceTests;

public class EventParticipantsServiceTests
{
    private Mock<ILogger> mockLogger = new Mock<ILogger>();

    [Fact]
    public async Task GetEventParticipantsReturnsListOfPersonsOnCourse()
    {
        var mockDataStore = new Mock<IDataStore<EventParticipant>>();

        var list = new List<EventParticipant>()
        {
            new EventParticipant()
            {
                Event = new Event(),
                Participant = new model.Person()
            },
            new EventParticipant()
            {
                Event = new Event(),
                Participant = new model.Person()
            }
        };

        var courseId = list[0].Event.Id;
        list[0].EventId = courseId;
        list[1].EventId = courseId;
        list[1].Event.Id = courseId;

        mockDataStore.Setup(d => d.MatchAsync(It.IsAny<ICriteria<EventParticipant>>())).ReturnsAsync(list);
            
        var eventParticipantService = new EventParticipantService(mockDataStore.Object, mockLogger.Object);

        var result = await eventParticipantService.GetParticipants(courseId);

        Assert.NotEmpty(result);
        Assert.IsType<List<model.Person>>(result);
    }

}