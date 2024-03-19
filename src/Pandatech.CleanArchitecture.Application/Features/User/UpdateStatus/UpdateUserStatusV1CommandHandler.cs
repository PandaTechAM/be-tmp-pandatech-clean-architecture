using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.UpdateStatus;

public class UpdateUserStatusV1CommandHandler(IUnitOfWork unitOfWork)
   : ICommandHandler<UpdateUserStatusV1Command>
{
   public async Task Handle(UpdateUserStatusV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken: cancellationToken);

      if (user is null || user.Role == UserRole.SuperAdmin)
      {
         throw new NotFoundException("User not found");
      }

      if (user.Status == request.Status)
      {
         throw new BadRequestException("status_already_set");
      }

      user.Status = request.Status;
      user.UpdatedAt = DateTime.UtcNow;

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
