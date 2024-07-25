using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using immersed.dive.shop.application.Person;
using immersed.dive.shop.domain.interfaces.Data;
using Moq;
using Serilog;
using Xunit;

namespace immersed.dive.shop.application.tests.PersonServiceTests;

public class PersonServiceTests
{
    private Mock<ILogger> mockLogger = new Mock<ILogger>();

    [Fact]
    public async Task GetAllReturnsAllPersons()
    {
        var mockDataStore = new Mock<IDataStore<model.Person>>();

        mockDataStore.Setup(d => d.GetAllAsync()).ReturnsAsync(() => new List<model.Person>
        {
            new model.Person(),
            new model.Person(),
            new model.Person()
        });

        var courseService = new PersonService(mockDataStore.Object, mockLogger.Object);

        var result = await courseService.GetAll();

        Assert.NotEmpty(result);
        Assert.True(result.Count == 3);
    }

    [Fact]
    public async Task GetByIdReturnsPersonThatExists()
    {
        var mockDataStore = new Mock<IDataStore<model.Person>>();

        var candidateGuid = Guid.NewGuid();

        var list = new List<model.Person>
        {
            new model.Person() {Id = candidateGuid},
            new model.Person(),
            new model.Person()
        };

        mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<model.Person, bool>>>())).ReturnsAsync(
            (Expression<Func<model.Person, bool>> predicate) => list.AsQueryable().Single(predicate));

        var personService = new PersonService(mockDataStore.Object, mockLogger.Object);

        var result = await personService.Get(candidateGuid);

        Assert.NotNull(result);
        Assert.True(result.Id == candidateGuid);
    }

    [Fact]
    public async Task GetByIdReturnsCourseDoesNotExist()
    {
        var mockDataStore = new Mock<IDataStore<model.Person>>();

        var candidateGuid = Guid.NewGuid();

        var list = new List<model.Person>
        {
            new model.Person() {Id = candidateGuid},
            new model.Person(),
            new model.Person()
        };

        mockDataStore.Setup(d => d.FindAsync(It.IsAny<Expression<Func<model.Person, bool>>>())).ReturnsAsync(
            (Expression<Func<model.Person, bool>> predicate) => list.AsQueryable().SingleOrDefault(predicate));

        var personService = new PersonService(mockDataStore.Object, mockLogger.Object);

        var result = await personService.Get(Guid.NewGuid());

        Assert.Null(result);
    }
}