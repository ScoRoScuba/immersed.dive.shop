using immersed.dive.shop.webapi.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace immersed.diveshop.intergration.tests.webapi.startup;

public class ClassControllerStartup
{
    private IConfiguration _configuration { get; }
    
    public ClassControllerStartup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureContainer(IServiceCollection services)
    {
        
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var assembly = typeof(immersed.dive.shop.webapi.Controllers.ClassesController).Assembly;

        services.AddControllers()
            .PartManager
            .ApplicationParts
            .Add(new AssemblyPart(assembly));        
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }     
}