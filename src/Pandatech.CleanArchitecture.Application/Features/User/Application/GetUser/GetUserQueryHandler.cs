using Pandatech.CleanArchitecture.Application.Features.User.Contracts.GetById;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetUser;

public class GetUserQueryHandler(IUnitOfWork unitOfWork)
   : IQueryHandler<GetUserQuery, GetUserQueryResponse>
{
   public async Task<GetUserQueryResponse> Handle(GetUserQuery request,
      CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdNoTrackingAsync(request.Id, cancellationToken);

      if (user is null || user.Role == UserRole.SuperAdmin)
      {
         throw new NotFoundException();
      }

      return GetUserQueryResponse.MapFromEntity(user);
   }
}
