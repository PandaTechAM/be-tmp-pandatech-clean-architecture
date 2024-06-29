using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Get;

public class GetUserConfigsQueryHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : IQueryHandler<GetUserConfigsQuery, Dictionary<string, string>>
{
   public Task<Dictionary<string, string>> Handle(GetUserConfigsQuery request, CancellationToken cancellationToken)
   {
      return unitOfWork.UserConfigs.GetByUserIdAndKeysAsNotTrackingToDictAsync(requestContext.Identity.UserId,
         request.Keys, cancellationToken);
   }
}
