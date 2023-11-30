using Core.Redis;
using Infrastructure.Redis.Base;
using Infrastructure.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Infrastructure.Extensions;

internal static class RedisExtension
{
    internal static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisSettingsConfig = configuration.GetSection("Redis");

        var redisSettings = redisSettingsConfig.Get<RedisCacheConfigurations>();

        services.Configure<RedisCacheConfigurations>(redisSettingsConfig);
        
        services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(_ =>
        {
            return new[] { redisSettings!.Configuration };
        });

        services.AddSingleton<ITestCacheService, TestRedisCacheService>();

        return services;
    }
}