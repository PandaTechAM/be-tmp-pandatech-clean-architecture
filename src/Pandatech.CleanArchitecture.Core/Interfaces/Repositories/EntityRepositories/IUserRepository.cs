using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
   Task<bool> IsUsernameDuplicateAsync(string username, CancellationToken cancellationToken = default);
   Task<List<UserEntity>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken = default);

   Task<UserEntity?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
