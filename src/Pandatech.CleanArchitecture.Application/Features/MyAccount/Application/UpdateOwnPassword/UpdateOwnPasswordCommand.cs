using SharedKernel.ValidatorAndMediatR;

namespace Pandatech.CleanArchitecture.Application.Features.MyAccount.Application.UpdateOwnPassword;

public record UpdateOwnPasswordCommand(string OldPassword, string NewPassword) : ICommand;
