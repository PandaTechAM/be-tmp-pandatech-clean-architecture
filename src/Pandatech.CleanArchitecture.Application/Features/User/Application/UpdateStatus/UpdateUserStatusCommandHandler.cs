using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.UpdateStatus;

public class UpdateUserStatusCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<UpdateUserStatusCommand>
{
   public async Task Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

      NotFoundException.ThrowIfNull(user);
      NotFoundException.ThrowIf(user.Role == UserRole.SuperAdmin);

      if (user.Status == request.Status)
      {
         return;
      }

      user.Status = request.Status;
      user.MarkAsUpdated(requestContext.Identity.UserId);

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
