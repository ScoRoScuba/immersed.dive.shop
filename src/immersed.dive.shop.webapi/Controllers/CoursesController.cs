using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers
{
    [ApiController]
    [Route("{controller}")]
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
    }
}
