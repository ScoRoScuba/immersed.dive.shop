﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using immersed.dive.shop.model;

namespace immersed.dive.shop.domain.interfaces;

public interface ICourseService
{
    Task<Course> Get(Guid id);
    Task Add(Course course);
    Task<IList<Course>> GetAll();
}