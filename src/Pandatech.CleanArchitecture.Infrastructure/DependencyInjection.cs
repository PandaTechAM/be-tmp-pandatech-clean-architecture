using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
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
         .AddPandaCryptoAndFilters()
         .AddRedisCache()
         .AddRepositories()
         .AddHealthChecks();

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
