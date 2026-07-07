using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.RefreshToken;

public class RefreshTokenCommandResponse
{
    public long UserId { get; set; }

    public bool ForcePasswordChange { get; set; }
    public UserRole UserRole { get; set; }
    public required string AccessTokenSignature { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public required string RefreshTokenSignature { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }

    public static RefreshTokenCommandResponse MapFromTokenEntity(Token token,
        string accessTokenSignature,
        string refreshTokenSignature,
        Token oldToken)
    {
        return new RefreshTokenCommandResponse
        {
            UserId = token.UserId,
            ForcePasswordChange = oldToken.User!.ForcePasswordChange,
            UserRole = oldToken.User.Role,
            AccessTokenSignature = accessTokenSignature,
            AccessTokenExpiration = token.AccessTokenExpiresAt,
            RefreshTokenSignature = refreshTokenSignature,
            RefreshTokenExpiration = token.RefreshTokenExpiresAt
        };
    }
}
