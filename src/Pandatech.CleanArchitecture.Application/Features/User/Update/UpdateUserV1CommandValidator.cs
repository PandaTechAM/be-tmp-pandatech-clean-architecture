using FluentValidation;
using Pandatech.CleanArchitecture.Core.Enums;

namespace Pandatech.CleanArchitecture.Application.Features.User.Update;

public class UpdateUserV1CommandValidator : AbstractValidator<UpdateUserV1Command>
{
   public UpdateUserV1CommandValidator()
   {
      RuleFor(x => x.Id).NotEmpty();
      RuleFor(x => x.Username).NotEmpty();
      RuleFor(x => x.FullName).NotEmpty();
      RuleFor(x => x.Role).NotEmpty().IsInEnum()
         .NotEqual(UserRole.SuperAdmin).WithMessage("SuperAdmin role is not allowed");
   }
}
