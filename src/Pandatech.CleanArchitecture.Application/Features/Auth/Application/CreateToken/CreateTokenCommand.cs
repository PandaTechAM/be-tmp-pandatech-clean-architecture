using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.CreateToken;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.CreateToken;

public record CreateTokenCommand(long UserId) : ICommand<CreateTokenCommandResponse>;
