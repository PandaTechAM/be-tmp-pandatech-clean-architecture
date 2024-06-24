using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.UpdateOwnPassword;

public record UpdateOwnPasswordCommand(string OldPassword, string NewPassword) : ICommand;
