using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokensExceptCurrentSession;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.UpdatePasswordForced;

public class UpdatePasswordForcedCommandHandler(
   IRequestContext requestContext,
   IUnitOfWork unitOfWork,
   Argon2Id argon2Id,
   ISender sender)
   : ICommandHandler<UpdatePasswordForcedCommand>
{
   public async Task Handle(UpdatePasswordForcedCommand request, CancellationToken cancellationToken)
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
         throw new BadRequestException(ErrorMessages.NewPasswordMustBeDifferentFromOldPassword);
      }

      user.PasswordHash = argon2Id.HashPassword(request.NewPassword);
      user.ForcePasswordChange = false;

      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllTokensExceptCurrentCommand(), cancellationToken));
   }
}
