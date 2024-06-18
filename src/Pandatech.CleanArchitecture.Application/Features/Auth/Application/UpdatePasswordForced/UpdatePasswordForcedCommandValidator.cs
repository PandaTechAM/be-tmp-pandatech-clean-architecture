using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.UpdatePasswordForced;

public class UpdatePasswordForcedCommandValidator : AbstractValidator<UpdatePasswordForcedCommand>
{
   public UpdatePasswordForcedCommandValidator()
   {
      RuleFor(x => x.NewPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
