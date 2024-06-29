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

      NotFoundException.ThrowIfNull(user);

      return GetUserQueryResponse.MapFromEntity(user);
   }
}
