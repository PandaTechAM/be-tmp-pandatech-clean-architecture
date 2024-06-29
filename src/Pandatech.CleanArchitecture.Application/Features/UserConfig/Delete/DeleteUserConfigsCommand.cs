using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Delete;

public record DeleteUserConfigsCommand(List<string> Keys) : ICommand;
