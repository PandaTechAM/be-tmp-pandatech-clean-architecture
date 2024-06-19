using BaseConverter;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<DeleteUsersCommand>
{
   public async Task Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
   {

      var users = await unitOfWork
         .Users
         .GetByIdsExceptSuperAsync(request.Ids, cancellationToken);

      if (users.Count == 0)
      {
         return;
      }

      foreach (var user in users)
      {
         user.MarkAsDeleted(requestContext.Identity.UserId);
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
