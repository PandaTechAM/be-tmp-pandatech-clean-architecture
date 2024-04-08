using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokensExceptCurrentSession;

public class RevokeAllUserTokensExceptCurrentV1CommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<RevokeAllUserTokensExceptCurrentV1Command>
{
   public async Task Handle(RevokeAllUserTokensExceptCurrentV1Command request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var tokens = await unitOfWork.UserTokens.GetAllUserTokensByUserIdExceptCurrentAsync(
         requestContext.Identity.UserId, requestContext.Identity.UserTokenId, cancellationToken);

      if (tokens.Count == 0)
      {
         return;
      }

      foreach (var token in tokens)
      {
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
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
