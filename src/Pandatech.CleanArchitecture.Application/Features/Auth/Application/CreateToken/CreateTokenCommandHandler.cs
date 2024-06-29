using Microsoft.Extensions.Configuration;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.CreateToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Helpers;
using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.CreateToken;

public class CreateTokenCommandHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
   : ICommandHandler<CreateTokenCommand, CreateTokenCommandResponse>
{
   private const int AccessTokenExpirationMinutes = TokenHelpers.AccessTokenExpirationMinutes;

   private readonly int _refreshTokenExpirationMinutes =
      TokenHelpers.SetRefreshTokenExpirationMinutes(configuration);

   public async Task<CreateTokenCommandResponse> Handle(CreateTokenCommand request,
      CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var accessTokenSignature = Guid.NewGuid().ToString();
      var refreshTokenSignature = Guid.NewGuid().ToString();

      var token = new Token
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

      unitOfWork.Tokens.Add(token);
      await unitOfWork.SaveChangesAsync(cancellationToken);

      return CreateTokenCommandResponse.MapFromEntity(token, accessTokenSignature, refreshTokenSignature);
   }
}
