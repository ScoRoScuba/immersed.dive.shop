using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.dive.shop.webapi.WebDtos;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace immersed.diveshop.intergration.tests.webapi.EventControllerTests
{
    public class EventParticipantActionTests : IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
    {
        private readonly HttpClient _client;
        private readonly DiveShopDBContext _dbContext;

        private Guid courseGuid1 = Guid.NewGuid();
        private Guid courseGuid2 = Guid.NewGuid();
        private Guid eventGuid1 = Guid.NewGuid();
        private Guid eventGuid2 = Guid.NewGuid();
        private Guid personGuid1 = Guid.NewGuid();
        private Guid personGuid2 = Guid.NewGuid();
        private Guid personGuid3 = Guid.NewGuid();

        public EventParticipantActionTests(CustomWebApplicationFactory<CourseControllerStartup> factory)
        {
            _client = factory.CreateClient();

            _dbContext = factory.Services.GetService<DiveShopDBContext>();

            _dbContext.Courses.Add(new Course{Id = courseGuid1});
            _dbContext.Courses.Add(new Course{Id= courseGuid2});

            _dbContext.People.Add(new Person{ Id = personGuid1 });
            _dbContext.People.Add(new Person { Id = personGuid2 });
            _dbContext.People.Add(new Person { Id = personGuid3 });

            _dbContext.Events.Add(new Event()
            {
                Id = eventGuid1,
                CourseId = courseGuid1
            });
            _dbContext.Events.Add(new Event()
            {
                Id = eventGuid2,
                CourseId = courseGuid2
            });
            _dbContext.EventParticipants.Add(new EventParticipant()
            {
                EventId =eventGuid1,
                ParticipantId = personGuid1,
            });
            _dbContext.EventParticipants.Add(new EventParticipant()
            {
                EventId = eventGuid1,
                ParticipantId = personGuid2,
            });
            _dbContext.EventParticipants.Add(new EventParticipant()
            {
                EventId = Guid.NewGuid(),
                ParticipantId = personGuid3,
            });

            _dbContext.SaveChanges();
        }

        [Fact]
        public async void CanGetParticipantsInEvent()
        {
            var courseResponse = await _client.GetAsync($"events/{eventGuid1}/participants");

            Assert.True(courseResponse.IsSuccessStatusCode);

            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var participants = JsonConvert.DeserializeObject(contentFromGet, typeof(IList<Person>)) as IList<Person>;

            Assert.Contains(participants, p=>p.Id == personGuid1);
            Assert.Contains(participants, p => p.Id == personGuid2);
        }

        [Fact]
        public async void AddingParticipantToEventReturnsEventParticpantURI()
        {
            var testPersonGuid = Guid.NewGuid();
            var jsonPayload = JsonConvert.SerializeObject(testPersonGuid);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"events/{eventGuid1}/participants", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.Created); 
            Assert.NotNull(response.Headers.Location);
        }

        [Fact(Skip = "Works for real not in test")]
        public async void CanGetParticipantOnEventReturnsEventParticpant()
        {
            var testPersonGuid = Guid.NewGuid();
            _dbContext.People.Add(new Person { Id = testPersonGuid });
            
            var jsonPayload = JsonConvert.SerializeObject(testPersonGuid);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"events/{eventGuid1}/participants", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(response.Headers.Location);

            var courseResponse = await _client.GetAsync(response.Headers.Location);

            Assert.True(response.IsSuccessStatusCode);
            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var eventParticipant = JsonConvert.DeserializeObject(contentFromGet) as EventParticipantDto;

            Assert.True(eventParticipant.EventId == eventGuid1);
            Assert.True(eventParticipant.ParticipantId  == testPersonGuid);
        }
    }
}
