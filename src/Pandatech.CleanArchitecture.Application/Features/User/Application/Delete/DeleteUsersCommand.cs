using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Delete;

public record DeleteUsersCommand(string Filter) : ICommand;