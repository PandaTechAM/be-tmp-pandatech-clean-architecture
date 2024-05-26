using MediatR;
using Pandatech.CleanArchitecture.Application.Contracts.Auth.Login;
using Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.CreateToken;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Login;

public class LoginV1CommandHandler(IUnitOfWork unitOfWork, Argon2Id argon2Id, ISender sender)
   : ICommandHandler<LoginV1Command, LoginV1CommandResponse>
{
   public async Task<LoginV1CommandResponse> Handle(LoginV1Command request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByUsernameAsync(request.Username.ToLower(), cancellationToken);

      if (user is null || user.Status != UserStatus.Active ||
          !argon2Id.VerifyHash(request.Password, user.PasswordHash))
      {
         throw new BadRequestException("invalid_username_or_password");
      }

      var userToken = await sender.Send(new CreateUserTokenV1Command(user.Id), cancellationToken);

      return LoginV1CommandResponse.MapFromEntity(userToken, user.Role, user.ForcePasswordChange);
   }
}
