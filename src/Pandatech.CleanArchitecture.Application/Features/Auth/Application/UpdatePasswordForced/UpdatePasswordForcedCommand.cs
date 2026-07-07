using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.UpdatePasswordForced;

public record UpdatePasswordForcedCommand(string NewPassword) : ICommand;
