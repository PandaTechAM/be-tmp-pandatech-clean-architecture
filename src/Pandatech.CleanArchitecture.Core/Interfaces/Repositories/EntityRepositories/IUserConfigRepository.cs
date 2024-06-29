using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserConfigRepository : IBaseRepository<UserConfig>
{
   Task<List<UserConfig>> GetByUserIdAndKeysAsync(long identityUserId, List<string> keys, CancellationToken cancellationToken);
   Task<Dictionary<string,string>> GetByUserIdAndKeysAsNotTrackingToDictAsync(long identityUserId, string[] requestKeys, CancellationToken cancellationToken);
}
