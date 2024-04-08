using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokens;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.UpdatePassword;

public class UpdateUserPasswordV1CommandHandler(
   IUnitOfWork unitOfWork,
   Argon2Id argon2Id,
   IRequestContext requestContext)
   : ICommandHandler<UpdateUserPasswordV1Command>
{
   public async Task Handle(UpdateUserPasswordV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

      if (user is null || user.Role == UserRole.SuperAdmin)
      {
         throw new NotFoundException("User not found");
      }

      user.PasswordHash = argon2Id.HashPassword(request.NewPassword);
      user.ForcePasswordChange = true;
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllUserTokensV1Command(request.Id), cancellationToken));
   }
}
