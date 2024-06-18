using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokens;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.UpdatePassword;

public class UpdateUserPasswordCommandHandler(
   IUnitOfWork unitOfWork,
   Argon2Id argon2Id,
   IRequestContext requestContext)
   : ICommandHandler<UpdateUserPasswordCommand>
{
   public async Task Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

      if (user is null || user.Role == UserRole.SuperAdmin)
      {
         throw new NotFoundException();
      }

      user.PasswordHash = argon2Id.HashPassword(request.NewPassword);
      user.ForcePasswordChange = true;
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllTokensCommand(request.Id), cancellationToken));
   }
}
