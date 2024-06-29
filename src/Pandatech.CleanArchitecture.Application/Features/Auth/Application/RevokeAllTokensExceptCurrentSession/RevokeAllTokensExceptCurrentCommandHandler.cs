using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokensExceptCurrentSession;

public class RevokeAllTokensExceptCurrentCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<RevokeAllTokensExceptCurrentCommand>
{
   public async Task Handle(RevokeAllTokensExceptCurrentCommand request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var tokens = await unitOfWork.Tokens.GetAllTokensByUserIdExceptCurrentAsync(
         requestContext.Identity.UserId, requestContext.Identity.TokenId, cancellationToken);


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
