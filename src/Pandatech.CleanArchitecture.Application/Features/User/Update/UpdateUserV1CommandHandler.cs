using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using ResponseCrafter.StandardHttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.User.Update;

public class UpdateUserV1CommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext) : ICommandHandler<UpdateUserV1Command>
{
   public async Task Handle(UpdateUserV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
      
      if (user is null)
      {
         throw new NotFoundException("User not found");
      }
      
      var username = request.Username.ToLower();
      
      if (user.Username != username)
      {
         var duplicateUser = await unitOfWork.Users.IsUsernameDuplicateAsync(username, cancellationToken);
         
         if (duplicateUser)
         {
            throw new ConflictException("username_already_exists");
         }
      }
      user.Username = username;
      user.FullName = request.FullName;
      user.Role = request.Role;
      user.Comment = request.Comment;
      user.MarkAsUpdated(requestContext.Identity.UserId);

      
      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
