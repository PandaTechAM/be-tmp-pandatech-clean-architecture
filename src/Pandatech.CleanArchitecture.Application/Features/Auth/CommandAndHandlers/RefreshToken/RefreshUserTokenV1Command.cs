using Pandatech.CleanArchitecture.Application.Contracts.Auth.RefreshToken;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RefreshToken;

public record RefreshUserTokenV1Command(string RefreshTokenSignature) : ICommand<RefreshUserTokenV1CommandResponse>;
