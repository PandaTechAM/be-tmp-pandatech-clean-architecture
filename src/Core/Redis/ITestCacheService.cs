using Core.Entities;

namespace Core.Redis;

public interface ITestCacheService : ICacheService<TestEntity>
{
    
}