using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.UpdateOwnPassword;

public record UpdateOwnPasswordCommand(string OldPassword, string NewPassword) : ICommand;
