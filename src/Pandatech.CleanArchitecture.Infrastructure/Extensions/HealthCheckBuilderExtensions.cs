﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pandatech.CleanArchitecture.Core.Extensions;
using RabbitMQ.Client;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

public static class HealthCheckBuilderExtensions
{
   public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
   {
      var configuration = builder.Configuration;
      var timeoutSeconds = TimeSpan.FromSeconds(5);
      var postgresConnectionString = configuration.GetConnectionString("Postgres")!;
      var redisConnectionString = configuration.GetConnectionString("Redis")!;
      var elasticSearchUrl = configuration.GetConnectionString("ElasticSearch")!;
      var rabbitMqUri = configuration["RabbitMqSettings:RabbitMqHost"]!;
  //    var auditTrailUrl = new CustomHealthChecks(configuration.GetConnectionString("AuditTrail")!);

      //This part is only for RMQ health check
      ConnectionFactory factory = new() { Uri = new Uri(rabbitMqUri) };
      var connection = factory.CreateConnection();


      if (builder.Environment.IsLocal())
      {
         builder.Services
            .AddSingleton(connection)
            .AddHealthChecks()
            .AddRabbitMQ(name: "rabbit_mq")
            .AddNpgSql(postgresConnectionString, timeout: timeoutSeconds, name: "postgres")
            .AddRedis(redisConnectionString, timeout: timeoutSeconds);
      }

      else if (builder.Environment.IsProduction())
      {
         builder.Services
            .AddSingleton(connection)
            .AddHealthChecks()
            .AddNpgSql(postgresConnectionString, timeout: timeoutSeconds, name: "postgres")
            .AddRedis(redisConnectionString, timeout: timeoutSeconds)
            .AddElasticsearch(elasticSearchUrl, timeout: timeoutSeconds)
            .AddRabbitMQ();
      }
      else
      {
         builder.Services
            .AddSingleton(connection)
            .AddHealthChecks()
            .AddNpgSql(postgresConnectionString, timeout: timeoutSeconds, name: "postgres")
            .AddRedis(redisConnectionString, timeout: timeoutSeconds)
            .AddElasticsearch(elasticSearchUrl, timeout: timeoutSeconds)
            .AddRabbitMQ();
      }

      return builder;
   }
}