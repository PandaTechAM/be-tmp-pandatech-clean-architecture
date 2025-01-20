using Communicator.Extensions;
using DistributedCache.Options;
using GridifyExtensions.Extensions;
using MassTransit.PostgresOutbox.Extensions;
using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Seed.User;
using SharedKernel.Extensions;
using SharedKernel.Helpers;
using SharedKernel.Logging;
using SharedKernel.Postgres.Extensions;
using SharedKernel.Resilience;

namespace Pandatech.CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddInfrastructureLayer(this WebApplicationBuilder builder)
   {
      AssemblyRegistry.Add(typeof(AssemblyReference).Assembly);

      builder
         .AddSerilog()
         .AddOpenTelemetry()
         .AddResilienceDefaultPipeline()
         .AddRedis(KeyPrefix.AssemblyNamePrefix)
         .AddDistributedSignalR("DistributedSignalR")
         .AddPostgresContextPool<PostgresContext>(builder.Configuration.GetPostgresUrl())
         .AddMassTransit(AssemblyRegistry.ToArray())
         .AddCommunicator()
         .AddGridify(typeof(DependencyInjection).Assembly)
         .AddHangfireServer()
         .AddRepositories()
         .AddHealthChecks();

      builder.Services.AddOutboxInboxServices<PostgresContext>();

      return builder;
   }

   public static WebApplication MapInfrastructureLayer(this WebApplication app)
   {
      app
         .MigrateDatabase<PostgresContext>()
         .EnsureHealthy()
         .UseHangfireServer()
         .SeedSystemUser();


      return app;
   }
}