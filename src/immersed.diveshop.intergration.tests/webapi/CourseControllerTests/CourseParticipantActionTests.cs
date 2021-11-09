﻿using System;
using System.Collections;
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

namespace immersed.diveshop.intergration.tests.webapi.CourseControllerTests
{
    public class CourseParticipantActionTests : IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
    {
        private readonly HttpClient _client;
        private readonly DiveShopDBContext _dbContext;

        private Guid courseGuid1 = Guid.NewGuid();
        private Guid courseGuid2 = Guid.NewGuid();
        private Guid personGuid1 = Guid.NewGuid();
        private Guid personGuid2 = Guid.NewGuid();
        private Guid personGuid3 = Guid.NewGuid();

        public CourseParticipantActionTests(CustomWebApplicationFactory<CourseControllerStartup> factory)
        {
            _client = factory.CreateClient();

            _dbContext = factory.Services.GetService<DiveShopDBContext>();

            _dbContext.CourseParticipants.Add(new CourseParticipant()
            {
                CourseId = courseGuid1,
                Course = new Course() { Id = courseGuid1 },
                ParticipantId = personGuid1,
                Participant = new Person() { Id = personGuid1 }
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
        }

        [Fact]
        public async void CanGetParticipantsOnCourse()
        {
            var courseResponse = await _client.GetAsync($"courses/{courseGuid1}/participants");

            Assert.True(courseResponse.IsSuccessStatusCode);

            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var participants = JsonConvert.DeserializeObject(contentFromGet, typeof(IList<Person>)) as IList<Person>;

            Assert.Contains(participants, p=>p.Id == personGuid1);
            Assert.Contains(participants, p => p.Id == personGuid2);
        }

        [Fact]
        public async void AddingParticipantToCourseReturnsCourseParticpantURI()
        {
            var testPersonGuid = Guid.NewGuid();
            var jsonPayload = JsonConvert.SerializeObject(testPersonGuid);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"courses/{courseGuid1}/participants", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.Created); 
            Assert.NotNull(response.Headers.Location);
        }

        [Fact]
        public async void CanGetParticipantOnCourseReturnsCourseParticpant()
        {
            var testPersonGuid = Guid.NewGuid();
            var jsonPayload = JsonConvert.SerializeObject(testPersonGuid);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"courses/{courseGuid1}/participants", content);

            Assert.True(response.IsSuccessStatusCode);
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(response.Headers.Location);

            var courseResponse = await _client.GetAsync(response.Headers.Location);

            Assert.True(response.IsSuccessStatusCode);
            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var courseParticipant = JsonConvert.DeserializeObject(contentFromGet, typeof(CourseParticipantDto)) as CourseParticipantDto;

            Assert.True(courseParticipant.CourseId == courseGuid1);
            Assert.True(courseParticipant.ParticipantId  == testPersonGuid);
        }
    }
}
