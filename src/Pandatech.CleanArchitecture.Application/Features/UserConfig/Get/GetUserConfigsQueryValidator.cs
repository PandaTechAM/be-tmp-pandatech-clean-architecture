using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Get;

public class GetUserConfigsQueryValidator : AbstractValidator<GetUserConfigsQuery>
{
   public GetUserConfigsQueryValidator()
   {
      RuleFor(x => x.Keys)
         .NotEmpty();

      RuleForEach(x => x.Keys)
         .MaximumLength(256);
   }
}
