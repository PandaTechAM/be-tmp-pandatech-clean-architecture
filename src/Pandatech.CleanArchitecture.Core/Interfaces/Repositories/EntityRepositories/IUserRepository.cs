using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserRepository : IBaseRepository<User>
{
   Task<bool> IsUsernameDuplicate(string username, CancellationToken cancellationToken = default);
   Task<List<User>> GetByIdsExceptSuper(List<long> ids, CancellationToken cancellationToken = default);
   
   IQueryable<User> WhereNotSuperAdmin();

   Task<User?> GetByUsername(string username, CancellationToken cancellationToken = default);
   Task Delete(string requestFilter, long identityUserId, CancellationToken cancellationToken);
}