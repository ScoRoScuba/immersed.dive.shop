﻿using System;
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
    [ApiController]
    [Route("[controller]")]
    public class CoursesController :  ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CoursesController(ICourseService courseService, IMapper mapper, ILogger logger)
        {
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
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
