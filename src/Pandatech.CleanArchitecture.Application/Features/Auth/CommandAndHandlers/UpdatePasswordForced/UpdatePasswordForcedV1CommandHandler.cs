using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokensExceptCurrentSession;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdatePasswordForced;

public class UpdatePasswordForcedV1CommandHandler(
   IRequestContext requestContext,
   IUnitOfWork unitOfWork,
   Argon2Id argon2Id,
   ISender sender)
   : ICommandHandler<UpdatePasswordForcedV1Command>
{
   public async Task Handle(UpdatePasswordForcedV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users
         .GetByIdAsync(requestContext.Identity.UserId, cancellationToken);

      if (user is null)
      {
         throw new InternalServerErrorException("User not found");
      }

      var sameWithOldPassword = argon2Id.VerifyHash(request.NewPassword, user.PasswordHash);

      if (sameWithOldPassword)
      {
         throw new BadRequestException("new_password_should_be_different_from_old_password");
      }

      user.PasswordHash = argon2Id.HashPassword(request.NewPassword);
      user.ForcePasswordChange = false;
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllUserTokensExceptCurrentV1Command(), cancellationToken));
   }
}
