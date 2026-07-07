using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.UserConfig.Delete;

public record DeleteUserConfigsCommand(List<string> Keys) : ICommand;
