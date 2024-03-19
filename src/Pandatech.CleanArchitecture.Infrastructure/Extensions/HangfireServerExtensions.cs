using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class HangfireServerExtensions
{
  public static WebApplicationBuilder AddHangfireServer(this WebApplicationBuilder builder)
  {
    var postgresConnectionString = builder.Configuration.GetConnectionString("Postgres");
    builder.Services.AddHangfire(configuration =>
    {
      configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
      configuration.UseSimpleAssemblyNameTypeSerializer();
      configuration.UseRecommendedSerializerSettings();
      configuration.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(postgresConnectionString));
    });

    builder.Services.AddHangfireServer(options =>
    {
      options.WorkerCount = 5;
    });
    return builder;
  }
}
