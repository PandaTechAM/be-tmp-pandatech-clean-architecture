using Core.Entities;
using Core.Redis;
using Infrastructure.Settings.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Infrastructure.Redis.Base;

internal class TestRedisCacheService : BaseRedisCacheService<TestEntity>, ITestCacheService
{
    private const string DbName = "test";

    public TestRedisCacheService(
        IRedisClient redisClient,
        IOptions<RedisCacheConfigurations> options
        ) : base(redisClient, options.Value, DbName)
    {
    }
}