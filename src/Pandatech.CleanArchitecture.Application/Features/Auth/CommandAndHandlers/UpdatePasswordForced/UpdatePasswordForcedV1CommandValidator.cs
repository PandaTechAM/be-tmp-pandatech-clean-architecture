using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdatePasswordForced;

public class UpdatePasswordForcedV1CommandValidator : AbstractValidator<UpdatePasswordForcedV1Command>
{
   public UpdatePasswordForcedV1CommandValidator()
   {
      RuleFor(x => x.NewPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
