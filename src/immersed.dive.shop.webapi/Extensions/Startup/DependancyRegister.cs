using System;
using Autofac;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace immersed.dive.shop.webapi.Externals
{
    public static class DependancyRegister  
    {
        public static ContainerBuilder  RegisterServices(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.RegisterType<CoursesStore>().AsImplementedInterfaces();
            containerBuilder.RegisterType<CourseService>().AsImplementedInterfaces();

            return containerBuilder;
        }
    }
}
