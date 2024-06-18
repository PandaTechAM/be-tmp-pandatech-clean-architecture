using BaseConverter;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<DeleteUsersCommand>
{
   public async Task Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
   {
      List<long> ids = [];
      ids.AddRange(request.Ids.Select(PandaBaseConverter.Base36ToBase10NotNull));

      var users = await unitOfWork
         .Users
         .GetByIdsExceptSuperAsync(ids, cancellationToken);

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
