using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<DeleteUsersCommand>
{
   public Task Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
   {
      return unitOfWork.Users
                       .Delete(request.Filter, requestContext.Identity.UserId, cancellationToken);
   }
}