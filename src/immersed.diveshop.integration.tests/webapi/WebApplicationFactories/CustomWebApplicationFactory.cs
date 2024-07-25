using System;
using immersed.dive.shop.repository;
using immersed.dive.shop.webapi.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Serilog.ILogger;

namespace immersed.diveshop.intergration.tests.webapi.WebApplicationFactories;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    private InMemoryDatabaseRoot _databaseRoot;
    private InMemoryDatabaseRoot DatabaseRoot => _databaseRoot ??= new InMemoryDatabaseRoot();

    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(x =>
            {
                x.UseStartup<TStartup>().UseTestServer();
            });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
                
            services.AddDbContext<DiveShopDBContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseInMemoryDatabase("InMemoryAppDb", DatabaseRoot);
            });                 

            services.AddSingleton(new Mock<ILogger>().Object);
        });
    }
}