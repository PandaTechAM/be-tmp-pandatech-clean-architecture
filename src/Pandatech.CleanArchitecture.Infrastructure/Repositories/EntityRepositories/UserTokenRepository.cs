using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserTokenRepository(PostgresContext postgresContext)
   : BaseRepository<UserTokenEntity>(postgresContext), IUserTokenRepository
{
   public async Task<List<UserTokenEntity>> GetAllUserTokensByUserIdExceptCurrentAsync(long userId, long userTokenId,
      CancellationToken cancellationToken = default)
   {
      return await Context.UserTokens
         .Where(x => x.UserId == userId && x.Id != userTokenId)
         .ToListAsync(cancellationToken);
   }

   public async Task<List<UserTokenEntity>> GetAllUserTokensByUserIdWhichAreNotExpiredAsync(long userId, CancellationToken cancellationToken = default)
   {
      var now = DateTime.UtcNow;
      return await Context.UserTokens
         .Where(x =>
            x.UserId == userId
            && (x.AccessTokenExpiresAt >= now || x.RefreshTokenExpiresAt >= now))
         .ToListAsync(cancellationToken: cancellationToken);
   }

   public async Task<UserTokenEntity?> GetUserTokenByRefreshTokenAsync(byte[] refreshTokenHash, CancellationToken cancellationToken = default)
   {
      return await Context.UserTokens
         .Include(ut => ut.User)
         .FirstOrDefaultAsync(x => x.RefreshTokenHash == refreshTokenHash, cancellationToken);
   }

   public async Task<UserTokenEntity?> GetUserTokenByAccessTokenAsync(byte[] accessTokenHash, CancellationToken cancellationToken = default)
   {
      return await Context.UserTokens
         .Include(ut => ut.User)
         .Where(t => t.AccessTokenHash == accessTokenHash)
         .AsNoTracking()
         .FirstOrDefaultAsync(cancellationToken);
   }
}
