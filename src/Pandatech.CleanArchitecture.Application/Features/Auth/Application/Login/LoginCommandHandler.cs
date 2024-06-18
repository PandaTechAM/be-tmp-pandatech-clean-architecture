using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.CreateToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto;
using ResponseCrafter.HttpExceptions;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Login;

public class LoginCommandHandler(IUnitOfWork unitOfWork, Argon2Id argon2Id, ISender sender)
   : ICommandHandler<LoginCommand, LoginCommandResponse>
{
   public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
   {
      var user = await unitOfWork.Users.GetByUsernameAsync(request.Username.ToLower(), cancellationToken);

      if (user is null || user.Status != UserStatus.Active ||
          !argon2Id.VerifyHash(request.Password, user.PasswordHash))
      {
         throw new BadRequestException(ErrorMessages.InvalidUsernameOrPassword);
      }

      var userToken = await sender.Send(new CreateTokenCommand(user.Id), cancellationToken);

      return LoginCommandResponse.MapFromEntity(userToken, user.Role, user.ForcePasswordChange);
   }
}
