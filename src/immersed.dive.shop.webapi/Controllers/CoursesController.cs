using System;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController :  ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _courseService.GetAll();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var course = await _courseService.Get(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Course course)
        {
            await _courseService.Add(course);

            return Created(new Uri($"/courses/{course.Id}", UriKind.Relative), null);
        }
    }
}
