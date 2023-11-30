using Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPostgresContext(configuration)
            .MigrateDatabase()
            .AddRedisCache(configuration)
            .AddRepositories();

        return services;
    }
}