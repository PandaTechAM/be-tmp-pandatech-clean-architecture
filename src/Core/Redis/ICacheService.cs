namespace Core.Redis;

public interface ICacheService<TEntity>
{
    Task SetRecordAsync(string key, TEntity data, TimeSpan? absoluteExpireTime = null, bool @override = true);
    Task SetRecordsAsync(Dictionary<string, TEntity> data, TimeSpan? absoluteExpireTime = null);
    Task<TEntity> GetRecordAsync(string key);
    Task<List<TEntity>> GetRecordsAsync(IEnumerable<string> keys);
    Task RemoveRecord(string key);
    Task RemoveRecords(IEnumerable<string> keys);
    Task<IDictionary<string, TEntity>> GetRecordsDictionaryAsync(IEnumerable<string> keys);
    Task<TEntity> HashGetRecordAsync(string hashKey, string key);
    Task<IDictionary<string, TEntity>> HashGetRecordAsync(string hashKey, string[] key);
    Task<IDictionary<string, TEntity>> HashGetRecordsAsync(string hashKey);
    Task HashSetRecordAsync(string hashKey, string key, TEntity data, TimeSpan? absoluteExpireTime = null,
        bool @override = true);
    Task HashSetRecordsAsync(string hashKey, Dictionary<string, TEntity> data, TimeSpan? absoluteExpireTime = null);
}
