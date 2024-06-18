using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserTokenRepository : IBaseRepository<Token>
{
   public Task<List<Token>> GetAllUserTokensByUserIdExceptCurrentAsync(long userId, long userTokenId,
      CancellationToken cancellationToken = default);

   public Task<List<Token>> GetAllUserTokensByUserIdWhichAreNotExpiredAsync(long userId,
      CancellationToken cancellationToken = default);

   public Task<Token?> GetUserTokenByRefreshTokenAsync(byte[] refreshTokenHash,
      CancellationToken cancellationToken = default);

   public Task<Token?> GetUserTokenByAccessTokenAsync(byte[] accessTokenHash,
      CancellationToken cancellationToken = default);
}
