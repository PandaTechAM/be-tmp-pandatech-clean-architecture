using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokensExceptCurrentSession;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdateOwnPassword;

public class UpdateOwnPasswordV1CommandHandler(
   IRequestContext requestContext,
   IUnitOfWork unitOfWork,
   Argon2Id argon2Id,
   ISender sender) : ICommandHandler<UpdateOwnPasswordV1Command>
{
   public async Task Handle(UpdateOwnPasswordV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users
         .GetByIdAsync(requestContext.Identity.UserId, cancellationToken);

      if (user is null)
      {
         throw new InternalServerErrorException("User not found");
      }

      user.PasswordHash = argon2Id.HashPassword(request.NewPassword);
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllUserTokensExceptCurrentV1Command(), cancellationToken));
   }
}
