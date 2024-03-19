using Microsoft.AspNetCore.Builder;
using Pandatech.CleanArchitecture.Infrastructure.Extensions;
using Pandatech.CleanArchitecture.Infrastructure.Seed.User;

namespace Pandatech.CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
   public static WebApplicationBuilder AddInfrastructureLayer(this WebApplicationBuilder builder)
   {
      builder.AddSerilog()
         .AddHangfireServer()
         .AddPostgresContext()
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
