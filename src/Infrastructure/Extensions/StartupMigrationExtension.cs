using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class StartupMigrationExtension
{
    internal static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<PostgresContext>();
        context.Database.Migrate();
        return services;
    }
}