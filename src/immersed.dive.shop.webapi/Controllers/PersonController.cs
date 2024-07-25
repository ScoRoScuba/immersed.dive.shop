using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.WebDtos;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace immersed.dive.shop.webapi.Controllers;

[ApiController]
[Route("[controller]")]

public class PersonController: ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public PersonController(IPersonService personService, IMapper mapper, ILogger logger)
    {
        _personService = personService;
        _mapper = mapper;
        _logger = logger;
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

        var result = _mapper.Map<IList<Person>, IList<PersonDto>>(list);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var person = await _personService.Get(id);
        if (person == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<Person, PersonDto>(person);

        return Ok(result);
    }


}