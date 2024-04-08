using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class HangfireServerExtensions
{
   public static WebApplicationBuilder AddHangfireServer(this WebApplicationBuilder builder)
   {
      var postgresConnectionString = builder.Configuration.GetConnectionString(ConfigurationPaths.PostgresUrl);
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
