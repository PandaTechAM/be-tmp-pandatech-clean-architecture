using Pandatech.CleanArchitecture.Application.Contracts.Auth.IdentityState;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.IdentityState;

public class GetIdentityStateV1QueryHandler(IRequestContext requestContext)
   : IQueryHandler<GetIdentityStateV1Query, IdentityStateV1CommandResponse>
{
   public Task<IdentityStateV1CommandResponse> Handle(GetIdentityStateV1Query request,
      CancellationToken cancellationToken)
   {
      return Task.FromResult(IdentityStateV1CommandResponse.MapFromIdentity(requestContext.Identity));
   }
}
