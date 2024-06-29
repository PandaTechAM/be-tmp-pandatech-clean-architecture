using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.CreateOrUpdate;

public record CreateOrUpdateUserConfigCommand(Dictionary<string, string> Configs) : ICommand;
