

using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.RevokeAllTokensExceptCurrentSession;

public record RevokeAllUserTokensExceptCurrentV1Command : ICommand;