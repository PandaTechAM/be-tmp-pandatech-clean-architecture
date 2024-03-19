using Pandatech.CleanArchitecture.Core.Interfaces.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Pandatech.CleanArchitecture.Infrastructure.Redis;

public abstract class BaseRedisCacheService<TEntity> : ICacheService<TEntity> where TEntity : class
{
    private readonly IRedisDatabase _redisDatabase;

    private protected BaseRedisCacheService(IRedisClient redisClient)
    {
        _redisDatabase = redisClient.GetDb(0);
    }

    public async Task SetRecordAsync(string key, TEntity data, TimeSpan? absoluteExpireTime = null,
        bool @override = true)
    {
        if (!@override && await _redisDatabase.ExistsAsync(key))
        {
            return;
        }

        await _redisDatabase.AddAsync(key, data, absoluteExpireTime ?? TimeSpan.FromHours(1));
    }

    public async Task SetRecordsAsync(Dictionary<string, TEntity> data, TimeSpan? absoluteExpireTime = null)
    {
        var dataArray = data.ToArray();
        var allData = new Tuple<string, TEntity>[data.Count];

        for (var i = 0; i < data.Count; i++)
        {
            allData[i] = new Tuple<string, TEntity>(dataArray[i].Key, dataArray[i].Value);
        }

        await _redisDatabase.AddAllAsync(allData,
            absoluteExpireTime ?? TimeSpan.FromHours(1));
    }

    public async Task<TEntity?> GetRecordAsync(string key)
    {
        return await _redisDatabase.GetAsync<TEntity>(key);
    }

    public async Task<List<TEntity>> GetRecordsAsync(IEnumerable<string> keys)
    {
        HashSet<string> set = new(keys);

        var records = await _redisDatabase.GetAllAsync<TEntity>(set);

        var nonNullValues = records.Values
            .Where(value => value != null)
            .Select(value => value!)
            .ToList();

        return nonNullValues;
    }

    public async Task RemoveRecordAsync(string key)
    {
        await _redisDatabase.RemoveAsync(key);
    }

    public async Task RemoveRecordsAsync(IEnumerable<string> keys)
    {
        await _redisDatabase.RemoveAllAsync(keys.ToArray());
    }

    public async Task<IDictionary<string, TEntity>> GetRecordsDictionaryAsync(IEnumerable<string> keys)
    {
        HashSet<string> set = new(keys);

        var records = await _redisDatabase.GetAllAsync<TEntity>(set);

        return records.Where(pair => pair.Value != null)
            .ToDictionary(pair => pair.Key, pair => pair.Value!);
    }

    public async Task<TEntity?> HashGetRecordAsync(string hashKey, string key)
    {
        var value = await _redisDatabase.HashGetAsync<TEntity>(hashKey, key);
        return value;
    }

    public async Task<IDictionary<string, TEntity>> HashGetRecordAsync(string hashKey, string[] key)
    {
        var value = await _redisDatabase.HashGetAsync<TEntity>(hashKey, key);
        return value!;
    }

    public async Task<IDictionary<string, TEntity>> HashGetRecordsAsync(string hashKey)
    {
        IDictionary<string, TEntity> values = (await _redisDatabase.HashGetAllAsync<TEntity>(hashKey))!;
        return values;
    }

    public async Task HashSetRecordAsync(string hashKey, string key, TEntity data,
        TimeSpan? absoluteExpireTime = null, bool @override = true)
    {
        if (!@override && await _redisDatabase.ExistsAsync(key))
        {
            return;
        }

        await _redisDatabase.HashSetAsync(hashKey, key, data);

        await _redisDatabase.UpdateExpiryAsync(hashKey,
            absoluteExpireTime ?? TimeSpan.FromHours(1));
    }

    public async Task HashSetRecordsAsync(string hashKey, Dictionary<string, TEntity> data,
        TimeSpan? absoluteExpireTime = null)
    {
        await _redisDatabase.HashSetAsync(hashKey, data);

        await _redisDatabase.UpdateExpiryAsync(hashKey,
            absoluteExpireTime ?? TimeSpan.FromHours(1));
    }
}
