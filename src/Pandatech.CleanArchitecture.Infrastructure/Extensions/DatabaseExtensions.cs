﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class DatabaseExtensions
{
   public static WebApplicationBuilder AddPostgresContext(this WebApplicationBuilder builder)
   {
      var configuration = builder.Configuration;

      var connectionString = configuration.GetConnectionString(ConfigurationPaths.PostgresUrl);
      builder.Services.AddDbContextPool<PostgresContext>(options =>
         options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
      return builder;
   }

   public static WebApplication MigrateDatabase(this WebApplication app)
   {
      using var scope = app.Services.CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<PostgresContext>();
      dbContext.Database.Migrate();
      return app;
   }
}
