using System.Reflection;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Infrastructure.Context;
using Pandatech.CleanArchitecture.Infrastructure.Helpers;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class MassTransitExtension
{
   public static WebApplicationBuilder AddMassTransit(this WebApplicationBuilder builder, params Assembly[] assemblies)
   {
      builder.Services.AddMassTransit(x =>
      {
         x.AddEntityFrameworkOutbox<PostgresContext>(o =>
         {
            o.DuplicateDetectionWindow = TimeSpan.FromMinutes(5);
            o.QueryDelay = TimeSpan.FromMilliseconds(10000);
            o.UsePostgres().UseBusOutbox();
         });

         x.AddConsumers(assemblies);
         x.SetKebabCaseEndpointNameFormatter();


         x.UsingRabbitMq((context, cfg) =>
         {
            cfg.Host(builder.Configuration.GetConnectionString(ConfigurationPaths.RabbitMqUrl));
            cfg.ConfigureEndpoints(context);
         });
      });
      return builder;
   }
}
