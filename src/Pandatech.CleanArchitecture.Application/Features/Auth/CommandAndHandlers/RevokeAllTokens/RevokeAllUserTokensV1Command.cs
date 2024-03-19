

using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokens;

public record RevokeAllUserTokensV1Command(long UserId) : ICommand;
