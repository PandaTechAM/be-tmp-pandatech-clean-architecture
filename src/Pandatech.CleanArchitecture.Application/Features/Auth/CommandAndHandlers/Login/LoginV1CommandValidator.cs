using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Login;

public class LoginV1CommandValidator : AbstractValidator<LoginV1Command>
{
   public LoginV1CommandValidator()
   {
      RuleFor(x => x.Username).NotEmpty();
      RuleFor(x => x.Password)
         .Must(password => password.ValidatePassword())
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
