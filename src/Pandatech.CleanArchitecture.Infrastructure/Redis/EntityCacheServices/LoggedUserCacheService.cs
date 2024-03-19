using Pandatech.CleanArchitecture.Core.Interfaces.Redis.EntityCacheServices;
using Pandatech.CleanArchitecture.Core.RedisEntities;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Pandatech.CleanArchitecture.Infrastructure.Redis.EntityCacheServices;

public class LoggedUserCacheService(IRedisClient redisClient)
   : BaseRedisCacheService<LoggedUserRedisEntity>(redisClient), ILoggedUserCacheService;
