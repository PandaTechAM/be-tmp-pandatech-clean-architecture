using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserRepository : IBaseRepository<User>
{
   Task<bool> IsUsernameDuplicateAsync(string username, CancellationToken cancellationToken = default);
   Task<List<User>> GetByIdsExceptSuperAsync(List<long> ids, CancellationToken cancellationToken = default);

   Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}
