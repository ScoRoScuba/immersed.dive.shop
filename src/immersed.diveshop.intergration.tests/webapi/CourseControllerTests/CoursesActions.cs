using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using immersed.diveshop.intergration.tests.webapi.startup;
using immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;
using Newtonsoft.Json;
using Xunit;

namespace immersed.diveshop.intergration.tests.webapi.CourseControllerTests
{
    public class CoursesActions :IClassFixture<CustomWebApplicationFactory<CourseControllerStartup>>
    {
        private readonly HttpClient _client;
        public CoursesActions(CustomWebApplicationFactory<CourseControllerStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetCourseOnceCreated()
        {
            var postCourse = new Course();

            var jsonPayload = JsonConvert.SerializeObject(postCourse);

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var result = _client.PostAsync("/courses", content).Result;

            Assert.True(result.IsSuccessStatusCode);
            Assert.True( result.StatusCode == HttpStatusCode.Created);
            Assert.NotNull(result.Headers.Location);

            var courseResponse = await _client.GetAsync(result.Headers.Location);

            Assert.True(result.IsSuccessStatusCode);
            var contentFromGet = await courseResponse.Content.ReadAsStringAsync();

            var getCourse = JsonConvert.DeserializeObject(contentFromGet, typeof(Course)) as Course;

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
