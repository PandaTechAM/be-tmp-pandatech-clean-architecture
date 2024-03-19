

using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.UpdatePasswordForced;

public record UpdatePasswordForcedV1Command(string NewPassword) : ICommand;
