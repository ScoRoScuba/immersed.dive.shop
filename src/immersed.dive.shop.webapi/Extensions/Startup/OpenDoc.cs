using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Filters;

namespace immersed.dive.shop.webapi.Extensions.Startup;

public static class OpenDoc
{
    public static void AddOpenDoc(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.OperationFilter<AddResponseHeadersFilter>();
            c.EnableAnnotations();
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    public static void UseOpenDoc(this WebApplication app, IConfiguration configuration)
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }
}