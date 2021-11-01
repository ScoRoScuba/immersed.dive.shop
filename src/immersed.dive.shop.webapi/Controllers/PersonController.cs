using System;
using System.Threading.Tasks;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PersonController: ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            await _personService.Add(person);

            return Created(new Uri($"/person/{person.Id}", UriKind.Relative), null);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _personService.GetAll();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var person = await _personService.Get(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }


    }
}