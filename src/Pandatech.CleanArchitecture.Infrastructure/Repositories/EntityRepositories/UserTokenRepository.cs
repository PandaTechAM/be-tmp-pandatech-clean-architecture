using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class UserTokenRepository(PostgresContext postgresContext)
   : BaseRepository<Token>(postgresContext), IUserTokenRepository
{
   public Task<List<Token>> GetAllUserTokensByUserIdExceptCurrentAsync(long userId, long userTokenId,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Where(x => x.UserId == userId && x.Id != userTokenId)
         .ToListAsync(cancellationToken);
   }

   public Task<List<Token>> GetAllUserTokensByUserIdWhichAreNotExpiredAsync(long userId,
      CancellationToken cancellationToken = default)
   {
      var now = DateTime.UtcNow;

      return Context.Tokens
         .Where(x =>
            x.UserId == userId
            && (x.AccessTokenExpiresAt >= now || x.RefreshTokenExpiresAt >= now))
         .ToListAsync(cancellationToken);
   }

   public Task<Token?> GetUserTokenByRefreshTokenAsync(byte[] refreshTokenHash,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Include(ut => ut.User)
         .FirstOrDefaultAsync(x => x.RefreshTokenHash == refreshTokenHash, cancellationToken);
   }

   public Task<Token?> GetUserTokenByAccessTokenAsync(byte[] accessTokenHash,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Include(ut => ut.User)
         .Where(t => t.AccessTokenHash == accessTokenHash)
         .AsNoTracking()
         .FirstOrDefaultAsync(cancellationToken);
   }
}
