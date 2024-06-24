using Pandatech.CleanArchitecture.Application.Features.MyAccount.Contracts;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.PersonalInformation;

public record GetPersonalInformationQuery : IQuery<GetPersonalInformationQueryResponse>;
