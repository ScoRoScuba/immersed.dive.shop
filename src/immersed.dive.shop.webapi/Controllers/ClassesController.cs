using System;
using System.Threading.Tasks;
using immersed.dive.shop.model;
using Microsoft.AspNetCore.Mvc;

namespace immersed.dive.shop.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Class classInstance)
        {
            classInstance.DateCreated = DateTime.UtcNow;
            classInstance.LastUpdated = DateTime.UtcNow;
            
            var actionName = nameof(Get);
            var routeValues = new { id = classInstance.Id };
            return CreatedAtAction(actionName, routeValues, classInstance);            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid ID)
        {
            return Ok();
        }
    }
}
