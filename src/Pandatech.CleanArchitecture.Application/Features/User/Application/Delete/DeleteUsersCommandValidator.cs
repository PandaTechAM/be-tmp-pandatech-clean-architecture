using FluentValidation;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public class DeleteUsersCommandValidator : AbstractValidator<DeleteUsersCommand>
{
   public DeleteUsersCommandValidator()
   {
      RuleFor(x => x.Ids)
         .NotEmpty()
         .ForEach(x => x.NotEmpty());
   }
}
