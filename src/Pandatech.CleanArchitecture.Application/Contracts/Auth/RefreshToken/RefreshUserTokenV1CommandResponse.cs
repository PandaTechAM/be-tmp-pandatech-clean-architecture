using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Contracts.Auth.RefreshToken;

public class RefreshUserTokenV1CommandResponse
{
   [PandaPropertyBaseConverter] public long UserId { get; set; }
   public bool ForcePasswordChange { get; set; }
   public UserRole UserRole { get; set; }
   public string AccessTokenSignature { get; set; } = null!;
   public DateTime AccessTokenExpiration { get; set; }
   public string RefreshTokenSignature { get; set; } = null!;
   public DateTime RefreshTokenExpiration { get; set; }

   public static RefreshUserTokenV1CommandResponse MapFromUserTokenEntity(UserTokenEntity userToken,
      string accessTokenSignature, string refreshTokenSignature, UserTokenEntity oldToken)
   {
      return new RefreshUserTokenV1CommandResponse
      {
         UserId = userToken.UserId,
         ForcePasswordChange = oldToken.User.ForcePasswordChange,
         UserRole = oldToken.User.Role,
         AccessTokenSignature = accessTokenSignature,
         AccessTokenExpiration = userToken.AccessTokenExpiresAt,
         RefreshTokenSignature = refreshTokenSignature,
         RefreshTokenExpiration = userToken.RefreshTokenExpiresAt
      };
   }
}
