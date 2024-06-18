using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.UpdatePasswordForced;

public record UpdatePasswordForcedCommand(string NewPassword) : ICommand;
