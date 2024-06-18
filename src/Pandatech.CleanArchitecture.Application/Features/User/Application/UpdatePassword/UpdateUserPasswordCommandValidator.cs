using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.UpdatePassword;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
   public UpdateUserPasswordCommandValidator()
   {
      RuleFor(x => x.Id).NotEmpty();

      RuleFor(x => x.NewPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
