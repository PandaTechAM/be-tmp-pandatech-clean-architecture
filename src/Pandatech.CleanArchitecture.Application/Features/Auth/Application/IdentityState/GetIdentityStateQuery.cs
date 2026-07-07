using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.IdentityState;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.IdentityState;

public class GetIdentityStateQuery : IQuery<IdentityStateCommandResponse>;
