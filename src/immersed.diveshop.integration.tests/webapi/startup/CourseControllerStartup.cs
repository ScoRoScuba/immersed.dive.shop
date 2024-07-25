using immersed.dive.shop.application;
using immersed.dive.shop.application.Courses;
using immersed.dive.shop.application.Person;
using immersed.dive.shop.domain.interfaces;
using immersed.dive.shop.domain.interfaces.Data;
using immersed.dive.shop.model;
using immersed.dive.shop.repository;
using immersed.dive.shop.repository.Criteria;
using immersed.dive.shop.webapi.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace immersed.diveshop.intergration.tests.webapi.startup;

public class CourseControllerStartup
{
    private IConfiguration _configuration { get; }
    public CourseControllerStartup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureContainer(IServiceCollection services)
    {
        services.AddTransient<IDataStore<Course>, CoursesStore>();
        services.AddTransient<ICourseService, CourseService>();

        services.AddTransient<IDataStore<Person>, PersonStore>();
        services.AddTransient<IPersonService, PersonService>();

        services.AddTransient<IEventService, EventService>();
        services.AddTransient<IDataStore<Event>, EventsStore>();

        services.AddTransient<IEventParticipantService, EventParticipantService>();
        services.AddTransient<IDataStore<EventParticipant>, EventParticipantStore>();

        services.AddTransient<IEventDateFilterBuilder, EventDateFilterBuilder>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var assembly = typeof(immersed.dive.shop.webapi.Controllers.CoursesController).Assembly;

        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddControllers()
            .PartManager
            .ApplicationParts
            .Add(new AssemblyPart(assembly));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    } 
}