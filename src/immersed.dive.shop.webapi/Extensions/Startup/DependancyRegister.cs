using Autofac;
using immersed.dive.shop.application;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.application.Person;
using immersed.dive.shop.repository;
using Microsoft.Extensions.Configuration;

namespace immersed.dive.shop.webapi.Extensions.Startup
{
    public static class DependancyRegister  
    {
        public static ContainerBuilder  RegisterServices(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterType<CoursesStore>().AsImplementedInterfaces();
            containerBuilder.RegisterType<CourseService>().AsImplementedInterfaces();

            containerBuilder.RegisterType<PersonStore>().AsImplementedInterfaces();
            containerBuilder.RegisterType<PersonService>().AsImplementedInterfaces();

            containerBuilder.RegisterType<EventService>().AsImplementedInterfaces();
            containerBuilder.RegisterType<EventsStore>().AsImplementedInterfaces();

            containerBuilder.RegisterType<EventParticipantService>().AsImplementedInterfaces();
            containerBuilder.RegisterType<EventParticipantStore>().AsImplementedInterfaces();

            return containerBuilder;
        }
    }
}
