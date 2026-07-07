using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.IdentityState;
using Pandatech.CleanArchitecture.Core.Interfaces;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.IdentityState;

public class GetIdentityStateQueryHandler(IRequestContext requestContext)
    : IQueryHandler<GetIdentityStateQuery, IdentityStateCommandResponse>
{
    public Task<IdentityStateCommandResponse> Handle(GetIdentityStateQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(IdentityStateCommandResponse.MapFromIdentity(requestContext.Identity));
    }
}
