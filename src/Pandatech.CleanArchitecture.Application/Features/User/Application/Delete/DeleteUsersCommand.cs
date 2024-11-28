using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public record DeleteUsersCommand(string Filter) : ICommand;