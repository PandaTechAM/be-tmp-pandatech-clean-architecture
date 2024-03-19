using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.User.UpdatePassword;

public class UpdateUserPasswordV1CommandValidator : AbstractValidator<UpdateUserPasswordV1Command>
{
   public UpdateUserPasswordV1CommandValidator()
   {
      RuleFor(x => x.Id).NotEmpty();

      RuleFor(x => x.NewPassword).NotEmpty()
         .Must(PasswordHelper.ValidatePassword)
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
