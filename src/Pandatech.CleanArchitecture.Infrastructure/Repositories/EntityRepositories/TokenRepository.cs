using Microsoft.EntityFrameworkCore;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;
using Pandatech.CleanArchitecture.Infrastructure.Context;

namespace Pandatech.CleanArchitecture.Infrastructure.Repositories.EntityRepositories;

public class TokenRepository(PostgresContext postgresContext)
   : BaseRepository<Token>(postgresContext), ITokenRepository
{
   public Task<List<Token>> GetAllTokensByUserIdExceptCurrentAsync(long userId, long tokenId,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Where(x => x.UserId == userId && x.Id != tokenId)
         .ToListAsync(cancellationToken);
   }

   public Task<List<Token>> GetAllTokensByUserIdWhichAreNotExpiredAsync(long userId,
      CancellationToken cancellationToken = default)
   {
      var now = DateTime.UtcNow;

      return Context.Tokens
         .Where(x =>
            x.UserId == userId
            && (x.AccessTokenExpiresAt >= now || x.RefreshTokenExpiresAt >= now))
         .ToListAsync(cancellationToken);
   }

   public Task<Token?> GetTokenByRefreshTokenAsync(byte[] refreshTokenHash,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Include(ut => ut.User)
         .FirstOrDefaultAsync(x => x.RefreshTokenHash == refreshTokenHash, cancellationToken);
   }

   public Task<Token?> GetTokenByAccessTokenAsync(byte[] accessTokenHash,
      CancellationToken cancellationToken = default)
   {
      return Context.Tokens
         .Include(ut => ut.User)
         .Where(t => t.AccessTokenHash == accessTokenHash)
         .AsNoTracking()
         .FirstOrDefaultAsync(cancellationToken);
   }
}
