using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeCurrentToken;

public class RevokeCurrentTokenV1CommandHandler(IRequestContext requestContext, IUnitOfWork unitOfWork)
   : ICommandHandler<RevokeCurrentTokenV1Command>
{
   public async Task Handle(RevokeCurrentTokenV1Command request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var token = await unitOfWork.UserTokens
         .GetByIdAsync(requestContext.Identity.UserTokenId, cancellationToken);

      if (token is null)
      {
         throw new NotFoundException("Token not found");
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
