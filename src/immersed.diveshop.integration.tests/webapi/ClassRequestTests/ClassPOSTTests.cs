using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Xunit;
using FluentAssertions;

namespace immersed.diveshop.intergration.tests.webapi.ClassRequestTests
{
    public class ClassPOSTTests : IClassFixture<CustomWebApplicationFactory<ClassControllerStartup>>
    {
        private readonly HttpClient _client;
        
        private readonly JsonSerializerOptions _jsonSerializationOptions =  new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
       
        public ClassPOSTTests(CustomWebApplicationFactory<ClassControllerStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task POSTGetsA201Result()
        {
            var classInstance = new Class();
            var jsonPayload = JsonSerializer.Serialize(classInstance);
            
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/classes", content);
            
            Assert.True(result.StatusCode == HttpStatusCode.Created);
        }

        [Fact]
        public async Task POSTHasLocationHeader()
        {
            var classInstance = new Class();
            var jsonPayload = JsonSerializer.Serialize(classInstance);
            
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result =  await _client.PostAsync("/classes", content);
            
            Assert.NotNull(result.Headers.Location);
        }

        [Fact]
        public async Task POSTLocationHeaderHasIDOFClass()
        {
            var expectedGuid = Guid.NewGuid();
            var classInstance = new Class(){ Id = expectedGuid };
            var jsonPayload = JsonSerializer.Serialize(classInstance);
            
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result =  await _client.PostAsync("/classes", content);

            result.Headers.Location.ToString().Should().Contain(expectedGuid.ToString());
        }

        [Fact]
        public async Task POSTResponseBodyShouldMatchBodySent()
        {
            var expectedGuid = Guid.NewGuid();
            var classInstance = new Class(){ Id = expectedGuid };
            var jsonPayload = JsonSerializer.Serialize(classInstance);
            
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result =  await _client.PostAsync("/classes", content);

            var jsonBody = await result.Content.ReadAsStringAsync();
            
            var bodyObject  = JsonSerializer.Deserialize<Class>(jsonBody, _jsonSerializationOptions);

            bodyObject.Id.Should().Be(expectedGuid);
        }
    }
}