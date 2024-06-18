using FluentValidation;
using Pandatech.CleanArchitecture.Core.Helpers;
using RegexBox;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
   public RefreshTokenCommandValidator()
   {
      RuleFor(x => x.RefreshTokenSignature)
         .NotEmpty()
         .Must(PandaValidator.IsGuid)
         .WithMessage(ErrorMessages.InvalidTokenFormat);
   }
}
