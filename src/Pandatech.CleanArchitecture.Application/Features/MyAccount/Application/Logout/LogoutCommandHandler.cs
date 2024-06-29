using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.Logout;

public class LogoutCommandHandler(IRequestContext requestContext, IUnitOfWork unitOfWork)
   : ICommandHandler<LogoutCommand>
{
   public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
   {
      var now = DateTime.UtcNow;

      var token = await unitOfWork.Tokens
         .GetByIdAsync(requestContext.Identity.TokenId, cancellationToken);

      InternalServerErrorException.ThrowIfNull(token, "Token not found");

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
