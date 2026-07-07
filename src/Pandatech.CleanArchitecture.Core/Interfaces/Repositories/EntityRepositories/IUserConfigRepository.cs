using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserConfigRepository : IBaseRepository<UserConfig>
{
    Task<List<UserConfig>> GetByUserIdAndKeys(long identityUserId,
        List<string> keys,
        CancellationToken cancellationToken);

    Task<Dictionary<string, string>> GetByUserIdAndKeysAsNotTrackingToDict(long identityUserId,
        string[] requestKeys,
        CancellationToken cancellationToken);
}
