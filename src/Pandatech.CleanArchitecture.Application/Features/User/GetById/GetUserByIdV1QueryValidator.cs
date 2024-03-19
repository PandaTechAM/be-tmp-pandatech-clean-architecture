using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.User.GetById;

public class GetUserByIdV1QueryValidator : AbstractValidator<GetUserByIdV1Query>
{
   public GetUserByIdV1QueryValidator()
   {
      RuleFor(x => x.Id).NotEmpty();
   }
}
