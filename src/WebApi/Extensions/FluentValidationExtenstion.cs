using System.Reflection;
using FluentValidation;
using WebApi.Attributes;

namespace WebApi.Extensions;

internal static class FluentValidationExtenstion
{
    private static readonly Assembly ApplicationLayerAssembly = AppDomain.CurrentDomain.Load("Application");

    internal static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationLayerAssembly);
        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(ValidatorModelFilterAttribute));
        });

        return services;
    }
}