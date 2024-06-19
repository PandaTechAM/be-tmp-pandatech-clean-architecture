using BaseConverter.Attributes;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.CreateToken;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;

public class LoginCommandResponse
{
   [PropertyBaseConverter] public long UserId { get; set; }

   public bool ForcePasswordChange { get; set; }
   public UserRole UserRole { get; set; }
   public string AccessTokenSignature { get; set; } = null!;
   public DateTime AccessTokenExpiration { get; set; }
   public string RefreshTokenSignature { get; set; } = null!;
   public DateTime RefreshTokenExpiration { get; set; }

   public static LoginCommandResponse MapFromEntity(CreateTokenCommandResponse token, UserRole userRole,
      bool forcePasswordChange)
   {
      return new LoginCommandResponse
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
