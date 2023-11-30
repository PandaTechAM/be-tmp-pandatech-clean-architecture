using Asp.Versioning;

namespace WebApi.Extensions;

internal static class ApiVersioningExtension
{
    internal static IServiceCollection AddApiVersioningFromHeader(this IServiceCollection services)
    { 
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
        }).AddMvc();

        return services;
    }
}