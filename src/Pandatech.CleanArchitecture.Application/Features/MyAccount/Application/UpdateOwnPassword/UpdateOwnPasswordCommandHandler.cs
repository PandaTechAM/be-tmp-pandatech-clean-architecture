using Hangfire;
using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokensExceptCurrentSession;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto.Helpers;
using ResponseCrafter.HttpExceptions;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.UpdateOwnPassword;

public class UpdateOwnPasswordCommandHandler(
   IRequestContext requestContext,
   IUnitOfWork unitOfWork,
   ISender sender) : ICommandHandler<UpdateOwnPasswordCommand>
{
   public async Task Handle(UpdateOwnPasswordCommand request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users
                                 .GetById(requestContext.Identity.UserId, cancellationToken);

      InternalServerErrorException.ThrowIfNull(user, "User not found");


      user.PasswordHash = Argon2Id.HashPassword(request.NewPassword);
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChanges(cancellationToken);

      BackgroundJob.Enqueue<ISender>(x => x.Send(new RevokeAllTokensExceptCurrentCommand(), cancellationToken));
   }
}