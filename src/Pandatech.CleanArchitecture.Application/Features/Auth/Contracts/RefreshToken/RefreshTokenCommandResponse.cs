using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.RefreshToken;

public class RefreshTokenCommandResponse
{
   [PandaPropertyBaseConverter] public long UserId { get; set; }
   public bool ForcePasswordChange { get; set; }
   public UserRole UserRole { get; set; }
   public string AccessTokenSignature { get; set; } = null!;
   public DateTime AccessTokenExpiration { get; set; }
   public string RefreshTokenSignature { get; set; } = null!;
   public DateTime RefreshTokenExpiration { get; set; }

   public static RefreshTokenCommandResponse MapFromUserTokenEntity(Token token,
      string accessTokenSignature, string refreshTokenSignature, Token oldToken)
   {
      return new RefreshTokenCommandResponse
      {
         UserId = token.UserId,
         ForcePasswordChange = oldToken.User.ForcePasswordChange,
         UserRole = oldToken.User.Role,
         AccessTokenSignature = accessTokenSignature,
         AccessTokenExpiration = token.AccessTokenExpiresAt,
         RefreshTokenSignature = refreshTokenSignature,
         RefreshTokenExpiration = token.RefreshTokenExpiresAt
      };
   }
}
