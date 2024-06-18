using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserRepository(PostgresContext postgresContext)
   : BaseRepository<User>(postgresContext), IUserRepository
{
   public Task<bool> IsUsernameDuplicateAsync(string username, CancellationToken cancellationToken = default)
   {
      return Context.Users.AnyAsync(x => x.Username == username, cancellationToken);
   }

   public Task<List<User>> GetByIdsExceptSuperAsync(List<long> ids, CancellationToken cancellationToken = default)
   {
      return Context.Users
         .Where(x => ids.Contains(x.Id))
         .Where(x => x.Role != UserRole.SuperAdmin)
         .ToListAsync(cancellationToken);
   }

   public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
   {
      return Context.Users
         .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
   }
}
