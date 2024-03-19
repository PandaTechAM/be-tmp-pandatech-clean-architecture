using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdateOwnPassword;

public class UpdateOwnPasswordV1CommandValidator : AbstractValidator<UpdateOwnPasswordV1Command>
{
   public UpdateOwnPasswordV1CommandValidator()
   {
      RuleFor(x => x.OldPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage);
      RuleFor(x => x.NewPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage)
         .NotEqual(x => x.OldPassword)
         .WithMessage("new_password_must_be_different_from_old_password");
   }
}
