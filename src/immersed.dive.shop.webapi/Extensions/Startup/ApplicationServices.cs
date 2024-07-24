using immersed.dive.shop.application;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.application.Person;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.dive.shop.repository.Criteria;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using immersed.dive.shop.webapi.Core;

namespace immersed.dive.shop.webapi.Extensions.Startup
{
    public static class ApplicationServices
    {
        public static void AddServices(this IServiceCollection containerBuilder, IConfiguration configuration)
        {
            containerBuilder.AddTransient<IDataStore<Course>, CoursesStore>();
            containerBuilder.AddTransient<ICourseService, CourseService>();

            containerBuilder.AddTransient<IDataStore<Person>, PersonStore>();
            containerBuilder.AddTransient<IPersonService, PersonService>();

            containerBuilder.AddTransient<IEventService, EventService>();
            containerBuilder.AddTransient<IDataStore<Event>, EventsStore>();

            containerBuilder.AddTransient<IEventParticipantService, EventParticipantService>();
            containerBuilder.AddTransient<IDataStore<EventParticipant>, EventParticipantStore>();

            containerBuilder.AddTransient<IEventDateFilterBuilder, EventDateFilterBuilder>();

            containerBuilder.AddTransient<IDateTimeProvider, DateTimeProvider>();
        }

        public static void AddThirdPartyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        }
    }
}