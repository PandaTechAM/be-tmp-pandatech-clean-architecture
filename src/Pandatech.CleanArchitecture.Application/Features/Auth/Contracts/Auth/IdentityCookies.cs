namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Auth;

public class IdentityCookies
{
   public required string AccessTokenSignature { get; set; }
   public required string RefreshTokenSignature { get; set; }
   public DateTime AccessTokenExpiresAt { get; set; }
   public DateTime RefreshTokenExpiresAt { get; set; }
}
