using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.CreateToken;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Contracts.Auth.Login;

public class LoginV1CommandResponse
{
   [PandaPropertyBaseConverter] public long UserId { get; set; }

   public bool ForcePasswordChange { get; set; }
   public UserRole UserRole { get; set; }
   public string AccessTokenSignature { get; set; } = null!;
   public DateTime AccessTokenExpiration { get; set; }
   public string RefreshTokenSignature { get; set; } = null!;
   public DateTime RefreshTokenExpiration { get; set; }

   public static LoginV1CommandResponse MapFromEntity(CreateUserTokenV1CommandResponse token, UserRole userRole,
      bool forcePasswordChange)
   {
      return new LoginV1CommandResponse
      {
         UserId = token.UserId,
         ForcePasswordChange = forcePasswordChange,
         UserRole = userRole,
         AccessTokenSignature = token.AccessTokenSignature,
         AccessTokenExpiration = token.AccessTokenExpiresAt,
         RefreshTokenSignature = token.RefreshTokenSignature,
         RefreshTokenExpiration = token.RefreshTokenExpiresAt
      };
   }
}
