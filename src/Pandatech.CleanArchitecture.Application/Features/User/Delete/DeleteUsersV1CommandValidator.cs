using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.User.Delete;

public class DeleteUsersV1CommandValidator : AbstractValidator<DeleteUsersV1Command>
{
   public DeleteUsersV1CommandValidator()
   {
      RuleFor(x => x.Ids).NotEmpty().ForEach(x => x.NotEmpty());
   }
}
