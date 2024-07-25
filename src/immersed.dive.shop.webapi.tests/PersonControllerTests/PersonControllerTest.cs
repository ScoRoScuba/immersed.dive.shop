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

namespace immersed.dive.shop.webapi.tests.PersonControllerTests;

public class PersonControllerTest
{
    private Mock<ILogger> mockLogger = new ();
    private IMapper mapper;

    public PersonControllerTest()
    {
        var mapperConfiguration = new MapperConfiguration(conf =>
        {
            conf.AddProfile(new MappingProfiles());
        });

        mapper = new AutoMapper.Mapper(mapperConfiguration);
    }

    [Fact]
    public async Task POST_ReturnsAcceptedResultWithUrl()
    {
        var mockService = new Mock<IPersonService>();

        var controller = new PersonController(mockService.Object, new Mock<IMapper>().Object, mockLogger.Object);

        var person = new Person();

        var result = await controller.Post(person);

        var createdObjectResult = result as CreatedResult;

        EndsWith(person.Id.ToString(), createdObjectResult.Location);
    }

    [Fact]
    public async Task GET_ReturnsOkResultWithPerson()
    {
        var mockService = new Mock<IPersonService>();

        mockService.Setup(g => g.GetAll()).ReturnsAsync(new List<Person>
        {
            new(),
            new()
        });

        var controller = new PersonController(mockService.Object, mapper, mockLogger.Object);

        var result = await controller.Get();

        var okObjectResult = result as OkObjectResult;

        _ = IsAssignableFrom<IList<PersonDto>>(okObjectResult.Value);
    }

    [Fact]
    public async Task GET_ReturnsNotFoundWhenIncorrectIdUsed()
    {
        var mockService = new Mock<IPersonService>();

        mockService.Setup(g => g.Get(It.IsAny<Guid>())).ReturnsAsync(() => null);

        var controller = new PersonController(mockService.Object, new Mock<IMapper>().Object, mockLogger.Object);

        var result = await controller.Get(Guid.Empty);

        var notFoundResult = result as NotFoundResult;
        Assert.NotNull(notFoundResult);
    }
}