using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.WebDtos;

namespace immersed.dive.shop.webapi.Core
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Person, PersonDto>()
                    .ForMember(dest => dest.Courses, opt => opt.Ignore());

            CreateMap<Course, CourseDto>();

            CreateMap<CourseParticipant, CourseParticipantDto>();
        }
    }
}
