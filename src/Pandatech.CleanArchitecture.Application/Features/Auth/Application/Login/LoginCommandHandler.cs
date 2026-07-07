using MediatR;
using Pandatech.CleanArchitecture.Application.Features.Auth.Application.CreateToken;
using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;
using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Helpers;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.Crypto.Helpers;
using ResponseCrafter.HttpExceptions;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Login;

public class LoginCommandHandler(IUnitOfWork unitOfWork, ISender sender)
    : ICommandHandler<LoginCommand, LoginCommandResponse>
{
    public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByUsername(request.Username.ToLower(), cancellationToken);

        if (user is null || user.Status != UserStatus.Active ||
            !Argon2Id.VerifyHash(request.Password, user.PasswordHash))
        {
            throw new BadRequestException(ErrorMessages.InvalidCredentials);
        }

        var token = await sender.Send(new CreateTokenCommand(user.Id), cancellationToken);

        return LoginCommandResponse.MapFromEntity(token, user.Role, user.ForcePasswordChange);
    }
}
