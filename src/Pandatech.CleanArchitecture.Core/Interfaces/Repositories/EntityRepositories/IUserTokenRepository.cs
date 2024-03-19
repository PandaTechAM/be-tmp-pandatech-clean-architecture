using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface IUserTokenRepository : IBaseRepository<UserTokenEntity>
{
   public Task<List<UserTokenEntity>> GetAllUserTokensByUserIdExceptCurrentAsync(long userId, long userTokenId, CancellationToken cancellationToken = default);
   public Task<List<UserTokenEntity>> GetAllUserTokensByUserIdWhichAreNotExpiredAsync(long userId, CancellationToken cancellationToken = default);
   
   public Task<UserTokenEntity?> GetUserTokenByRefreshTokenAsync(byte[] refreshTokenHash, CancellationToken cancellationToken = default);
   public Task<UserTokenEntity?> GetUserTokenByAccessTokenAsync(byte[] accessTokenHash, CancellationToken cancellationToken = default);
}
