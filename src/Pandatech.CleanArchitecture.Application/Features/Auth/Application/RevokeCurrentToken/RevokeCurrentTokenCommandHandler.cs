using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeCurrentToken;

public class RevokeCurrentTokenCommandHandler(IRequestContext requestContext, IUnitOfWork unitOfWork)
   : ICommandHandler<RevokeCurrentTokenCommand>
{
   public async Task Handle(RevokeCurrentTokenCommand request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var token = await unitOfWork.UserTokens
         .GetByIdAsync(requestContext.Identity.UserTokenId, cancellationToken);

      if (token is null)
      {
         throw new NotFoundException();
      }

      if (token.AccessTokenExpiresAt > now)
      {
         token.AccessTokenExpiresAt = now;
         token.UpdatedAt = now;
      }

      if (token.RefreshTokenExpiresAt > now)
      {
         token.RefreshTokenExpiresAt = now;
         token.UpdatedAt = now;
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
