using Pandatech.CleanArchitecture.Application.Features.MyAccount.Contracts;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.PersonalInformation;

public record GetPersonalInformationQuery : IQuery<GetPersonalInformationQueryResponse>;