using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface ITokenRepository : IBaseRepository<Token>
{
   public Task<List<Token>> GetAllTokensByUserIdExceptCurrentAsync(long userId, long tokenId,
      CancellationToken cancellationToken = default);

   public Task<List<Token>> GetAllTokensByUserIdWhichAreNotExpiredAsync(long userId,
      CancellationToken cancellationToken = default);

   public Task<Token?> GetTokenByRefreshTokenAsync(byte[] refreshTokenHash,
      CancellationToken cancellationToken = default);

   public Task<Token?> GetTokenByAccessTokenAsync(byte[] accessTokenHash,
      CancellationToken cancellationToken = default);
}
