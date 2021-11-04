using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public class PersonControllerTest
    {
        [Fact]
        public async Task POST_ReturnsAcceptedResultWithUrl()
        {
            var mockService = new Mock<IPersonService>();

            var controller = new PersonController(mockService.Object);

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

            var controller = new PersonController(mockService.Object);

            var result = await controller.Get();

            var okObjectResult = result as OkObjectResult;

            _ = IsAssignableFrom<IList<Person>>(okObjectResult.Value);
        }

        [Fact]
        public async Task GET_ReturnsNotFoundWhenIncorrectIdUsed()
        {
            var mockService = new Mock<IPersonService>();

            mockService.Setup(g => g.Get(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var controller = new PersonController(mockService.Object);

            var result = await controller.Get(Guid.Empty);

            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
        }
    }
}

