using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Update;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<UpdateUserCommand>
{
   public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);

      NotFoundException.ThrowIfNull(user);


      var username = request.Username.ToLower();

      if (user.Username != username)
      {
         var duplicateUser = await unitOfWork.Users.IsUsernameDuplicateAsync(username, cancellationToken);
         ConflictException.ThrowIf(duplicateUser, ErrorMessages.DuplicateUsername);

      }

      user.Username = username;
      user.FullName = request.FullName;
      user.Role = request.Role;
      user.Comment = request.Comment;
      user.MarkAsUpdated(requestContext.Identity.UserId);


      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
