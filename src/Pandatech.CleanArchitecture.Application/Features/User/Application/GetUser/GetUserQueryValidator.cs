using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
   public GetUserQueryValidator()
   {
      RuleFor(x => x.Id).NotEmpty();
   }
}
