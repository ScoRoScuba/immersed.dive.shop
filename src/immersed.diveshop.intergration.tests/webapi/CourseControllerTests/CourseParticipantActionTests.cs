using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace immersed.diveshop.intergration.tests.webapi.CourseControllerTests
{
    public class CourseParticipantActionTests : IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
    {
        private readonly HttpClient _client;
        private readonly DiveShopDBContext _dbContext;

        public CourseParticipantActionTests(CustomWebApplicationFactory<CourseControllerStartup> factory)
        {
            _client = factory.CreateClient();

            _dbContext = factory.Services.GetService<DiveShopDBContext>();
        }

        [Fact]
        public async void CanGetParticipantsOnCourse()
        {
            var courseGuid1 = Guid.NewGuid();
            var courseGuid2 = Guid.NewGuid();
            var personGuid1 = Guid.NewGuid();
            var personGuid2 = Guid.NewGuid();
            var personGuid3 = Guid.NewGuid();

            _dbContext.CourseParticipants.Add(new CourseParticipant()
            {
                CourseId = courseGuid1, Course = new Course() {Id = courseGuid1}, ParticipantId = personGuid1,
                Participant = new Person() {Id = personGuid1}
            });
            _dbContext.CourseParticipants.Add(new CourseParticipant()
            {
                CourseId = courseGuid1,
                ParticipantId = personGuid2,
                Participant = new Person() { Id = personGuid2 }
            });
            _dbContext.CourseParticipants.Add(new CourseParticipant()
            {
                CourseId = Guid.NewGuid(),
                Course = new Course() { Id = courseGuid2 },
                ParticipantId = personGuid3,
                Participant = new Person() { Id = personGuid3 }
            });

            _dbContext.SaveChanges();

            var courseResponse = await _client.GetAsync($"courses/{courseGuid1}/participants");

            Assert.True(courseResponse.IsSuccessStatusCode);

            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var participants = JsonConvert.DeserializeObject(contentFromGet, typeof(IList<Person>)) as IList<Person>;

            Assert.Contains(participants, p=>p.Id == personGuid1);
            Assert.Contains(participants, p => p.Id == personGuid2);
        }

    }
}
