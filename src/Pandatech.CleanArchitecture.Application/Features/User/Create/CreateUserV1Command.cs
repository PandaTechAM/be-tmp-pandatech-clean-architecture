using Pandatech.CleanArchitecture.Core.Enums;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.User.Create;

public record CreateUserV1Command(
   string FullName,
   string Username,
   string Password,
   UserRole UserRole,
   string? Comment) : ICommand;
