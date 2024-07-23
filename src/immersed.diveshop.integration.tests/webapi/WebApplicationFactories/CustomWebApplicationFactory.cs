using System;
using Autofac.Extensions.DependencyInjection;
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

namespace immersed.diveshop.intergration.tests.webapi.WebApplicationFactories
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
    {
        private InMemoryDatabaseRoot _databaseRoot;
        private InMemoryDatabaseRoot DatabaseRoot => _databaseRoot ??= new InMemoryDatabaseRoot();

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<TStartup>().UseTestServer();
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .AddEntityFrameworkProxies()
                        .BuildServiceProvider();

                services.AddAutoMapper(typeof(MappingProfiles).Assembly);

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<DiveShopDBContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseInMemoryDatabase("InMemoryAppDb", DatabaseRoot);
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddSingleton(new Mock<ILogger>().Object);

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<DiveShopDBContext>();

                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                // Ensure the database is created.
                appDb.Database.EnsureCreated();

                try
                {


                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                }
            });
        }
    }
}
