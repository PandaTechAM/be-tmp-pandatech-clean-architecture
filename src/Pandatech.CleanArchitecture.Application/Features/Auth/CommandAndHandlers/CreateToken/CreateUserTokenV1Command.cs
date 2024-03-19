using Pandatech.CleanArchitecture.Application.Contracts.Auth.CreateToken;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.CreateToken;

public record CreateUserTokenV1Command(long UserId) : ICommand<CreateUserTokenV1CommandResponse>;
