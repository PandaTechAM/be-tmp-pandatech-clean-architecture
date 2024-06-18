using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokens;

public record RevokeAllTokensCommand(long UserId) : ICommand;
