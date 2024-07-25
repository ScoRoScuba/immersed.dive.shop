using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Xunit;
using System.Text.Json;

namespace immersed.diveshop.intergration.tests.webapi.CourseRequestTests
{
    public class CoursesGetTests :IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
    {
        private readonly HttpClient _client;
        
        private readonly JsonSerializerOptions _jsonSerializationOptions =  new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };        
        public CoursesGetTests(CustomWebApplicationFactory<CourseControllerStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetCourseOnceCreated()
        {
            var postCourse = new Course();

            var jsonPayload = JsonSerializer.Serialize(postCourse);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/courses", content);

            Assert.True(result.IsSuccessStatusCode);
            Assert.True( result.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(result.Headers.Location);

            var courseResponse = await _client.GetAsync(result.Headers.Location);

            Assert.True(result.IsSuccessStatusCode);
            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();
            
            var getCourse  = JsonSerializer.Deserialize<Course>(contentFromGet, _jsonSerializationOptions);
            
            Assert.True(postCourse.Id == getCourse.Id);
        }

        [Fact]
        public async Task Gets404ResultWhenNotFound()
        {
            var candidateGuid = Guid.NewGuid();

            var courseResponse = await _client.GetAsync($"/courses/{candidateGuid}");
            Assert.False(courseResponse.IsSuccessStatusCode);

            Assert.True(courseResponse.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
