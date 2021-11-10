using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.WebDtos;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace immersed.dive.shop.webapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EventsController(IEventService eventService, IMapper mapper, ILogger logger)
        {
            _eventService = eventService;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpPost]
        [Route("{eventId:guid}/participants")]
        public async Task<IActionResult> AddParticipantToEvent(Guid eventId, [FromBody] Guid personId)
        {
            var courseParticipantId = await _eventService.AddParticipant(eventId, personId);

            return Created(new Uri($"/events/{eventId}/participants/{courseParticipantId}", UriKind.Relative), null);
        }

        [HttpGet]
        [Route("{eventId:guid}/participants")]
        public async Task<OkObjectResult> GetEventsParticipants(Guid eventId)
        {
            var result = await _eventService
                .GetParticipants(eventId);

            var dtoResult = _mapper.Map<IList<Person>, IList<PersonDto>>(result);

            return Ok(dtoResult);
        }

        [HttpGet]
        [Route("{eventId:guid}/participants/{eventParticipantId:guid}")]
        public async Task<OkObjectResult> GetEventParticipant(Guid eventId, Guid eventParticipantId)
        {
            var result = await _eventService
                .GetEventParticipant(eventId, eventParticipantId);

            var dtoResult = _mapper.Map<EventParticipant, EventParticipantDto>(result);

            return Ok(dtoResult);
        }

    }
}
