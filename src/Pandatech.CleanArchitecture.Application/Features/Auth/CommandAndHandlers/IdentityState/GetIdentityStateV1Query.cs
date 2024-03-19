using Pandatech.CleanArchitecture.Application.Contracts.Auth.IdentityState;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.IdentityState;

public class GetIdentityStateV1Query : IQuery<IdentityStateV1CommandResponse>;
