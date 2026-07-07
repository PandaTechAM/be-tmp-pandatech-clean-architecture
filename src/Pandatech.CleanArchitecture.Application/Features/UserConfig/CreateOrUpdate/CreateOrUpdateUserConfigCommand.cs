using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.CreateOrUpdate;

public record CreateOrUpdateUserConfigCommand(Dictionary<string, string> Configs) : ICommand;
