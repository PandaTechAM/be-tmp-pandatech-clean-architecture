using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Create;

public record CreateUserCommand(
   string FullName,
   string Username,
   string Password,
   UserRole UserRole,
   string? Comment) : ICommand;
