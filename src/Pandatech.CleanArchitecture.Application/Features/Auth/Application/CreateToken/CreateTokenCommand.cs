using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.CreateToken;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.CreateToken;

public record CreateTokenCommand(long UserId) : ICommand<CreateTokenCommandResponse>;