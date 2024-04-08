using Pandatech.CleanArchitecture.Core.Entities;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Create;

public class CreateUserV1CommandHandler(IUnitOfWork unitOfWork, Argon2Id argon, IRequestContext requestContext)
   : ICommandHandler<CreateUserV1Command>
{
   public async Task Handle(CreateUserV1Command request, CancellationToken cancellationToken)
   {
      var isDuplicateUsername =
         await unitOfWork.Users.IsUsernameDuplicateAsync(request.Username.ToLower(), cancellationToken);

      if (isDuplicateUsername)
      {
         throw new BadRequestException("duplicate_username");
      }

      var passwordHash = argon.HashPassword(request.Password);

      var user = new UserEntity
      {
         Username = request.Username.ToLower(),
         FullName = request.FullName,
         PasswordHash = passwordHash,
         Role = request.UserRole,
         Comment = request.Comment,
         CreatedByUserId = requestContext.Identity.UserId
      };
      await unitOfWork.Users.AddAsync(user, cancellationToken);
      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
