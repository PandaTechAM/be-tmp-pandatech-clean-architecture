using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Create;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, Argon2Id argon, IRequestContext requestContext)
   : ICommandHandler<CreateUserCommand>
{
   public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
   {
      var isDuplicateUsername =
         await unitOfWork.Users.IsUsernameDuplicateAsync(request.Username.ToLower(), cancellationToken);

      BadRequestException.ThrowIf(isDuplicateUsername, ErrorMessages.DuplicateUsername);

      var passwordHash = argon.HashPassword(request.Password);

      var user = new Core.Entities.User
      {
         Username = request.Username.ToLower(),
         FullName = request.FullName,
         PasswordHash = passwordHash,
         Role = request.UserRole,
         Comment = request.Comment,
         CreatedByUserId = requestContext.Identity.UserId
      };
      unitOfWork.Users.Add(user);
      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
