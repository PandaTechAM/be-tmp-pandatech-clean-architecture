using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
   public LoginCommandValidator()
   {
      RuleFor(x => x.Username).NotEmpty();
      RuleFor(x => x.Password)
         .Must(password => password.ValidatePassword())
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
