using Pandatech.CleanArchitecture.Core.RedisEntities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Redis.EntityCacheServices;

public interface ILoggedUserCacheService : ICacheService<LoggedUserRedisEntity>
{
}
