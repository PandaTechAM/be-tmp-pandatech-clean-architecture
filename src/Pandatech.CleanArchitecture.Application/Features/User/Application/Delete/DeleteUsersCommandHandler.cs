using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<DeleteUsersCommand>
{
   public Task Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
   {
      return unitOfWork.Users
                       .DeleteAsync(request.Filter, requestContext.Identity.UserId, cancellationToken);
   }
}