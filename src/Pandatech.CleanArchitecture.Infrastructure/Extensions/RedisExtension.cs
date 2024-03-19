using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pandatech.CleanArchitecture.Core.Extensions;
using Pandatech.CleanArchitecture.Core.Interfaces.Redis.EntityCacheServices;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using StackExchange.Redis.Extensions.Protobuf;

namespace Pandatech.CleanArchitecture.Infrastructure.Extensions;

internal static class RedisExtension
{
   internal static WebApplicationBuilder AddRedisCache(this WebApplicationBuilder builder)
   {
      var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
      builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString!));

      var redisConfiguration = new RedisConfiguration { ConnectionString = redisConnectionString };

      if (builder.Environment.IsLocalOrDevelopmentOrQa())
      {
         builder.Services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
      }
      else
      {
         builder.Services.AddStackExchangeRedisExtensions<ProtobufSerializer>(redisConfiguration);
      }


      builder.Services.AddSingleton<ILoggedUserCacheService, ILoggedUserCacheService>();

      return builder;
   }
}
