using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.RefreshToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RefreshToken;

public class RefreshUserTokenV1CommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
   : ICommandHandler<RefreshUserTokenV1Command, RefreshUserTokenV1CommandResponse>
{
   private const int AccessTokenExpirationMinutes = UserTokenHelpers.AccessTokenExpirationMinutes;

   private readonly int _refreshTokenExpirationMinutes =
      UserTokenHelpers.SetRefreshTokenExpirationMinutes(configuration);

   private readonly int _refreshTokenMaxExpirationMinutes =
      UserTokenHelpers.SetRefreshTokenMaxExpirationMinutes(configuration);

   public async Task<RefreshUserTokenV1CommandResponse> Handle(RefreshUserTokenV1Command request,
      CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var refreshTokenHash = Sha3.Hash(request.RefreshTokenSignature);

      var userToken = await unitOfWork.UserTokens.GetUserTokenByRefreshTokenAsync(refreshTokenHash,
         cancellationToken);

      ValidateUserToken(userToken, now);

      var newToken =
         CreateNewToken(now, userToken, out var newRefreshTokenSignature, out var accessTokenSignature);

      await unitOfWork.UserTokens.AddAsync(newToken, cancellationToken);
      InvalidateOldToken(userToken, now);
      await unitOfWork.SaveChangesAsync(cancellationToken);
      return RefreshUserTokenV1CommandResponse.MapFromUserTokenEntity(newToken, accessTokenSignature,
         newRefreshTokenSignature, userToken!);
   }

   private static void ValidateUserToken(UserTokenEntity? userToken, DateTime now)
   {
      if (userToken == null)
      {
         throw new NotFoundException("refresh_token_is_invalid");
      }

      if (userToken.User.Status != UserStatus.Active)
      {
         throw new UnauthorizedException("you_cannot_refresh_token_with_this_user");
      }

      if (userToken.RefreshTokenExpiresAt < now)
      {
         throw new UnauthorizedException("refresh_token_has_expired");
      }
   }

   private UserTokenEntity CreateNewToken(DateTime now, UserTokenEntity? userToken, out string refreshTokenSignature,
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

      return new UserTokenEntity
      {
         UserId = userToken.UserId,
         PreviousUserTokenId = userToken.Id,
         AccessTokenHash = Sha3.Hash(accessTokenSignature),
         RefreshTokenHash = Sha3.Hash(refreshTokenSignature),
         AccessTokenExpiresAt = now.AddMinutes(AccessTokenExpirationMinutes),
         RefreshTokenExpiresAt = newExpirationDate,
         InitialRefreshTokenCreatedAt = userToken.InitialRefreshTokenCreatedAt,
         CreatedAt = now,
         UpdatedAt = now
      };
   }

   private static void InvalidateOldToken(UserTokenEntity? userToken, DateTime now)
   {
      userToken!.RefreshTokenExpiresAt = now;
      userToken.UpdatedAt = now;
      if (userToken.AccessTokenExpiresAt > now)
      {
         userToken.AccessTokenExpiresAt = now;
      }
   }
}
