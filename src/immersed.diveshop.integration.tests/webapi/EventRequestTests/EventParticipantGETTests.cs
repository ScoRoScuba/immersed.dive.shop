using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.dive.shop.webapi.WebDtos;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace immersed.diveshop.intergration.tests.webapi.EventRequestTests;

public class EventParticipantGETTests : IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
{
    private readonly JsonSerializerOptions _jsonSerializationOptions =  new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };        
        
    private readonly HttpClient _client;
    private readonly DiveShopDBContext _dbContext;
        

    private Guid courseGuid1 = Guid.NewGuid();
    private Guid courseGuid2 = Guid.NewGuid();
    private Guid eventGuid1 = Guid.NewGuid();
    private Guid eventGuid2 = Guid.NewGuid();
    private Guid personGuid1 = Guid.NewGuid();
    private Guid personGuid2 = Guid.NewGuid();
    private Guid personGuid3 = Guid.NewGuid();
        
    private Guid eventPersonGuid = Guid.NewGuid();
        

    public EventParticipantGETTests(CustomWebApplicationFactory<CourseControllerStartup> factory)
    {
        _client = factory.CreateClient();

        using (var scope = factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = _dbContext = scopedServices.GetRequiredService<DiveShopDBContext>();

            _dbContext.Courses.Add(new Course { Id = courseGuid1 });
            _dbContext.Courses.Add(new Course { Id = courseGuid2 });

            _dbContext.People.Add(new Person { Id = personGuid1 });
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
                EventId = eventGuid1,
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
                
            _dbContext.People.Add(new Person { Id = eventPersonGuid });

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public async Task CanGetParticipantsInEvent()
    {
        var courseResponse = await _client.GetAsync($"events/{eventGuid1}/participants");

        Assert.True(courseResponse.IsSuccessStatusCode);

        var contentFromGet = await courseResponse.Content.ReadAsStringAsync();
            
        var participants  = JsonSerializer.Deserialize<IList<Person>>(contentFromGet, _jsonSerializationOptions);            

        Assert.Contains(participants, p=>p.Id == personGuid1);
        Assert.Contains(participants, p => p.Id == personGuid2);
    }

    [Fact]
    public async Task AddingParticipantToEventReturnsEventParticpantURI()
    {
        var testPersonGuid = Guid.NewGuid();
        var jsonPayload = JsonSerializer.Serialize(testPersonGuid);

        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"events/{eventGuid1}/participants", content);

        Assert.True(response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == HttpStatusCode.Created); 
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task CanGetParticipantOnEventReturnsEventParticpant()
    {
        var jsonPayload = JsonSerializer.Serialize(eventPersonGuid);

        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"events/{eventGuid1}/participants", content);

        Assert.True(response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        Assert.NotNull(response.Headers.Location);

        var courseResponse = await _client.GetAsync(response.Headers.Location);

        Assert.True(response.IsSuccessStatusCode);
        var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

        var eventParticipant  = JsonSerializer.Deserialize<EventParticipantDto>(contentFromGet, _jsonSerializationOptions);            
            
        Assert.True(eventParticipant.EventId == eventGuid1);
        Assert.True(eventParticipant.ParticipantId  == eventPersonGuid);
    }
}