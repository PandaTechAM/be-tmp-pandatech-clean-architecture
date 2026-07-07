using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

public interface ITokenRepository : IBaseRepository<Token>
{
    public Task<List<Token>> GetAllTokensByUserIdExceptCurrent(long userId,
        long tokenId,
        CancellationToken cancellationToken = default);

    public Task<List<Token>> GetAllTokensByUserIdWhichAreNotExpired(long userId,
        CancellationToken cancellationToken = default);

    public Task<Token?> GetTokenByRefreshToken(byte[] refreshTokenHash,
        CancellationToken cancellationToken = default);

    public Task<Token?> GetTokenByAccessToken(byte[] accessTokenHash,
        CancellationToken cancellationToken = default);
}
