using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.UpdateStatus;

public class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
{
   public UpdateUserStatusCommandValidator()
   {
      RuleFor(x => x.Id).NotEmpty();
      RuleFor(x => x.Status).IsInEnum();
   }
}
