using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.CreateToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.CreateToken;

public class CreateUserTokenV1CommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
   : ICommandHandler<CreateUserTokenV1Command, CreateUserTokenV1CommandResponse>
{
   private const int AccessTokenExpirationMinutes = UserTokenHelpers.AccessTokenExpirationMinutes;

   private readonly int _refreshTokenExpirationMinutes =
      UserTokenHelpers.SetRefreshTokenExpirationMinutes(configuration);

   public async Task<CreateUserTokenV1CommandResponse> Handle(CreateUserTokenV1Command request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var accessTokenSignature = Guid.NewGuid().ToString();
      var refreshTokenSignature = Guid.NewGuid().ToString();

      var token = new UserTokenEntity
      {
         UserId = request.UserId,
         AccessTokenHash = Sha3.Hash(accessTokenSignature),
         RefreshTokenHash = Sha3.Hash(refreshTokenSignature),
         AccessTokenExpiresAt = now.AddMinutes(AccessTokenExpirationMinutes),
         RefreshTokenExpiresAt = now.AddMinutes(_refreshTokenExpirationMinutes),
         InitialRefreshTokenCreatedAt = now,
         CreatedAt = now,
         UpdatedAt = now
      };

      await unitOfWork.UserTokens.AddAsync(token, cancellationToken);
      await unitOfWork.SaveChangesAsync(cancellationToken);

      return CreateUserTokenV1CommandResponse.MapFromEntity(token, accessTokenSignature, refreshTokenSignature);
   }
}
