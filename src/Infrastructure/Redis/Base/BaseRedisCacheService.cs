using Core.Redis;
using Infrastructure.Settings.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Infrastructure.Redis.Base;

internal abstract class BaseRedisCacheService<TEntity> : ICacheService<TEntity> where TEntity : class
{
    private readonly IRedisDatabase _redisDatabase;
    private readonly RedisServiceConfigurations _configurations;

    internal BaseRedisCacheService(
        IRedisClient redisClient,
        RedisCacheConfigurations options,
        string service)
    {
        _configurations = options.Databases.First(x => x.ServiceName == service);

        _redisDatabase = redisClient.GetDb(_configurations.Database);
    }

    public async Task SetRecordAsync(string key, TEntity data, TimeSpan? absoluteExpireTime = null,
        bool @override = true)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        if (!@override)
        {
            var exists = await _redisDatabase.ExistsAsync(key);
            if (exists)
            {
                return;
            }
        }

        await _redisDatabase.AddAsync(key, data, 
            absoluteExpireTime ?? TimeSpan.FromSeconds(_configurations.ExpireSeconds ?? 3600));

    }

    public async Task SetRecordsAsync(Dictionary<string, TEntity> data, TimeSpan? absoluteExpireTime = null)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        var dataArray = data.ToArray();
        var allData = new Tuple<string, TEntity>[data.Count];

        for (var i = 0; i < data.Count; i++)
        {
            allData[i] = new Tuple<string, TEntity>(dataArray[i].Key, dataArray[i].Value);
        }

        await _redisDatabase.AddAllAsync(allData,
            absoluteExpireTime ?? TimeSpan.FromSeconds(_configurations.ExpireSeconds ?? 3600));
    }

    public async Task<TEntity> GetRecordAsync(string key)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        return (await _redisDatabase.GetAsync<TEntity>(key))!;
    }

    public async Task<List<TEntity>> GetRecordsAsync(IEnumerable<string> keys)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        HashSet<string> set = new(keys);
        IDictionary<string, TEntity> values = (await _redisDatabase.GetAllAsync<TEntity>(set))!;
        return values.Values.ToList();
    }

    public async Task RemoveRecord(string key)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        await _redisDatabase.RemoveAsync(key);
    }

    public async Task RemoveRecords(IEnumerable<string> keys)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        await _redisDatabase.RemoveAllAsync(keys.ToArray());
    }

    public async Task<IDictionary<string, TEntity>> GetRecordsDictionaryAsync(IEnumerable<string> keys)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        HashSet<string> set = new(keys);
        return (await _redisDatabase.GetAllAsync<TEntity>(set))!;
    }

    public async Task<TEntity> HashGetRecordAsync(string hashKey, string key)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        var value = await _redisDatabase.HashGetAsync<TEntity>(hashKey, key);
        return value!;
    }

    public async Task<IDictionary<string, TEntity>> HashGetRecordAsync(string hashKey, string[] key)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        var value = await _redisDatabase.HashGetAsync<TEntity>(hashKey, key);
        return value!;
    }

    public async Task<IDictionary<string, TEntity>> HashGetRecordsAsync(string hashKey)
    {
        if (!_configurations.Enabled)
        {
            return default!;
        }

        IDictionary<string, TEntity> values = (await _redisDatabase.HashGetAllAsync<TEntity>(hashKey))!;
        return values;
    }

    public async Task HashSetRecordAsync(string hashKey, string key, TEntity data,
        TimeSpan? absoluteExpireTime = null, bool @override = true)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        if (!@override)
        {
            var exists = await _redisDatabase.ExistsAsync(key);

            if (exists)
            {
                return;
            }
        }

        await _redisDatabase.HashSetAsync(hashKey, key, data);

        await _redisDatabase.UpdateExpiryAsync(hashKey,
            absoluteExpireTime ?? TimeSpan.FromSeconds(_configurations.ExpireSeconds ?? 3600));
    }

    public async Task HashSetRecordsAsync(string hashKey, Dictionary<string, TEntity> data,
        TimeSpan? absoluteExpireTime = null)
    {
        if (!_configurations.Enabled)
        {
            return;
        }

        await _redisDatabase.HashSetAsync(hashKey, data);

        await _redisDatabase.UpdateExpiryAsync(hashKey,
            absoluteExpireTime ?? TimeSpan.FromSeconds(_configurations.ExpireSeconds ?? 3600));
    }
}