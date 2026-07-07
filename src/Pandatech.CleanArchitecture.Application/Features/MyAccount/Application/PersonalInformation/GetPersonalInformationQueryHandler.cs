using Pandatech.CleanArchitecture.Application.Features.MyAccount.Contracts;
using Pandatech.CleanArchitecture.Core.Interfaces;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.PersonalInformation;

public class GetPersonalInformationQueryHandler(IRequestContext requestContext)
    : IQueryHandler<GetPersonalInformationQuery, GetPersonalInformationQueryResponse>
{
    public Task<GetPersonalInformationQueryResponse> Handle(GetPersonalInformationQuery request,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(GetPersonalInformationQueryResponse.MapFromRequestContext(requestContext));
    }
}
