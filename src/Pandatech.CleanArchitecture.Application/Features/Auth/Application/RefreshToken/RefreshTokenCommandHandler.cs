using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.RefreshToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RefreshToken;

public class RefreshTokenCommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
   : ICommandHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
{
   private const int AccessTokenExpirationMinutes = TokenHelpers.AccessTokenExpirationMinutes;

   private readonly int _refreshTokenExpirationMinutes =
      TokenHelpers.SetRefreshTokenExpirationMinutes(configuration);

   private readonly int _refreshTokenMaxExpirationMinutes =
      TokenHelpers.SetRefreshTokenMaxExpirationMinutes(configuration);

   public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request,
      CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var refreshTokenHash = Sha3.Hash(request.RefreshTokenSignature);

      var token = await unitOfWork.Tokens.GetTokenByRefreshTokenAsync(refreshTokenHash,
         cancellationToken);

      ValidateUserToken(token, now);

      var newToken =
         CreateNewToken(now, token, out var newRefreshTokenSignature, out var accessTokenSignature);

      unitOfWork.Tokens.Add(newToken);
      InvalidateOldToken(token, now);
      await unitOfWork.SaveChangesAsync(cancellationToken);
      return RefreshTokenCommandResponse.MapFromTokenEntity(newToken, accessTokenSignature,
         newRefreshTokenSignature, token!);
   }

   private static void ValidateUserToken(Token? userToken, DateTime now)
   {
      NotFoundException.ThrowIfNull(userToken);

      UnauthorizedException.ThrowIf(userToken.User.Status != UserStatus.Active,
         ErrorMessages.ThisUserIsNotAllowedToPerformThisAction);

      UnauthorizedException.ThrowIf(userToken.RefreshTokenExpiresAt < now, ErrorMessages.RefreshTokenExpired);
   }

   private Token CreateNewToken(DateTime now, Token? userToken, out string refreshTokenSignature,
      out string accessTokenSignature)
   {
      var newExpirationDate = now.AddMinutes(_refreshTokenExpirationMinutes);

      if (newExpirationDate > userToken!.InitialRefreshTokenCreatedAt.AddMinutes(_refreshTokenMaxExpirationMinutes))
      {
         newExpirationDate = userToken.InitialRefreshTokenCreatedAt.AddMinutes(_refreshTokenMaxExpirationMinutes);
      }

      if (newExpirationDate <= now.AddMinutes(60))
      {
         newExpirationDate = now.AddMinutes(60);
      }

      accessTokenSignature = Guid.NewGuid().ToString();
      refreshTokenSignature = Guid.NewGuid().ToString();

      return new Token
      {
         UserId = userToken.UserId,
         PreviousTokenId = userToken.Id,
         AccessTokenHash = Sha3.Hash(accessTokenSignature),
         RefreshTokenHash = Sha3.Hash(refreshTokenSignature),
         AccessTokenExpiresAt = now.AddMinutes(AccessTokenExpirationMinutes),
         RefreshTokenExpiresAt = newExpirationDate,
         InitialRefreshTokenCreatedAt = userToken.InitialRefreshTokenCreatedAt,
         CreatedAt = now,
         UpdatedAt = now
      };
   }

   private static void InvalidateOldToken(Token? userToken, DateTime now)
   {
      userToken!.RefreshTokenExpiresAt = now;
      userToken.UpdatedAt = now;
      if (userToken.AccessTokenExpiresAt > now)
      {
         userToken.AccessTokenExpiresAt = now;
      }
   }
}
