using Microsoft.Extensions.Hosting;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers.ApiAuth;
using Pandatech.CleanArchitecture.Core.DTOs.Auth;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Auth;

public class AuthQueryHandler(IUnitOfWork unitOfWork, IHostEnvironment environment, IRequestContext requestContext)
   : IQueryHandler<AuthQuery>
{
   public async Task Handle(AuthQuery request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;
      var requestId = request.HttpContext.TryParseRequestId();
      var clientType = request.HttpContext.TryParseClientType().ConvertToEnum(!request.IgnoreClientType);
      var accessTokenSignature = request.HttpContext.TryParseAccessTokenSignature(environment);

      var metadata = new MetaData { RequestId = requestId, RequestTime = now, ClientType = clientType };

      requestContext.MetaData = metadata;

      if (request.Anonymous)
      {
         return;
      }

      UnauthorizedException.ThrowIfNullOrWhiteSpace(accessTokenSignature, ErrorMessages.AccessTokenIsRequired);


      var accessTokenHash = Sha3.Hash(accessTokenSignature);

      var tokenEntity = await unitOfWork.Tokens.GetTokenByAccessTokenAsync(accessTokenHash, cancellationToken);

      UnauthorizedException.ThrowIfNull(tokenEntity);
      UnauthorizedException.ThrowIf(tokenEntity.User.Status is not UserStatus.Active);
      UnauthorizedException.ThrowIf(tokenEntity.AccessTokenExpiresAt <= DateTime.UtcNow,
         ErrorMessages.AccessTokenIsExpired);

      var identity = new Identity
      {
         UserId = tokenEntity.UserId,
         Username = tokenEntity.User.Username,
         Status = tokenEntity.User.Status,
         ForcePasswordChange = tokenEntity.User.ForcePasswordChange,
         FullName = tokenEntity.User.FullName,
         UserRole = tokenEntity.User.Role,
         CreatedAt = tokenEntity.User.CreatedAt,
         UpdatedAt = tokenEntity.User.UpdatedAt,
         TokenId = tokenEntity.Id,
         AccessTokenSignature = accessTokenSignature,
         AccessTokenExpiration = tokenEntity.AccessTokenExpiresAt
      };

      AuthorizationHelper.Authorize(identity, request.MinimalUserRole, request.ForcedToChangePassword);

      requestContext.Identity = identity;
      requestContext.IsAuthenticated = true;
   }
}
