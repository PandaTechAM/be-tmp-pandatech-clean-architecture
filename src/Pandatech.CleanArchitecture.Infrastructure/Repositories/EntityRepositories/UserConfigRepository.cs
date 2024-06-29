using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserConfigRepository(PostgresContext postgresContext)
   : BaseRepository<UserConfig>(postgresContext), IUserConfigRepository
{
   public Task<List<UserConfig>> GetByUserIdAndKeysAsync(long identityUserId, List<string> keys,
      CancellationToken cancellationToken)
   {
      return Context.UserConfigs
         .Where(x => x.UserId == identityUserId)
         .Where(x => keys.Contains(x.Key))
         .ToListAsync(cancellationToken);
   }

   public Task<Dictionary<string, string>> GetByUserIdAndKeysAsNotTrackingToDictAsync(long identityUserId,
      string[] requestKeys,
      CancellationToken cancellationToken)
   {
      return Context.UserConfigs
         .Where(x => x.UserId == identityUserId)
         .Where(x => requestKeys.Contains(x.Key))
         .AsNoTracking()
         .ToDictionaryAsync(x => x.Key, x => x.Value, cancellationToken);
   }
}
