using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class DatabasesExtension
{
    internal static IServiceCollection AddPostgresContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        
        services.AddDbContextPool<PostgresContext>(options => options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

        return services;
    }
}