using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.RefreshToken;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RefreshToken;

public record RefreshTokenCommand(string RefreshTokenSignature) : ICommand<RefreshTokenCommandResponse>;
