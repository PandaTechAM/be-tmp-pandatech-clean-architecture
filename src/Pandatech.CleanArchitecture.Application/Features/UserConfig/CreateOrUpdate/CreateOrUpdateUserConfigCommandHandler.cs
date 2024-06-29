using Pandatech.CleanArchitecture.Core.Interfaces;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories;
using Pandatech.CleanArchitecture.Core.Interfaces.Repositories.EntityRepositories;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.CreateOrUpdate;

public class CreateOrUpdateUserConfigCommandHandler(IUnitOfWork unitOfWork, IRequestContext requestContext)
   : ICommandHandler<CreateOrUpdateUserConfigCommand>
{
   public async Task Handle(CreateOrUpdateUserConfigCommand request, CancellationToken cancellationToken)
   {
      var keys = request.Configs.Select(x => x.Key).ToList();

      var userConfigs = await unitOfWork.UserConfigs.GetByUserIdAndKeysAsync(requestContext.Identity.UserId, keys, cancellationToken);

      foreach (var requestedUserConfig in request.Configs)
      {
         var userConfigEntity = userConfigs.Find(x => x.Key == requestedUserConfig.Key);

         if (userConfigEntity is null)
         {
            userConfigEntity = new Core.Entities.UserConfig
            {
               Key = requestedUserConfig.Key,
               Value = requestedUserConfig.Value,
               UserId = requestContext.Identity.UserId,
               CreatedByUserId = requestContext.Identity.UserId
            };

            unitOfWork.UserConfigs.Add(userConfigEntity);
         }
         else
         {
            if (userConfigEntity.Value != requestedUserConfig.Value)
            {
               userConfigEntity.Value = requestedUserConfig.Value;
            }
         }
      }

      await unitOfWork.SaveChangesAsync(cancellationToken);
   }
}
