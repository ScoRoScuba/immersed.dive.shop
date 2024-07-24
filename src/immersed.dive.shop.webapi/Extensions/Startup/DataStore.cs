using System;
using System.Linq;
using System.Threading.Tasks;
using immersed.dive.shop.repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace immersed.dive.shop.webapi.Extensions.Startup;

public static class DataStore
{
    public static void AddDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DiveShopDBContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            options.UseLazyLoadingProxies();
            options.UseSqlServer(connectionString);
        });            
    }

    public static async Task RunMigrations(IServiceProvider appServices, string[] args)
    {
        using var scope = appServices.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DiveShopDBContext>();

            await context.Database.MigrateAsync();

            if (args.Length > 0)
            {
                if(args.Contains("reseed")){
                    await services.SeedData(context);
                }
            }
        }

        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "error occurred during migration");
        }        
    }
}