using FluentValidation;
using Pandatech.CleanArchitecture.Core.Helpers;
using SharedKernel.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshTokenSignature)
            .NotEmpty()
            .Must(ValidationHelper.IsGuid)
            .WithMessage(ErrorMessages.InvalidTokenFormat);
    }
}
