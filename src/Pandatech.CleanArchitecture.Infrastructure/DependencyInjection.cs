using BaseConverter;
using Communicator.Extensions;
using DistributedCache.Extensions;
using GridifyExtensions.Extensions;
using MassTransit.PostgresOutbox.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;
using Pandatech.CleanArchitecture.Infrastructure.Seed.User;

namespace Pandatech.CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddInfrastructureLayer(this WebApplicationBuilder builder)
   {
      AssemblyRegistry.AddAssemblies(typeof(DependencyInjection).Assembly);

      builder.AddSerilog()
         .AddHangfireServer()
         .AddPostgresContext()
         .ConfigureOpenTelemetry()
         .AddPandaCrypto()
         .AddGridify(PandaBaseConverter.Base36Chars)
         .AddRepositories()
         .AddCommunicator()
         .AddDistributedCache(options =>
         {
            options.RedisConnectionString = builder.Configuration.GetConnectionString(ConfigurationPaths.RedisUrl)!;
         })
         .AddHealthChecks();

      builder.Services.AddOutboxInboxServices<PostgresContext>();

      return builder;
   }

   public static WebApplication UserInfrastructureLayer(this WebApplication app)
   {
      app.MigrateDatabase()
         .EnsureHealthy()
         .UseHangfireServer()
         .SeedSystemUser();
      return app;
   }
}
