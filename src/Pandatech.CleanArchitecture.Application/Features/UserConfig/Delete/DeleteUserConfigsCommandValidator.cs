using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Delete;

public class DeleteUserConfigsCommandValidator : AbstractValidator<DeleteUserConfigsCommand>
{
   public DeleteUserConfigsCommandValidator()
   {
      RuleFor(x => x.Keys)
         .NotEmpty();

      RuleForEach(x => x.Keys)
         .MaximumLength(256);
   }
}
