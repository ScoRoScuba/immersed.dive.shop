using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController :  ControllerBase
    {
        public CoursesController() { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = new List<Course>()
            {
                new Course(),
                new Course()
            };

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var course = new Course()
                {Id = id};

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Course course)
        {
            return Created(new Uri($"/courses/{course.Id}", UriKind.Relative), null);
        }
    }
}
