using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RevokeAllTokens;

public record RevokeAllTokensCommand(long UserId) : ICommand;