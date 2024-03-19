using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserRepository(PostgresContext postgresContext)
   : BaseRepository<UserEntity>(postgresContext), IUserRepository
{
   public async Task<bool> IsUsernameDuplicateAsync(string username, CancellationToken cancellationToken = default)
   {
      return await Context.Users.AnyAsync(x => x.Username == username, cancellationToken);
   }

   public async Task<List<UserEntity>> GetByIdsAsync(List<long> ids, CancellationToken cancellationToken = default)
   {
      return await Context.Users
         .Where(x => ids.Contains(x.Id))
         .ToListAsync(cancellationToken);
   }

   public async Task<UserEntity?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
   {
      return await Context.Users
         .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
   }
}
