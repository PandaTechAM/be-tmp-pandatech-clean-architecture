using Pandatech.CleanArchitecture.Core.Enums;
using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.User.Application.Create;

public record CreateUserCommand(
    string FullName,
    string Username,
    string Password,
    UserRole UserRole,
    string? Comment) : ICommand;
