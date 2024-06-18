using FluentValidation;
using Pandatech.CleanArchitecture.Application.Helpers;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
   public CreateUserCommandValidator()
   {
      RuleFor(x => x.FullName).NotEmpty();

      RuleFor(x => x.Username).NotEmpty();

      RuleFor(x => x.UserRole).IsInEnum();
      RuleFor(x => x.UserRole)
         .NotEqual(UserRole.SuperAdmin)
         .WithMessage("not_supported_role");
      RuleFor(x => x.Password)
         .Must(password => password.ValidatePassword())
         .WithMessage(PasswordHelper.WrongPasswordMessage);
   }
}
