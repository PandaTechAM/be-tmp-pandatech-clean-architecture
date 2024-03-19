using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Authenticate;

public class AuthenticateV1QueryHandler(IUnitOfWork unitOfWork)
   : IQueryHandler<AuthenticateV1Query, Identity>
{
   public async Task<Identity> Handle(AuthenticateV1Query request, CancellationToken cancellationToken)
   {
      var accessTokenHash = Sha3.Hash(request.AccessTokenSignature);

      var tokenEntity = await unitOfWork.UserTokens.GetUserTokenByAccessTokenAsync(accessTokenHash, cancellationToken);

      if (tokenEntity is null || tokenEntity.User.Status is not UserStatus.Active)
      {
         throw new UnauthorizedException();
      }

      if (tokenEntity.AccessTokenExpiresAt <= DateTime.UtcNow)
      {
         throw new UnauthorizedException("access_token_is_expired");
      }

      return new Identity
      {
         UserId = tokenEntity.UserId,
         Status = tokenEntity.User.Status,
         ForcePasswordChange = tokenEntity.User.ForcePasswordChange,
         FullName = tokenEntity.User.FullName,
         UserRole = tokenEntity.User.Role,
         CreatedAt = tokenEntity.User.CreatedAt,
         UpdatedAt = tokenEntity.User.UpdatedAt,
         UserTokenId = tokenEntity.Id,
         AccessTokenSignature = request.AccessTokenSignature,
         AccessTokenExpiration = tokenEntity.AccessTokenExpiresAt
      };
   }
}
