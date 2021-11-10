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

            CreateMap<Course, CourseDto>()
                    .ForMember(dest => dest.Participants, opt => opt.Ignore());

            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Participants, opt => opt.Ignore());

            CreateMap<EventDate, EventDateDto>();

            CreateMap<EventParticipant, EventParticipantDto>();
        }
    }
}
