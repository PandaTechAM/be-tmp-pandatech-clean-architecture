using Pandatech.CleanArchitecture.Application.Features.Auth.Contracts.Login;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.Application.Login;

public record LoginCommand(string Username, string Password) : ICommand<LoginCommandResponse>;
