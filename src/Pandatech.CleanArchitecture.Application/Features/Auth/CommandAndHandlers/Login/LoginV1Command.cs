using Pandatech.CleanArchitecture.Application.Contracts.Auth.Login;
using Pandatech.CleanArchitecture.Core.Interfaces;

namespace Pandatech.CleanArchitecture.Application.Features.Auth.CommandAndHandlers.Login;

public record LoginV1Command(string Username, string Password) : ICommand<LoginV1CommandResponse>;
