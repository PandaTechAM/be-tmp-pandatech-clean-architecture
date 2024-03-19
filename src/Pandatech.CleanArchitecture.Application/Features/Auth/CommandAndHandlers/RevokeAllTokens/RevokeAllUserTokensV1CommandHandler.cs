using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokens;

public class RevokeAllUserTokensV1CommandHandler(IUnitOfWork unitOfWork)
   : ICommandHandler<RevokeAllUserTokensV1Command>
{
   public async Task Handle(RevokeAllUserTokensV1Command request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var tokens =
         await unitOfWork.UserTokens.GetAllUserTokensByUserIdWhichAreNotExpiredAsync(request.UserId, cancellationToken);

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
