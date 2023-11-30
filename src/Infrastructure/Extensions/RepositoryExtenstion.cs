using Core.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class RepositoryExtenstion
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITestRepository, TestRepository>();

        return services;
    }
}