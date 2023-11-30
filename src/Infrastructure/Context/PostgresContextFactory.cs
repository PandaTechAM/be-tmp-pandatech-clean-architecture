using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
{
    private static IConfiguration BuildConfiguration(string basePath)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        return new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();
    } // TODO maybe move to common

    private string FindRootDirectoryName(DirectoryInfo baseDir, int depthThreshold = 5)
    {
        var dir = baseDir;
        var depth = 0;

        while (++depth < depthThreshold && dir?.Name != "bin")
        {
            dir = dir?.Parent;
        }

        return dir?.Parent?.FullName;
    } // TODO maybe move to common

    public PostgresContext CreateDbContext(string[] args)
    {
        var path = FindRootDirectoryName(Directory.GetParent(AppContext.BaseDirectory)!);
        var config = BuildConfiguration(path);

        var connectionString = config.GetConnectionString("Postgres");

        var optionsBuilder = new DbContextOptionsBuilder<PostgresContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new PostgresContext(optionsBuilder.Options);
    }
}