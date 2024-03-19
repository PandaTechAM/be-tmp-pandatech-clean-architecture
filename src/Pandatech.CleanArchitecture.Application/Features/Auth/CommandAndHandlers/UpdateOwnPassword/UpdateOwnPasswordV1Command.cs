

using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdateOwnPassword;

public record UpdateOwnPasswordV1Command(string OldPassword, string NewPassword) : ICommand;
