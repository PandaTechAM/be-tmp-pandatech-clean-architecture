using Pandatech.CleanArchitecture.Application.Contracts.User.GetById;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.GetById;

public class GetUserByIdV1QueryHandler(IUnitOfWork unitOfWork)
   : IQueryHandler<GetUserByIdV1Query, GetUserByIdV1QueryResponse>
{
   public async Task<GetUserByIdV1QueryResponse> Handle(GetUserByIdV1Query request,
      CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdNoTrackingAsync(request.Id, cancellationToken);

      if (user is null || user.Role == UserRole.SuperAdmin)
      {
         throw new NotFoundException("User not found");
      }

      return GetUserByIdV1QueryResponse.MapFromEntity(user);
   }
}
