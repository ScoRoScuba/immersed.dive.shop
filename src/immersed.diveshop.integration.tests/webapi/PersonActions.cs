using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Newtonsoft.Json;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace immersed.diveshop.intergration.tests.webapi;

public class PersonActions : IClassFixture<CustomWebApplicationFactory<PersonControllerStartup>>
{
    private readonly HttpClient _client;
        
    private readonly JsonSerializerOptions _jsonSerializationOptions =  new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };        
    public PersonActions(CustomWebApplicationFactory<PersonControllerStartup> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CanGetPersonOnceCreated()
    {
        var postPerson = new Person();

        var jsonPayload = JsonConvert.SerializeObject(postPerson);

        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var result = await _client.PostAsync("/Person", content);

        Assert.True(result.IsSuccessStatusCode);
        Assert.True(result.StatusCode == HttpStatusCode.Created);
        Assert.NotNull(result.Headers.Location);

        var personResponse = await _client.GetAsync(result.Headers.Location);

        Assert.True(result.IsSuccessStatusCode);
        var contentFromGet = await personResponse.Content.ReadAsStringAsync();

        var getPerson  = JsonSerializer.Deserialize<Person>(contentFromGet, _jsonSerializationOptions);
            
        Assert.True(postPerson.Id == getPerson.Id);
    }

    [Fact]
    public async Task Gets404ResultWhenNotFound()
    {
        var candidateGuid = Guid.NewGuid();

        var personResponse = await _client.GetAsync($"/get/{candidateGuid}");
        Assert.False(personResponse.IsSuccessStatusCode);

        Assert.True(personResponse.StatusCode == HttpStatusCode.NotFound);
    }
}