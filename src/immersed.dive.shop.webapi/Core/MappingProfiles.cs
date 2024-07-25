using System;
using System.Linq;
using AutoMapper;
using immersed.dive.shop.model;
using immersed.dive.shop.webapi.WebDtos;

namespace immersed.dive.shop.webapi.Core;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.Courses, opt => opt.Ignore());

        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.Participants, opt => opt.Ignore());

        CreateMap<Event, EventDto>()
//                .ForMember(dest => dest.Participants, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate,
                opt => opt.MapFrom(src =>
                    src.Dates.Any() ? src.Dates.OrderBy(a => a.Date).FirstOrDefault().Date : src.DateCreated));

        CreateMap<EventDate, EventDateDto>();

        CreateMap<EventParticipant, EventParticipantDto>();
    }
}