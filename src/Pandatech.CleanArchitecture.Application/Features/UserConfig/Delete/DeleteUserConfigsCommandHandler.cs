using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Delete;

public class DeleteUserConfigsCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
    : ICommandHandler<DeleteUserConfigsCommand>
{
    public async Task Handle(DeleteUserConfigsCommand request, CancellationToken cancellationToken)
    {
        var userConfigs =
            await unitOfWork.UserConfigs.GetByUserIdAndKeys(requestContext.Identity.UserId,
                request.Keys,
                cancellationToken);

        if (userConfigs.Count != 0)
        {
            unitOfWork.UserConfigs.RemoveRange(userConfigs, cancellationToken);

            await unitOfWork.SaveChanges(cancellationToken);
        }
    }
}
