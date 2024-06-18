using Pandatech.CleanArchitecture.Core.Entities;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.CreateToken;

public record CreateTokenCommandResponse(
   long Id,
   long UserId,
   string AccessTokenSignature,
   byte[] AccessTokenHash,
   string RefreshTokenSignature,
   byte[] RefreshTokenHash,
   DateTime AccessTokenExpiresAt,
   DateTime RefreshTokenExpiresAt,
   DateTime CreatedAt,
   DateTime UpdatedAt
)
{
   public static CreateTokenCommandResponse MapFromEntity(Token entity, string accessTokenSignature,
      string refreshTokenSignature)
   {
      return new CreateTokenCommandResponse(
         entity.Id,
         entity.UserId,
         accessTokenSignature,
         entity.AccessTokenHash,
         refreshTokenSignature,
         entity.RefreshTokenHash,
         entity.AccessTokenExpiresAt,
         entity.RefreshTokenExpiresAt,
         entity.CreatedAt,
         entity.UpdatedAt
      );
   }
}
