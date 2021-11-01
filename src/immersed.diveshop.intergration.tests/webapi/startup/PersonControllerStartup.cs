using Autofac;
using immersed.dive.shop.application;
using immersed.dive.shop.repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace immersed.diveshop.intergration.tests.webapi.startup
{
    public class PersonControllerStartup
    {
        private IConfiguration _configuration { get; }
        public PersonControllerStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<PersonService>().AsImplementedInterfaces();
            builder.RegisterType<PersonStore>().AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(immersed.dive.shop.webapi.Controllers.PersonController).Assembly;
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
}
