using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.CreateOrUpdate;

public class CreateOrUpdateUserConfigCommandValidator : AbstractValidator<CreateOrUpdateUserConfigCommand>
{
   public CreateOrUpdateUserConfigCommandValidator()
   {
      RuleFor(x => x.Configs)
         .NotEmpty();
   }
}
